// Into Program class

using Rx.Calculations.API;

var builder = WebApplication.CreateBuilder(args);

// Add startup services
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);


app.Run();

/* End class Program */



// Startup Class
public class Startup
{
    // Step 1: Add Startup constructor
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    // Step 2: Add ConfigureServices method
    public void ConfigureServices(IServiceCollection services)
    {
        // Get Connection String
        string myConnection = Configuration.GetConnectionString("DefaultConnection");


        // Connection String configuration to use ADO // RxConnectionOptions object with connectionString property
        services.AddOptions();
        services.Configure<RxConnectionOptions>(connection =>
        {
            connection.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        });
        
        
        services.AddTransient<ITestingBL,  TestingBL>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    
    // Step 3: Add Configure method
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}


/* **************************Helper class********************************** */

// Helper class to use services.AddOptions
public class RxConnectionOptions
{
    public string ConnectionString { get; set; }
}
