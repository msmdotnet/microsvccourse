var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGetSpecialsServices(getSpecialsOptions =>
{
    builder.Configuration.GetRequiredSection(GetSpecialsOptions.SectionKey)
    .Bind(getSpecialsOptions);
},
getSpecialsDBOptions =>
{
    builder.Configuration.GetRequiredSection(GetSpecialsDBOptions.SectionKey)
    .Bind(getSpecialsDBOptions);
});
/*

builder.Services.AddGetSpecialsCoreServices(getSpecialsOptions =>
{
    builder.Configuration.GetRequiredSection(GetSpecialsOptions.SectionKey)
    .Bind(getSpecialsOptions);
});

builder.Services.AddRepositories(getSpecialsDBOptions =>
{
    builder.Configuration.GetRequiredSection(GetSpecialsDBOptions.SectionKey)
    .Bind(getSpecialsDBOptions);
});
*/

builder.Services.AddCors( options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    }
    );
});

builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(Endpoints.GetSpecials,
    async (IGetSpecialsController controller) =>
    TypedResults.Ok(await controller.GetSpecialsAsync()));

app.InitializeDB();

app.Run();


