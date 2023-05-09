using Newtonsoft.Json;

configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

public partial class Program
{
    public static IConfiguration? configuration { get; set; }
}

/*public static class SessionExtensions 
{
    public static void SetObjectAsJson(this ISession session, string key, object value) 
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = SessionExtensions.GetString(key);

        return value == null ? default(T) : JsonConvert.DeserializeObject<T>((string)value);
    }

    private static object GetString(string key)
    {
        throw new NotImplementedException();
    }

    public class SessionVariables 
    {
        public const string SessionData = "SessionData";
    }
}*/