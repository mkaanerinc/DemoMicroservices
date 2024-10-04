# Demo Microservice Project


## Overview
This demo project is an ongoing microservices-based architecture built using .NET. The system aims to be scalable, maintainable, and efficient with each microservice being developed independently.


## Table of Contents
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Roadmap](#roadmap)
- [License](#license)


## Features
- **Catalog API**: The microservice that will be responsible for storing and presenting information about our courses.
- **PhotoStock API**: The microservice that will be responsible for storing and presenting course images.
- **Basket API**: The microservice that will be responsible for handling cart operations.
- **Discount API**: The microservice that will be responsible for managing discount coupons assigned to the user.
- **Order API**: The microservice that will be responsible for handling order operations.
- **FakePayment API**: The microservice that will be responsible for handling payment operations.
- **Gateway**: Ocelot
- **ASP.NET Core MVC App**: The UI microservice that will be responsible for displaying data received from other microservices and interacting with the user.
- **Authentication and Authorization**: Implemented using IdentityServer. The microservice that will be responsible for storing user data and generating tokens and refresh tokens.
- **Database Integration**: SQL Server, PostgreSQL, MongoDB, Redis


## Architecture
The architecture of this project follows a microservices pattern where each service is built using .NET. Some services are fully implemented, while others are still being developed.


## Technologies Used
- **Programming Language**: C#
- **Frameworks**: 
  - **ASP.NET Core**: For building web APIs and building UI.
  - **Entity Framework Core**: For data access.
  - **Dapper**: For data access.
- **Database**: SQL Server, PostgreSQL, MongoDB, Redis
- **Message Broker**: RabbitMQ
- **Authentication**: IdentityServer
- **Containerization**: Docker
- **API Gateway**: Ocelot
- **Patterns**: CQRS, Domain Driven Design


## Roadmap

- [x] **Catalog API**: Completed
- [x] **Identity Server**: Completed
- [x] **PhotoStock API**: Completed
- [x] **Basket API**: Completed
- [x] **Discount API**: Completed
- [x] **Order API**: Completed
- [x] **FakePayment API**: Completed
- [x] **Gateway**: Completed
- [ ] **ASP.NET Core MVC App**: In Progress
- [ ] **MassTransit-RabbitMQ**: Planned
- [ ] **Eventual Consistency**: Planned
- [ ] **Token Exchange**: Planned
- [ ] **Docker Compose**: Planned


## License

This project was created following the [.NET ile Microservices](https://www.udemy.com/course/net-ile-microservices/?couponCode=KEEPLEARNING) from Udemy, taught by Fatih Çakıroğlu.
