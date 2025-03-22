namespace Template.Models.Models;

public class ConfigurationOption
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; }
    public string FieldName { get; set; }
    public string Name { get; set; }
}