namespace Template.Models.Models
{
    public class EditQuoteLineViewModel
    {
        public Boolean EditAll { get; set; }
        public QuoteLine QuoteLine { get; set; }
        public List<ConfigurationOption> ConfigurationOptions { get; set; }
    }
}
