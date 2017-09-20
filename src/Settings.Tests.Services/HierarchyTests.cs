using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Settings.Common.Domain;
using Settings.Services;

namespace Settings.Tests.Services
{
    [TestClass]
    public class HierarchyTests
    {
        [TestMethod]
        public void ConvertToTreeTest()
        {
            var h = new HierarchyHelper();

            var env = new Environment
            {
                LeftWeight = 1,
                RightWeight = 8,
                Name = "Top",
                Parent = null,
                Children = new List<Environment>()
            };

                    var envChild1Level1 = new Environment
                    {
                        LeftWeight = 2,
                        RightWeight = 3,
                        Name = "Level2Item1",
                        Parent = env,
                        Children = new List<Environment>()
                    };
                    var envChild2Level1 = new Environment
                    {
                        LeftWeight = 4,
                        RightWeight = 7,
                        Name = "Level2Item1",
                        Parent = env,
                        Children = new List<Environment>()
                    };
                            var envChild2Level2 = new Environment
                            {
                                LeftWeight = 5,
                                RightWeight = 6,
                                Name = "Level2Item1",
                                Parent = envChild2Level1,
                                Children = new List<Environment>()
                            };

            envChild2Level1.Children.Add(envChild2Level2);
            env.Children.Add(envChild1Level1);
            env.Children.Add(envChild2Level1);

            var tree = h.GetHierarchicalTree(env);

            Assert.AreEqual(2, tree.Children.Count);
            Assert.AreEqual(0, tree.Children.First().Children.Count);
            Assert.AreEqual(1, tree.Children.Skip(1).First().Children.Count);
        }
    }
}
