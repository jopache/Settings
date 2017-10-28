using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Settings.Common.Domain;
using Settings.Services;
using Settings.Common.Models;

namespace Settings.Tests.Services
{
    [TestClass]
    public class HierarchyTests
    {
        [TestMethod]
        public void ConvertToTreeTest()
        {
            var h = new HierarchyHelper();


            var env = GetSampleEnvironmentTree();

            var tree = h.GetHierarchicalTree(env);

            Assert.AreEqual(2, tree.Children.Count);
            Assert.AreEqual(0, tree.Children.First().Children.Count);
            Assert.AreEqual(1, tree.Children.Skip(1).First().Children.Count);
        }

        private Environment GetSampleEnvironmentTree()
        {
                                        var env = new Environment
                                        {
                                            LeftWeight = 1,
                                            RightWeight = 8,
                                            Name = "Top",
                                            Parent = null,
                                            Children = new List<Environment>()
                                        };
                                               //
                                               //
                                               //===============================
                                               //                              //
                         var envChild1Level1 = new Environment                 //
                         {                                                     //
                             LeftWeight = 2,                                   //
                             RightWeight = 3,                                  //
                             Name = "Level2Item1",                             //
                             Parent = env,                                     //
                             Children = new List<Environment>()                //
                         };                                                    //
                                                                               //
                                                                      var envChild2Level1 = new Environment
                                                                      {
                                                                          LeftWeight = 4,
                                                                          RightWeight = 7,
                                                                          Name = "Level2Item1",
                                                                          Parent = env,
                                                                          Children = new List<Environment>()
                                                                      };
                                                                                //
                                                                                //
                                                                                //
                                                                                //
                                                                                //
                                                                                //
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

            return env;
        }

        [TestMethod]
        public void ValidateWeightsNow()
        {
            var env = GetSampleEnvironmentTree();

            var left = 1;
            var isValid = ValidateWeights(env, ref left);

            Assert.IsTrue(isValid);
        }

        public bool ValidateWeights<T>(IHierarchicalItem<T> topItem, ref int currentLeftWeight)
            where T : IHierarchicalItem<T>
        {
            var right = currentLeftWeight + 1;
            return ValidateWeights(topItem, ref currentLeftWeight, ref right);
        }

        private bool ValidateWeights<T>(IHierarchicalItem<T> topItem, ref int currentLeftWeight, ref int currentRightWeight)
            where T : IHierarchicalItem<T>
        {
            if(topItem.LeftWeight != currentLeftWeight)
            {
                return false;
            }

            var children = topItem.Children.OrderBy(x => x.LeftWeight);

            if (!children.Any())
            {
                //currentRightWeight++;
            }

            foreach(var child in children)
            {
                currentLeftWeight++;
                currentRightWeight++;
                if (!ValidateWeights(child, ref currentLeftWeight, ref currentRightWeight)){
                    return false;
                }
                currentLeftWeight++;
                currentRightWeight++;

            }

            if(topItem.RightWeight != currentRightWeight)
            {
                return false;
            }

            return true;
        }
    }
}
