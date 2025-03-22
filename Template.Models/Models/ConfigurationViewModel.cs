namespace Template.Models.Models
{
    public class ConfigurationViewModel
    {
        public ProductType SelectedProductType { get; set; }
        public List<ConfigurationOption> Options { get; internal set; }
        public string SelectedField { get; internal set; }
        public char? SelectedLetter { get; internal set; }
    }
}
