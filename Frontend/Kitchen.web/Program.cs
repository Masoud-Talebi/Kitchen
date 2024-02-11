var builder = WebApplication.CreateBuilder(args);

#region Rigester Service
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ISettingService, SettingService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();
builder.Services.AddHttpClient<IFoodService, FoodService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<ICommentService, CommentService>();
builder.Services.AddHttpClient<IUserService, UserService>();
SD.KitchenApiBase = builder.Configuration["ServiceUrls:KitchenUrl"];
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IShopingCartService, ShopingCartService>();
builder.Services.AddScoped<ISettingService, SettingService>();
#endregion

builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var options = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisCache"));
        options.Password = "kitchenmasood1384";
        return ConnectionMultiplexer.Connect(options);
    });

// builder.Services.AddStackExchangeRedisCache(option =>
// {
//     option.Configuration = "localhost:6740";
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
