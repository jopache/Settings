using System.Linq;
using Newtonsoft.Json;
using Settings.Common.Domain;

namespace Settings.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(SettingsDbContext context)
        {
            //TODO: Need to add some contraints here and/or make sure they are created
            //1. Unique on application name and environment name
            context.Database.EnsureCreated();

            if(!context.Applications.Any())
            {
                 var appGlobal = new Application
                 {
                     Name = "Global",
                     LeftWeight = 1,
                     RightWeight = 10
                 };

                         var appLevel1 = new Application
                         {
                             Name = "level1",
                             LeftWeight = 2,
                             RightWeight = 9,
                             Parent = appGlobal
                         };

                                var appLevel2a = new Application
                                {
                                    Name = "level2a",
                                    LeftWeight = 3,
                                    RightWeight = 6,
                                    Parent = appLevel1
                                };

                                         var appLevel2aNested1 = new Application
                                         {
                                             Name = "Level2aNested1",
                                             LeftWeight = 4,
                                             RightWeight = 5,
                                             Parent = appLevel2a
                                         };

                                var appLevel2b = new Application
                                {
                                    Name = "Level2b",
                                    LeftWeight = 7,
                                    RightWeight = 8,
                                    Parent = appLevel1
                                };

                context.Applications.Add(appGlobal);
                context.Applications.Add(appLevel1);
                context.Applications.Add(appLevel2a);
                context.Applications.Add(appLevel2aNested1);
                context.Applications.Add(appLevel2b);
                context.SaveChanges();

                
                  

                var envAll = new Environment
                {
                    Name = "All",
                    LeftWeight = 1,
                    RightWeight = 8
                };

                        var envProduction = new Environment
                        {
                            Name = "Production",
                            LeftWeight = 2,
                            RightWeight = 3,
                            Parent = envAll
                        };

                        var envDevelopment = new Environment
                        {
                            Name = "Development",
                            LeftWeight = 4,
                            RightWeight = 7,
                            Parent = envAll
                        };

                                 var envDevelopmentJose = new Environment
                                 {
                                     Name = "Development-Jose",
                                     LeftWeight = 5,
                                     RightWeight = 6,
                                     Parent = envDevelopment
                                 };

                context.Environments.Add(envAll);
                context.Environments.Add(envProduction);
                context.Environments.Add(envDevelopment);
                context.Environments.Add(envDevelopmentJose);

                context.SaveChanges();


                var configStringGlobalEnvAll = JsonConvert.SerializeObject(new
                {
                    CompanyName = "TestCompanyName",
                    Website = "www.testcompanyname.com",
                    GlobalTimeout = 60                    
                });

                    var configStringLevel1EnvAll = JsonConvert.SerializeObject(new
                    {
                        DbName = "DbNameLevel1EnvAll",
                        Website = "level1.testcompanyname.com",
                        GlobalTimeout = 20,
                        Roles = new Newtonsoft.Json.Linq.JArray("foo")
                    });

                    var configStringLevel1EnvDevelopment = JsonConvert.SerializeObject(new
                    {
                        DbName = "DbNameLevel1EnvDevelopment",
                        Website = "the.dev.url",
                        Roles = new Newtonsoft.Json.Linq.JArray("bar")
                    });

                    var configStringLevel1EnvDevelopmentJose = JsonConvert.SerializeObject(new
                    {
                        Website = "localhost"
                    });

                        var configStringLevel2aNested1EnvDevelopmentJose = JsonConvert.SerializeObject(new
                        {
                            GlobalTimeout = 10
                        });

                context.Settings.Add(new Setting { Application = appGlobal, Environment = envAll, Contents = configStringGlobalEnvAll});
                context.Settings.Add(new Setting { Application = appLevel1, Environment = envAll, Contents = configStringLevel1EnvAll });
                context.Settings.Add(new Setting { Application = appLevel2aNested1, Environment = envDevelopmentJose, Contents = configStringLevel2aNested1EnvDevelopmentJose });
                context.Settings.Add(new Setting { Application = appLevel1, Environment = envDevelopment, Contents = configStringLevel1EnvDevelopment });
                context.Settings.Add(new Setting { Application = appLevel1, Environment = envDevelopmentJose, Contents = configStringLevel1EnvDevelopmentJose });
                context.SaveChanges();
            }          
           
        }
    }
}
