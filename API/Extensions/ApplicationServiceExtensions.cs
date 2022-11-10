﻿using Aplication.Interfaces;
using Infrastruture.Services;

namespace API.Extensions;

public static  class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserNameList, UserList>();
        return services;
    }
}