using Application;
using Infrastructure;
using Identity;
using Application.AIML;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                          policy.WithOrigins("https://localhost:4200");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

builder.Services.AddApplication();
if (!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
    builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("UserConnection")));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
    builder.Services.AddDbContext<UsersDbContext>(options => options.UseInMemoryDatabase("TestDb_Identity"));
}
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please insert JWT token into field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// If you have a pre-trained model, just load it here. If not, train it.
// Assuming you want to train at startup from properties.csv:
var predictionModel = new PropertyPricePredictionModel();

// Make sure properties.csv is accessible. For example, if it's in src/Data and copied to output, 
// you can reference it directly as "Data/properties.csv" if Data folder is also copied to output directory.
var dataPath = @"C:\Users\ghiar\Desktop\.NET\SmartRealEstateManagementSystem\Application\AIML\Data\properties_cleaned.csv";

// Train the model at startup (only do this if you don't have a pre-trained model.zip)
predictionModel.Train(dataPath);

// Optionally, save the trained model so next time you can just load it:
var modelPath = @"C:\Users\ghiar\Desktop\.NET\SmartRealEstateManagementSystem\Application\AIML\Data\model.zip";
predictionModel.SaveModel(modelPath);

// Register the model as a singleton so the controller can use it:
builder.Services.AddSingleton(predictionModel);

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

public partial class Program { }
