using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.BLL.Service;
using TeamUp.BLL.contract;
using TeamUp.Model;
using TeamUp.DAL;
using TeamUp.DAL.Interfaces;
using TeamUp.DAL.Repository;
using TeamUp.Utility;

namespace TeamUp.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamUpContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TeamUpSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventsCommentService, EventsCommentService>();
        }
    }
}
