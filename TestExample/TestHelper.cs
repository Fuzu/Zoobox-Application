using System;
using System.Collections.Generic;
using System.Text;
using ZooboxApplication.Data;
using ZooboxApplication.Models;
using ZooboxApplication.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestZoobox
{
    class TestHelper
    {
        public IServiceProvider ServiceProvider { get; }
        public ApplicationDbContext Context { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public IEmailSender IEmailSender { get; }
        public UrlEncoder UrlEncoder { get; }

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="popularBd"> Se verdadeiro usa a class DbInitializer para popular a base de daos em memoria</param>
        public TestHelper(bool popularBd)
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            var config = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton<IConfiguration>(config);
            services.AddOptions();
            services
                .AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase("Scratch").UseInternalServiceProvider(efServiceProvider));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 2;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddTransient<ZooboxApplication.Startup.EmailSender, AuthMessageSender>();

            services.AddMvc();
            services.AddSingleton<IAuthenticationService, NoOpAuth>();
            services.AddLogging();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = context,
                });

            //services.Configure<DefinicoesEmail>(config.GetSection("DefinicoesEmail"));

            ServiceProvider = services.BuildServiceProvider();

            //IEmailSender = ServiceProvider.GetRequiredService<IEmailSender>();


            UrlEncoder = ServiceProvider.GetRequiredService<UrlEncoder>();
            Context = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager = ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SignInManager = ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

            if (popularBd)
            {
                PopularBd();
            }
        }

        /// <summary>
        /// Metodo para popular a BD
        /// </summary>
        public void PopularBd()
        {
            //ApplicationDbContext init = new ApplicationDbContext(Context, UserManager, RoleManager, ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>());
            //init.Inicializar().Wait();
        }
        /// <summary>
        /// Gera o HttpContext para poder ser inserido no controlador
        /// </summary>
        /// <param name="userId">Id do utilizador que se prentede que esteja logado (com sessão iniciada)</param>
        /// <returns></returns>
        public HttpContext GerarHttpContext(string userId = null)
        {
            var httpContext = ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;


            if (!String.IsNullOrEmpty(userId))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
                httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }

            httpContext.RequestServices = ServiceProvider;
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost", 44365);
            return httpContext;
        }

    }
    public class NoOpAuth : IAuthenticationService
    {
        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return Task.FromResult(0);
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return Task.FromResult(0);
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        Task<AuthenticateResult> IAuthenticationService.AuthenticateAsync(HttpContext context, string scheme)
        {
            throw new NotImplementedException();
        }

        Task IAuthenticationService.ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        Task IAuthenticationService.ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        Task IAuthenticationService.SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        Task IAuthenticationService.SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
