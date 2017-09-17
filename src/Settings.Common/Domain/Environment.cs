using System.Collections;
using System.Collections.Generic;
using Settings.Common.Models;

namespace Settings.Common.Domain
{
    public class Environment : IHierarchicalItem<Environment>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Environment Parent { get; set; }
        public virtual ICollection<Environment> Children { get; set; }
        public int? ParentId { get; set; }
        public int LeftWeight { get; set; }
        public int RightWeight { get; set; }

        public Environment()
        {
            Children = new List<Environment>();
        }
    }
}
