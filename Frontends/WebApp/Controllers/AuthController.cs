﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _identityService.SignIn(signInInput);

        if (!response.IsSuccessful)
        {
            response.Errors.ForEach(x =>
            {
                ModelState.AddModelError(string.Empty,x);
            });

            return View();
        }

        return RedirectToAction(nameof(Index), "Home");
    }
}
