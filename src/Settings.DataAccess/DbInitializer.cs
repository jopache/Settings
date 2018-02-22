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
                 var appGlobal = new Application
                 {
                     Name = "Global"
                 };

                         var app_global_engineering = new Application
                         {
                             Name = "Engineering",
                             Parent = appGlobal
                         };

                                var app_global_engineering_userportal = new Application
                                {
                                    Name = "UserPortal",
                                    Parent = app_global_engineering
                                };

                                         var app_global_engineering_userportal_dataintegration = new Application
                                         {
                                             Name = "DataIntegration",
                                             Parent = app_global_engineering_userportal
                                         };

                                var app_global_engineering_paymentsapi = new Application
                                {
                                    Name = "PaymentsApi",
                                    Parent = app_global_engineering
                                };

                context.Applications.Add(appGlobal);
                context.Applications.Add(app_global_engineering);
                context.Applications.Add(app_global_engineering_userportal);
                context.Applications.Add(app_global_engineering_userportal_dataintegration);
                context.Applications.Add(app_global_engineering_paymentsapi);
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

                                 var envDevelopmentJose = new Environment
                                 {
                                     Name = "Development-Jose",
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
                context.Environments.Add(envDevelopmentJose);
                context.Environments.Add(envStaging);

                context.SaveChanges();


                var config_global_all = JsonConvert.SerializeObject(new
                {
                    CompanyName = "TestCompanyName",
                    Website = "www.testcompanyname.com",
                    ContactNumber = "1-800-222-2222"
                });
                
                   var config_userportal_all = JsonConvert.SerializeObject(new {
                       PasswordExpirationDays = 90,                         //add
                       ContactNumber = "1-800-222-1111",                    //override
                       AdminRoles = new JArray("LocalAdmin", "SuperAdmin")  //add
                   });
                
                       var config_userportal_production = JsonConvert.SerializeObject(new
                       {
                           DbConnection = "production.dbserver;database=userportal",    //add
                           Website = "userportal.testcompanyname.com",                  //override
                       });
                
                       var config_userportal_development = JsonConvert.SerializeObject(new
                       {
                           DbConnection = "development.dbserver;database=userportal",   //add
                           Website = "development.userportal.testcompanyname.com"       //override
                       });
                
                            var config_userportal_developmentJose = JsonConvert.SerializeObject(new
                           {
                               DbConnection = "localhost;database=userportal",  //add
                               Website = "localhost:8080",                      //override
                               InDevelopmentFeatureConfig = "test_value"        //add
                           });
                
                        var config_userportal_stage = JsonConvert.SerializeObject(new
                        {
                            DbConnection = "stage.dbserver;database=userportal", //add
                            Website = "stage.userportal.testcompanyname.com"     //override
                        });
                
                        //Note that this is same environment as above,
                        //but now data integration application
                        var config_userportal__dataintegration_development = 
                        JsonConvert.SerializeObject(new
                        {
                            MessageQueueConnection = "RabbitMq.development"      //add
                        });

                           

                context.Settings.Add(new Setting
                {
                    Application = appGlobal,
                    Environment = envAll,
                    Contents = config_global_all
                });
                context.Settings.Add(new Setting
                {
                    Application = app_global_engineering_userportal,
                    Environment = envAll,
                    Contents = config_userportal_all
                });
                context.Settings.Add(new Setting
                {
                    Application = app_global_engineering_userportal,
                    Environment = envProduction,
                    Contents = config_userportal_production
                });
                context.Settings.Add(new Setting
                {
                    Application = app_global_engineering_userportal,
                    Environment = envStaging,
                    Contents = config_userportal_stage
                });
                context.Settings.Add(new Setting
                {
                    Application = app_global_engineering_userportal,
                    Environment = envDevelopment,
                    Contents = config_userportal_development
                });
                context.Settings.Add(new Setting
                {
                    Application = app_global_engineering_userportal,
                    Environment = envDevelopmentJose,
                    Contents = config_userportal_developmentJose
                });
                context.Settings.Add(new Setting()
                {
                    Application = app_global_engineering_userportal_dataintegration,
                    Environment = envDevelopment,
                    Contents = config_userportal__dataintegration_development
                });
                context.SaveChanges();
            }          
           
        }
    }
}
