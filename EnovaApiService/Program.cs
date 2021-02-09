namespace EnovaApiService
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var config = new HttpSelfHostConfiguration("http://localhost:9000");

            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { controller = "System", id = RouteParameter.Optional });

            var server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Enova Api Server has started at http://localhost:9000");
            Console.ReadLine();
            */

            var bootstrapper = new Bootstrapper();
            bootstrapper.ConfigureComponents();
            bootstrapper.RunServices();
        }
    }
}
