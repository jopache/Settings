using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Settings.Common.Domain;
using Settings.Services;
using Settings.Common.Models;
using Microsoft.EntityFrameworkCore;
using Settings.DataAccess;

namespace Settings.Tests.Services
{
    [TestClass]
    public class HierarchyTests
    {
       public SettingsDbContext _settingsContext {
           get {
               var options = new DbContextOptionsBuilder<SettingsDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new BloggingContext(options))
            {
                var service = new BlogService(context);
                service.Add("http://sample.com");
            }

           }
       }
    }
}
