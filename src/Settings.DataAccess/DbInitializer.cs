using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Settings.Common.Domain;

namespace Settings.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(SettingsDbContext context, bool refreshData)
        {
            if(refreshData)
            {
                var settings = context.Settings.ToList();
                foreach(var setting in settings)
                {
                    context.Remove(setting);
                }
                context.SaveChanges();

                var envs = context.Environments.ToList();
                foreach (var env in envs)
                {
                    context.Remove(env);
                }
                context.SaveChanges();

                var apps = context.Applications.ToList();
                foreach (var app in apps)
                {
                    context.Remove(app);
                }
                context.SaveChanges();
            }
            //TODO: Need to add some contraints here and/or make sure they are created
            //1. Unique on application name and environment name
            context.Database.EnsureCreated();

            if(!context.Applications.Any())
            {
                var app_global = new Application
                {
                    Name = "Global"
                };

                    var app_global_igaming = new Application
                    {
                        Name = "iGaming",
                        Parent = app_global
                    };

                        var app_global_igaming_boss = new Application
                        {
                            Name = "Boss",
                            Parent = app_global_igaming
                        };

                        var app_global_igaming_boss_bonus = new Application
                        {
                            Name = "Bonus",
                            Parent = app_global_igaming
                        };

                    var app_global_TwinSpires = new Application
                    {
                        Name = "TwinSpires",
                        Parent = app_global
                    };

                        var app_global_frontend = new Application
                        {
                            Name = "Frontend",
                            Parent = app_global_TwinSpires
                        };
                            var app_global_frontend_tux = new Application
                            {
                                Name = "Tux",
                                Parent = app_global_frontend
                            };
                            var app_global_frontend_mdot = new Application
                            {
                                Name = "Mdot",
                                Parent = app_global_frontend
                            };
                                var app_global_frontend_mdot_native = new Application
                                {
                                    Name = "Native",
                                    Parent = app_global_frontend_mdot
                                };
                                    var app_global_frontend_mdot_native_iOS = new Application
                                    {
                                        Name = "Native-iOS",
                                        Parent = app_global_frontend_mdot_native
                                    };
                        var app_global_backend = new Application
                        {
                            Name = "Backend",
                            Parent = app_global_TwinSpires
                        };
                            var app_global_backend_adw = new Application
                            {
                                Name = "ADW",
                                Parent = app_global_backend
                            };
                            var app_global_backend_rtws = new Application
                            {
                                Name = "RTWS",
                                Parent = app_global_backend
                            };

                context.Applications.Add(app_global);
                    context.Applications.Add(app_global_igaming);
                        context.Applications.Add(app_global_igaming_boss);
                        context.Applications.Add(app_global_igaming_boss_bonus);
                    context.Applications.Add(app_global_TwinSpires);
                        context.Applications.Add(app_global_frontend);
                            context.Applications.Add(app_global_frontend_mdot);
                                context.Applications.Add(app_global_frontend_mdot_native);
                                context.Applications.Add(app_global_frontend_mdot_native_iOS);
                            context.Applications.Add(app_global_frontend_tux);
                        context.Applications.Add(app_global_backend);
                            context.Applications.Add(app_global_backend_adw);
                            context.Applications.Add(app_global_backend_rtws);
                
                context.SaveChanges();

                var envAll = new Environment
                {
                    Name = "All",
                };

                        var envProduction = new Environment
                        {
                            Name = "Production",
                            Parent = envAll
                        };

                        var envDevelopment = new Environment
                        {
                            Name = "Development",
                            Parent = envAll
                        };

                            var envDevelopment_2800 = new Environment
                            {
                                Name = "Dev-2800",
                                Parent = envDevelopment
                            };

                                var envDevelopment_2800_local = new Environment
                                {
                                    Name = "Dev-2800-local",
                                    Parent = envDevelopment_2800
                                };
                                    var envDevelopment_2800_local_jose_pacheco = new Environment
                                    {
                                        Name = "Dev-2800-local-jose-pacheco",
                                        Parent = envDevelopment_2800_local
                                    };

                            var envDevelopment_4100 = new Environment
                            {
                                Name = "Dev-4100",
                                Parent = envDevelopment
                            };
                        
                        var envStaging = new Environment
                        {
                            Name = "Staging",
                            Parent = envAll
                        };

                context.Environments.Add(envAll);
                    context.Environments.Add(envProduction);
                    context.Environments.Add(envDevelopment);
                        context.Environments.Add(envDevelopment_2800);
                        context.Environments.Add(envDevelopment_4100);
                            context.Environments.Add(envDevelopment_2800_local);
                                context.Environments.Add(envDevelopment_2800_local_jose_pacheco);
                    context.Environments.Add(envStaging);

                context.SaveChanges();


                // var config_global_all = JsonConvert.SerializeObject(new
                // {
                //     CompanyName = "TestCompanyName",
                //     Website = "www.testcompanyname.com",
                //     ContactNumber = "1-800-222-2222"
                // });
                
                //    var config_userportal_all = JsonConvert.SerializeObject(new {
                //        PasswordExpirationDays = 90,                         //add
                //        ContactNumber = "1-800-222-1111",                    //override
                //        AdminRoles = new JArray("LocalAdmin", "SuperAdmin")  //add
                //    });
                
                //        var config_userportal_production = JsonConvert.SerializeObject(new
                //        {
                //            DbConnection = "production.dbserver;database=userportal",    //add
                //            Website = "userportal.testcompanyname.com",                  //override
                //        });
                
                //        var config_userportal_development = JsonConvert.SerializeObject(new
                //        {
                //            DbConnection = "development.dbserver;database=userportal",   //add
                //            Website = "development.userportal.testcompanyname.com"       //override
                //        });
                
                //             var config_userportal_developmentJose = JsonConvert.SerializeObject(new
                //            {
                //                DbConnection = "localhost;database=userportal",  //add
                //                Website = "localhost:8080",                      //override
                //                InDevelopmentFeatureConfig = "test_value"        //add
                //            });
                
                //         var config_userportal_stage = JsonConvert.SerializeObject(new
                //         {
                //             DbConnection = "stage.dbserver;database=userportal", //add
                //             Website = "stage.userportal.testcompanyname.com"     //override
                //         });
                
                //         //Note that this is same environment as above,
                //         //but now data integration application
                //         var config_userportal__dataintegration_development = 
                //         JsonConvert.SerializeObject(new
                //         {
                //             MessageQueueConnection = "RabbitMq.development"      //add
                //         });

                           

                // context.Settings.Add(new Setting
                // {
                //     Application = app_global,
                //     Environment = envAll,
                //     Contents = config_global_all
                // });
                // context.Settings.Add(new Setting
                // {
                //     Application = app_global_igaming_boss,
                //     Environment = envAll,
                //     Contents = config_userportal_all
                // });
                // context.Settings.Add(new Setting
                // {
                //     Application = app_global_igaming_boss,
                //     Environment = envProduction,
                //     Contents = config_userportal_production
                // });
                // context.Settings.Add(new Setting
                // {
                //     Application = app_global_igaming_boss,
                //     Environment = envStaging,
                //     Contents = config_userportal_stage
                // });
                // context.Settings.Add(new Setting
                // {
                //     Application = app_global_igaming_boss,
                //     Environment = envDevelopment,
                //     Contents = config_userportal_development
                // });
                // context.Settings.Add(new Setting
                // {
                //     Application = app_global_igaming_boss,
                //     Environment = envDevelopment_2800,
                //     Contents = config_userportal_developmentJose
                // });
                // context.Settings.Add(new Setting()
                // {
                //     Application = app_global_igaming_boss_bonus,
                //     Environment = envDevelopment,
                //     Contents = config_userportal__dataintegration_development
                // });
                // context.SaveChanges();
            }          
           
        }
    }
}
