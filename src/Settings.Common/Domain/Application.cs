
using System.Collections.Generic;
using Settings.Common.Models;

namespace Settings.Common.Domain
{
    public class Application : IHierarchicalItem<Application>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Application Parent { get; set; }
        public virtual ICollection<Application> Children { get; set; }
        public int? ParentId { get; set; }
        public int HierarchyId { get; set; }

        public Application()
        {
            Children = new List<Application>();
        }
    }
}
