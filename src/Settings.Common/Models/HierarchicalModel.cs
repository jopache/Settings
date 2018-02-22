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

        public List<int> RecursivelyGetIdOfSelfAndAllDescendants(HierarchicalModel model) {
            var ids = new List<int>{ model.Id };
            var hasMoreChildren = model.Children.Count > 0;

            while(hasMoreChildren == true) {
                foreach(var child in model.Children) {
                    ids.AddRange(RecursivelyGetIdOfSelfAndAllDescendants(child));
                }
                hasMoreChildren = false;
            }
            return ids;
        }

        public List<int> RecursivelyGetIdOfSelfAndAllAncestors(HierarchicalModel model) {

        }

        
    }
}