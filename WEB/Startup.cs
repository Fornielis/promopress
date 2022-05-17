using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WEB.Context;
using WEB.Models;
using WEB.Repositories;
using WEB.Repositories.Interfaces;
using Pomelo;
using Microsoft.AspNetCore.Identity;

namespace WEB;
public class Startup
{
    public Startup(IConfiguration configuration)
    {   
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("MySql"),ServerVersion.Parse("8.0.28-mysql")));

        services.AddIdentity<IdentityUser, IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

        services.AddTransient<IArquivoRepository, ArquivoRepository>();
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<ILoginRepository, LoginRepository>();
        services.Configure<Configuracao>(Configuration.GetSection("InformacoesGlobais"));
        services.Configure<UsuarioInicial>(Configuration.GetSection("UsuarioMaster"));
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUsuarioRepository usuarioRepository)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();
        usuarioRepository.RoleInicial();
        usuarioRepository.UsuarioInicial();


        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{Id?}");
        });
    }
}