// use connection string with IOptions

/* Create a class with a field thta will receive information */
public class MyOptions
{
    public string ConnectionString { get; set; }
}

/* In the Startup class add services.Configure method */
public void ConfigureServices(IServiceCollection services)
{ 
    // Adds services required for using options.
    services.AddOptions();

    services.Configure<MyOptions>(myOptions =>
    {
        myOptions.ConnString = Configuration.GetConnectionString("MyContext");
    });
}

/* Inside the contoller add constructor, property and use IOptions */
 public class RxCalculationsController : ControllerBase
 {
     // Add property
     IOptions<RxConnectionOptions> connectionOptions;

     // Constructor and inject IOptions
     public RxCalculationsController(IOptions<RxConnectionOptions> options)
     {
         connectionOptions = options;
     }

     // Method
     [HttpGet(Name = "TestConnection")]
     public async Task TestConnection()
     {
         var connectionString = connectionOptions.Value.ConnectionString;
     }
 }