using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LearnAPI.Repos;
using System.Linq;


public class TableChangeChecker : IHostedService, IDisposable
{
    private readonly IHubContext<MyChatHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;
    private readonly LearndataContext _learndataContext;

    public TableChangeChecker(IHubContext<MyChatHub> hubContext, IServiceProvider serviceProvider , LearndataContext context)
    {
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
        _learndataContext = context;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckTableForChanges, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    private void CheckTableForChanges(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<LearndataContext>();

            bool hasChanges = false;

            // Kiểm tra sự thay đổi trong bảng của bạn, ví dụ như bảng "Products"
            var latestEntry = dbContext.TblOtpManagers.OrderByDescending(p => p.Expiration).FirstOrDefault();
            if (latestEntry != null && latestEntry.Expiration > DateTime.Now.AddSeconds(-10))
            {
                hasChanges = true;
            }

            if (hasChanges)
            {
                _hubContext.Clients.All.SendAsync("ReceiveTableChange", DateTime.Now);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
