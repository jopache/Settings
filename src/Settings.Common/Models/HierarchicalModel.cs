using System.Collections.Generic;

namespace Settings.Common.Models
{
    public class HierarchicalModel : IHierarchicalItem<HierarchicalModel>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public ICollection<HierarchicalModel> Children { get; set; }
        public HierarchicalModel Parent { get; set; }
        public string Name { get; set; }

        public int Depth { get; set; }

        public static List<int> GetIdOfSelfAndAllDescendants(HierarchicalModel model) {
            var ids = new List<int>{ model.Id };
            var hasMoreChildren = model.Children.Count > 0;

            while(hasMoreChildren == true) {
                foreach(var child in model.Children) {
                    ids.AddRange(GetIdOfSelfAndAllDescendants(child));
                }
                hasMoreChildren = false;
            }
            return ids;
        }

        public static List<int> GetIdOfSelfAndAncestors(HierarchicalModel model) {
            var ids = new List<int>{ model.Id };
            
            var hasParent = model.Parent != null;

            if (!hasParent) {
                return ids;
            }

            ids.AddRange(GetIdOfSelfAndAncestors(model.Parent));
            return ids;
        }

        public static List<HierarchicalModel> FlattenAncestors(HierarchicalModel model){
            var models = new List<HierarchicalModel>{ model };
            if (model.Parent != null) {
                models.AddRange(FlattenAncestors(model.Parent));
            }
            return models;
        }

        public static List<HierarchicalModel> FlattenChildren(HierarchicalModel model){
            var models = new List<HierarchicalModel>{ model };
            if (model.Children.Count > 0) {
                foreach (var child in model.Children) {
                    models.AddRange(FlattenChildren(child));
                }
            }
            return models;
        }
    }
}