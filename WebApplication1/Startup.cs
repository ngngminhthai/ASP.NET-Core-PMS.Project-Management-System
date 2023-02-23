namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database Services
            services.AddDbContext<ManageAppDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection"),
                   o => o.MigrationsAssembly("PMS.DataEF")));



            services.AddIdentity<ManageUser, AppRole>()
                .AddEntityFrameworkStores<ManageAppDbContext>()
                .AddDefaultTokenProviders();
            #endregion


            #region Identity Server 4
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

            }) //https://nhatkyhoctap.blogspot.com/2017/09/identity-server-4-su-dung-sigining.html
          .AddInMemoryApiResources(Config.Apis) // bên folder IdentityServer thêm Config
                                                // .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
          .AddInMemoryClients(Config.Clients) // lấy ra các client
          .AddInMemoryIdentityResources(Config.Ids)

          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddAspNetIdentity<ManageUser>()
          .AddDeveloperSigningCredential();


            services.AddTransient<IEmailSender, EmailSenderService>();

            services.AddAuthentication()
             .AddLocalApi("Bearer", option =>
             {
                 option.ExpectedScope = "api.WebApp";
             })
             .AddFacebook(facebookOptions =>
             {
                 IConfiguration facebookAuthSection = Configuration.GetSection("Authentication:Facebook");
                 facebookOptions.AppId = facebookAuthSection["AppId"];
                 facebookOptions.AppSecret = facebookAuthSection["AppSecret"];
                 facebookOptions.CallbackPath = "/signin-facebook";
             });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>  // thêm một cái chính sách
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        var attributeRouteModel = selector.AttributeRouteModel;
                        attributeRouteModel.Order = -1;
                        attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
                    }
                });
            });
            #endregion


            #region Services
            //base services
            services.AddControllersWithViews();
            services.AddMediatR(typeof(ListProduct.Handler));
            services.AddMediatR(typeof(ListProject.Handler));

            services.AddSignalR();

            //database services
            services.AddTransient<InitDatabase>();

            #region repository
            //implemented services
            services.AddScoped<IUserClaimsPrincipalFactory<ManageUser>, ClaimsPrincipalFactory>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));


            // project
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectService, ProjectService>();

            services.AddTransient<IProjectUploadedFileRepository, ProjectUploadedFileRepository>();
            services.AddTransient<IProjectUploadedFileService, ProjectUploadedFileService>();

            services.AddTransient<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddTransient<IProjectTaskService, ProjectTaskService>();
            services.AddTransient<IProjectRole_UserService, ProjectRole_UserService>();


            services.AddTransient<IProjectRoleService, ProjectRoleService>();
            services.AddTransient<IProjectFunctionService, ProjectFunctionService>();
            services.AddTransient<IProjectPermissionService, ProjectPermissionService>();

            services.AddTransient<IProjectRoleRepository, ProjectRoleRepository>();
            services.AddTransient<IProjectFunctionRepository, ProjectFunctionRepository>();
            services.AddTransient<IProjectPermissionRepository, ProjectPermissionRepository>();




            services.AddTransient<IProjectTask_UserRepository, ProjectTask_UserRepository>();
            services.AddTransient<IProjectRole_UserRepository, ProjectRole_UserRepository>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleService, RoleService>();


            services.AddTransient<IProjectTask_UserService, ProjectTask_UserService>();
            services.AddTransient<IProjectTask_UserRepository, ProjectTask_UserRepository>();



            services.AddTransient<IProjectCommentRepository, ProjectCommentRepository>();
            services.AddTransient<IProjectCommentService, ProjectCommentService>();


            services.AddTransient<IAuthorizationHandler, BaseResourceAuthorizationHandler>();


            #endregion
            services.AddMvc()
             .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts =>
             {
                 opts.ResourcesPath = "Resources";
             })
             .AddDataAnnotationsLocalization()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
             });


            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(
             opts =>
             {
                 var supportedCultures = new List<CultureInfo>
                 {
                        new CultureInfo("en-US"),
                        new CultureInfo("vi-VN")
                 };

                 opts.DefaultRequestCulture = new RequestCulture("en-US");
                 // Formatting numbers, dates, etc.
                 opts.SupportedCultures = supportedCultures;
                 // UI strings that we have localized.
                 opts.SupportedUICultures = supportedCultures;
             });

            #endregion

            AutoMapperInitializer.Initialize();

            /*#region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp Space Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Configuration["AuthorityUrl"] + "/connect/authorize"),
                            Scopes = new Dictionary<string, string> { { "api.WebApp", "WebApp API" } }
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>{ "api.WebApp" }
                    }
                });


            });
            #endregion*/


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
                endpoints.MapHub<SignalSever>("/signalrServer");
            });

            /*  app.UseSwagger();
              app.UseSwaggerUI(c =>
              {
                  c.OAuthClientId("swagger");
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp Space Api V1");
              });*/

        }
    }
}
