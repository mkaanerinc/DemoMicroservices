using Microsoft.AspNetCore.Mvc;
using SharedNET3.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedNET3.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
