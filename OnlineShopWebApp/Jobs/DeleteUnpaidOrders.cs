using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineShop.Db.Enums;
using OnlineShopWebApp.Interfaces;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Jobs
{
    [DisallowConcurrentExecution]
    public class DeleteUnpaidOrders : IJob
    {
        private readonly IServiceProvider _provider;

        public DeleteUnpaidOrders() { }

        private readonly ILogger<DeleteUnpaidOrders> _logger;

        public DeleteUnpaidOrders(ILogger<DeleteUnpaidOrders> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var orderStorage = scope.ServiceProvider.GetService<IOrderStorage>();
                var unpaidOrders = orderStorage
                    .GetOrdersQuery()
                    .Where(x => 
                        x.OrderStatus != OrderStatus.Canceled
                        && x.PaymentStatus == PaymentStatus.NotPaid
                        && x.OrderTime.AddHours(24) < DateTime.Now);
                orderStorage.UpdateStatusList(unpaidOrders, OrderStatus.Canceled);
                _logger.LogInformation("processing unpaid orders completed");
            }

            await Task.CompletedTask;
        }
    }
}
