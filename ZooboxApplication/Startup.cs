////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Startup.cs
//
// summary:	Implements the startup class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace ZooboxApplication
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A startup. </summary>
    ///
    /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Startup
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <param name="configuration">    The configuration. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Configure services. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <param name="services"> The services. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                  //options.UseSqlServer(
                  //  Configuration.GetConnectionString("DefaultConnection")));
                  options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
        // services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Configures. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <param name="app">  The application. </param>
        /// <param name="env">  The environment. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An email sender. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public class EmailSender : IEmailSender
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Sends an email asynchronous. </summary>
            ///
            /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
            ///
            /// <param name="email">    The email. </param>
            /// <param name="subject">  The subject. </param>
            /// <param name="message">  The message. </param>
            ///
            /// <returns>   An asynchronous result. </returns>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public Task SendEmailAsync(string email, string subject, string message)
            {
                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("Zoobox", "zoobox.app@gmail.com"));
                msg.To.Add(new MailboxAddress(email, email));
                msg.Subject = subject;
                msg.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("zoobox.app@gmail.com", "123456_Abc");
                    client.Send(msg);
                    client.Disconnect(true);
                }

                    return Task.CompletedTask;
            }
        }
    }
}
