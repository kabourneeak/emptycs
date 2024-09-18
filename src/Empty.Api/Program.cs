using Empty.Api;
using Prometheus;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "[HH:mm:ss] ";
            });
        });

        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        // to add our services from <see cref="HostExtensions"/>
        builder.AddApi();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMetricServer();

        app.MapControllers();
        
        app.Run();
    }
}
