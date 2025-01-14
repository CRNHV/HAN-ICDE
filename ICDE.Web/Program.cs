using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ICDE.Data;
using ICDE.Data.Entities;
using ICDE.Data.Entities.Identity;
using ICDE.Data.Extensions;
using ICDE.Data.Interceptors;
using ICDE.Lib.Extensions;
using ICDE.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ICDE.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services
            .AddLib()
            .AddData();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Identity.Application";
            options.DefaultSignInScheme = "Identity.Application";
            options.DefaultChallengeScheme = "Identity.Application";
        }).AddCookie("Identity.Application", options =>
        {
            options.LoginPath = "/auth/login"; // Redirect here if not authenticated
            options.LogoutPath = "/auth/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration
            options.AccessDeniedPath = "/error/401";
        });

        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();

        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer("Server=localhost;Database=ICDE;Trusted_Connection=True;Encrypt=False");
            opt.AddInterceptors(new OnderwijsOnderdeelInterceptor());
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/auth/login";
        });

        var app = builder.Build();

        // Test code to seed the database.

        SeedDatabase(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePagesWithReExecute("/Error/{0}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static async void SeedDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            if (!dbContext.Roles.Any())
            {
                roleManager.CreateAsync(new Role()
                {
                    Name = "Auteur",
                    NormalizedName = "AUTEUR"
                }).Wait();

                roleManager.CreateAsync(new Role()
                {
                    Name = "Docent",
                    NormalizedName = "DOCENT"
                }).Wait();

                roleManager.CreateAsync(new Role()
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }).Wait();

                usermanager.CreateAsync(new User()
                {
                    UserName = "Student",
                }, "Student12345!").Wait();

                usermanager.CreateAsync(new User()
                {
                    UserName = "Docent",
                }, "Docent12345!").Wait();

                usermanager.CreateAsync(new User()
                {
                    UserName = "Auteur",
                }, "Auteur12345!").Wait();

                var docent = usermanager.FindByNameAsync("Docent").Result;
                var student = usermanager.FindByNameAsync("Student").Result;
                var auteur = usermanager.FindByNameAsync("Auteur").Result;

                usermanager.AddToRoleAsync(docent, "Docent").Wait();
                usermanager.AddToRoleAsync(student, "Student").Wait();
                usermanager.AddToRoleAsync(auteur, "Auteur").Wait();

                usermanager.AddClaimsAsync(docent, new List<Claim>()
                {
                    new Claim("Id", $"{docent.Id}"),
                    new Claim("Role", "Docent"),
                }).Wait();

                usermanager.AddClaimsAsync(auteur, new List<Claim>()
                {
                    new Claim("Id", $"{auteur.Id}"),
                    new Claim("Role", "Auteur"),
                }).Wait();

                usermanager.AddClaimsAsync(student, new List<Claim>()
                {
                    new Claim("Id", $"{student.Id}"),
                    new Claim("Role", "Student"),
                }).Wait();

                var studentEntity = new Student()
                {
                    User = student,
                    UserId = student.Id,
                };

                dbContext.Studenten.Add(studentEntity);
                dbContext.SaveChanges();

                var leeruitkomst1 = new Leeruitkomst()
                {
                    Naam = "Kan Chinees",
                    Beschrijving = "Moet chinees kunnen op C1 niveau",
                    GroupId = Guid.NewGuid(),
                };

                var leeruitkomst2 = new Leeruitkomst()
                {
                    Naam = "Kan jongleren",
                    Beschrijving = "Moet kunnen jongleren",
                    GroupId = Guid.NewGuid(),
                };

                var leeruitkomst3 = new Leeruitkomst()
                {
                    Naam = "Kan programmeren",
                    Beschrijving = "Moet kunnen progammeren in 200 verschillende talen.",
                    GroupId = Guid.NewGuid(),
                };

                var leeruitkomst4 = new Leeruitkomst()
                {
                    Naam = "Heeft er nog zin in",
                    Beschrijving = "Moet het wel allemaal kunnen opbrengen eh.",
                    GroupId = Guid.NewGuid(),
                };


                var opleiding = new Opleiding()
                {
                    Naam = "HBO-ICT",
                    Beschrijving = "HBO ICT aan de HAN!!!!!!",
                    GroupId = Guid.NewGuid(),
                };

                var vak = new Vak()
                {
                    Naam = "ADB",
                    Beschrijving = "Advanced databases",
                    GroupId = Guid.NewGuid(),
                    Leeruitkomsten = new List<Leeruitkomst>()
                    {
                        leeruitkomst1,
                        leeruitkomst2,
                        leeruitkomst4,
                    }
                };

                var cursus1 = new Cursus()
                {
                    Naam = "RDT",
                    Beschrijving = "Research database technologie",
                    GroupId = Guid.NewGuid(),
                    Leeruitkomsten = new List<Leeruitkomst>()
                    {
                        leeruitkomst1,
                        leeruitkomst2,
                        leeruitkomst4,
                    },
                    CursusMateriaal = "1x Ak-47, 100x 7.62mm"
                };

                var cursus2 = new Cursus()
                {
                    Naam = "RDI",
                    Beschrijving = "Relationele database implementatie",
                    GroupId = Guid.NewGuid(),
                };

                var cursus3 = new Cursus()
                {
                    Naam = "DDQ",
                    Beschrijving = "Data kwaliteit en data....",
                    GroupId = Guid.NewGuid(),
                };

                var planning = new Planning()
                {
                    Name = "Planning RDT 2024",
                    PlanningItems = new System.Collections.Generic.List<PlanningItem>()
                    {
                        new PlanningItem()
                        {
                            Index = 0,
                            Les = new Les()
                            {
                                Naam = "Introductie",
                                Beschrijving = "De RDT introductie",
                                GroupId = Guid.NewGuid(),
                                Leeruitkomsten = new List<Leeruitkomst>()
                                {
                                    leeruitkomst1,
                                    leeruitkomst2
                                }
                            }
                        },
                        new PlanningItem()
                        {
                            Index = 1,
                            Les = new Les()
                            {
                                Naam = "Les 2",
                                Beschrijving = "Dit is de tweede les",
                                GroupId = Guid.NewGuid(),
                                Leeruitkomsten = new List<Leeruitkomst>()
                                {
                                    leeruitkomst2
                                },
                                LesInhoud = "Quantum mechanics for babies."
                            }
                        },
                        new PlanningItem()
                        {
                            Index = 2,
                            Opdracht = new Opdracht()
                            {
                                 GroupId = Guid.NewGuid(),
                                 Naam = "Grote boze Casus!1",
                                 Beschrijving = "Maak de casus in week 3 lol haha",
                                 BeoordelingCritereas = new List<BeoordelingCriterea>()
                                 {
                                     new BeoordelingCriterea()
                                     {
                                         Naam = "Voldoet aan eisen enzo",
                                         Beschrijving = "Het gaat om de volgende eisen: .......",
                                         GroupId = Guid.NewGuid(),
                                         Leeruitkomsten = new List<Leeruitkomst>()
                                         {
                                             leeruitkomst1
                                         }
                                     },
                                     new BeoordelingCriterea()
                                     {
                                         Naam = "Vloeiend Chinees",
                                         Beschrijving = "Ja je zal toch echt vloeiend Chinees moeten kunnen",
                                         GroupId = Guid.NewGuid(),
                                         Leeruitkomsten = new List<Leeruitkomst>()
                                         {
                                             leeruitkomst1
                                         }
                                     },
                                     new BeoordelingCriterea()
                                     {
                                         Naam = "Jongleren",
                                         Beschrijving = "Je zal 100 balletjes moeten kunnen jongleren",
                                         GroupId = Guid.NewGuid(),
                                         Leeruitkomsten = new List<Leeruitkomst>()
                                         {
                                             leeruitkomst1
                                         }
                                     },
                                 },
                                 IngeleverdeOpdrachten = new List<IngeleverdeOpdracht>()
                                 {
                                     new IngeleverdeOpdracht()
                                     {
                                         StudentNummer = studentEntity.StudentNummer,
                                         Locatie = "./Opdrachten/asjdhajksd.png",
                                         Naam = "Casus.zip",
                                     }
                                 },
                                 DeadlineUtc = DateTime.UtcNow,
                            }
                        }
                    }
                };

                dbContext.Opleidingen.Add(opleiding);
                dbContext.Vakken.Add(vak);
                dbContext.Cursussen.Add(cursus1);
                dbContext.Cursussen.Add(cursus2);
                dbContext.Cursussen.Add(cursus3);
                dbContext.Plannings.Add(planning);

                dbContext.SaveChanges();

                opleiding.Vakken.Add(vak);
                vak.Cursussen.Add(cursus1);
                vak.Cursussen.Add(cursus2);
                vak.Cursussen.Add(cursus3);

                cursus1.Planning = planning;

                dbContext.SaveChanges();
            }
        }
    }
}
