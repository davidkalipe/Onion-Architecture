using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string Fullname { get; set; }
    public string Phonenumber { get; set; }
    public string Password { get; set; }

    public ICollection<Product> Products { get; set; }
}