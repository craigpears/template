namespace Template.Models.Models
{
    public class CalendarTimeslot
    {
        public String Name { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Time { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
