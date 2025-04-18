﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Services
{

    public static class ServicesServiceCollectionExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // services.AddTransient<LocalUserStorage>();
            services.AddSingleton<LocalUserStorage>();
            return services;
        }
    }
}
