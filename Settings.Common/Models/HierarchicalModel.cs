using System.Collections.Generic;

namespace Settings.Common.Models
{
    public class HierarchicalModel : IHierarchicalItem<HierarchicalModel>
    {
        public int Id { get; set; }
        public int LeftWeight { get; set; }
        public int RightWeight { get; set; }
        public ICollection<HierarchicalModel> Children { get; set; }
        public string Name { get; set; }
    }
}