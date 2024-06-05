using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string BareCode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
}