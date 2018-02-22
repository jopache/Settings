
using System.Collections.Generic;
using Settings.Common.Domain;
using Settings.Common.Models;
using System.Linq;

namespace Settings.Services
{
    public class HierarchyHelper
    {
        public HierarchicalModel GetHierarchicalTree<T>(IHierarchicalItem<T> node) 
            where T : IHierarchicalItem<T>
        {
            var rootNode = new HierarchicalModel()
            {
                Id = node.Id,
                ParentId = node.ParentId,
                Name = node.Name,
                Children = new List<HierarchicalModel>()
            };

            foreach (var child in node.Children)
            {
                rootNode.Children.Add(GetHierarchicalTree<T>(child));
            }

            return rootNode;
        }
    }
}