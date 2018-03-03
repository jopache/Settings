using System.Collections.Generic;

namespace Settings.Common.Models
{
    public interface IHierarchicalItem<T>
    {
        int Id { get; set; }
        int? ParentId { get; set; }
        ICollection<T> Children { get; set; }
        string Name { get; set; }
    }
}