namespace Template.Models.Models
{
    public class CalendarWeek
    {
        public String Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CalendarDay> Days { get; set; }
    }
}
