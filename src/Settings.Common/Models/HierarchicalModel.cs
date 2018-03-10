using System.Collections.Generic;
using System.Linq;

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

        public PermissionsAggregateModel AggregatePermissions { get; set;}

        public IEnumerable<int> GetDescendantIds() {
            return GetIdsOfSelfAndAllDescendants()
                .Where( x => x != this.Id);
        }

        public IEnumerable<int> GetAncestorIds() {
            return GetIdsOfSelfAndAncestors()
                .Where( x => x != this.Id);
        }
        public IEnumerable<int> GetIdsOfSelfAndAllDescendants(){
            return GetIdsOfSelfAndAllDescendants(this);
        }

        public IEnumerable<int> GetIdsOfSelfAndAncestors() {
            return GetIdsOfSelfAndAncestors(this);
        }

        public IEnumerable<HierarchicalModel> FlattenAncestors() { 
            return FlattenAncestors(this);
        }

        public IEnumerable<HierarchicalModel> FlattenChildren() {
            return FlattenChildren(this);
        }
        private IEnumerable<int> GetIdsOfSelfAndAllDescendants(HierarchicalModel model) {
            var ids = new List<int>{ model.Id };
            var hasMoreChildren = model.Children.Count > 0;

            while(hasMoreChildren == true) {
                foreach(var child in model.Children) {
                    ids.AddRange(GetIdsOfSelfAndAllDescendants(child));
                }
                hasMoreChildren = false;
            }
            return ids;
        }

        private IEnumerable<int> GetIdsOfSelfAndAncestors(HierarchicalModel model) {
            var ids = new List<int>{ model.Id };
            var hasParent = model.Parent != null;
            if (!hasParent) {
                return ids;
            }
            ids.AddRange(GetIdsOfSelfAndAncestors(model.Parent));
            return ids;
        }

        private IEnumerable<HierarchicalModel> FlattenAncestors(HierarchicalModel model) {
            var models = new List<HierarchicalModel>{ model };
            if (model.Parent != null) {
                models.AddRange(FlattenAncestors(model.Parent));
            }
            return models;
        }

        private IEnumerable<HierarchicalModel> FlattenChildren(HierarchicalModel model) {
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