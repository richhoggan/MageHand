namespace TeamServer.Models.Listeners
{
    public class HttpListener : Listener
    {

        public override string Name { get; }
        public int BindPort { get; }
        private CancellationTokenSource _tokenSource;

        public HttpListener(string name, int bindPort)
        {
            Name = name;
            BindPort = bindPort;
        }

        public override async Task Start()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHostDefaults(Host =>
                {
                    Host.UseUrls($"http://0.0.0.0:{BindPort}");
                    Host.Configure(ConfigureApp);
                    Host.ConfigureServices(ConfigureServices);
                });
            var host = hostBuilder.Build();
            _tokenSource = new CancellationTokenSource();
            host.RunAsync(_tokenSource.Token);
        }

        private void ConfigureApp(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("/", "/", new
                {
                    controller = "HttpListener",
                    action = "HandleImplant"
                });
            });
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(AgentService);
        }

        public override void Stop()
        {
            _tokenSource.Cancel();
        }
    }
}
