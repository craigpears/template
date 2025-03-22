namespace Template.Models.Models;

public class Appointment
{
    public int Id { get; set; }
    public CustomerType CustomerType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime AppointmentDate { get; set; }
    public Title Title { get; set; }
    public string? Surname { get; set; }
    public string? CompanyName { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Town { get; set; }
    public string? County { get; set; }
    public string? Postcode { get; set; }
    public string? Telephone { get; set; }
    public string? AlternativeTelephone { get; set; }
    public string? Email { get; set; }
    public bool DeliveryAddressSameAsOrderAddress { get; set; }
    public string? DeliveryAddress1 { get; set; }
    public string? DeliveryAddress2 { get; set; }
    public string? DeliveryTown { get; set; }
    public string? DeliveryCounty { get; set; }
    public string? DeliveryPostcode { get; set; }
    public string? SpecialInstructions { get; set; }
    public double? PriceQuoted { get; set; }

    public List<QuoteLine> QuoteLines { get; set; } = new List<QuoteLine>();

    public bool IsDeleted { get; set; }

    public string DisplayName => $"{Title} {Surname}";
}