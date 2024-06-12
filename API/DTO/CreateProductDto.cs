namespace API.DTO;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Barecode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
}