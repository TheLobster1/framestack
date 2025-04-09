using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.ViewModels
{
    public static class ViewModelServiceCollectionExtention
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            //services.AddTransient<StartPageViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<RegisterPageViewModel>();
            services.AddTransient<HomePageViewModel>();
            return services;
        }
    }
}
