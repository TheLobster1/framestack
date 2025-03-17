using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framestack.Views
{
    public static class ViewsServiceCollectionExtention
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            //services.AddTransient<StartPage>();
            services.AddTransient<MainPage>();

            return services;
        }
    }
}
