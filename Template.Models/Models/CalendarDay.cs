namespace Template.Models.Models
{
    public class CalendarDay
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public List<CalendarTimeslot> Timeslots { get; set; }
        public bool AnyAppointments => Timeslots.Any(ts => ts.Appointments.Any());
    }
}
