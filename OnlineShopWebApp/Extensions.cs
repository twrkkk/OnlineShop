using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopWebApp.Jobs;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace OnlineShopWebApp
{
    public static class Extensions
    {
        public static bool Remove<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public static void AddQuartzConfiguration(this IServiceCollection services, string cronExpression)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<DeleteUnpaidOrders>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DeleteUnpaidOrders),
                cronExpression: cronExpression)); //run every 5 seconds 0/5 * * * * ?
            services.AddHostedService<QuartzHostedService>();
        }
    }
}
