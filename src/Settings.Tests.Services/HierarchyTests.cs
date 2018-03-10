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
        public SettingsDbContext CreateSettingsContext() {
           var optionsBuilder = new DbContextOptionsBuilder<SettingsDbContext>();
           optionsBuilder.UseInMemoryDatabase("Settings");
           return new SettingsDbContext(optionsBuilder.Options);
        }
        
        public void Lol() {
            using (var context = CreateSettingsContext()) {

            }
        }
    }
}

