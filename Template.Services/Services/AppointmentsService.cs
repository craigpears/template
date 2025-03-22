using Microsoft.EntityFrameworkCore;
using Template.Data;
using Template.Models.Extensions;
using Template.Models.Models;

namespace Template.Services.Services;

public class AppointmentsService(ApplicationDbContext context)
{
    public async Task SaveAppointmentAsync(Appointment appointment)
    {
        var appointmentEntity = appointment.MapToEntity();
        
        context.Add(appointmentEntity);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        var appointmentEntity = appointment.MapToEntity();
        
        context.Attach(appointmentEntity).State = EntityState.Modified;
        foreach (var quoteLine in appointment.QuoteLines)
        {
            context.Attach(quoteLine).State = EntityState.Modified;
        }
        
        await context.SaveChangesAsync();
    }

    public async Task<Appointment> GetAppointmentAsync(int appointmentId)
    {
        var appointmentEntity = await context.Appointments
            .Include(x => x.QuoteLines)
            .SingleAsync(x => x.Id == appointmentId);

        return appointmentEntity.MapToModel();
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(DateTime fromDate, DateTime toDate)
    {
        var appointmentEntities = await context.Appointments
            .Where(x => x.AppointmentDate >= fromDate && x.AppointmentDate <= toDate).ToListAsync();

        return appointmentEntities.Select(x => x.MapToModel()).ToList();
    }

    public async Task<Calendar> GetAppointmentsCalendar(DateTime fromDate, DateTime toDate)
    {
        var appointments = await GetAppointmentsAsync(fromDate, toDate);

        var weeks = GetCalendarWeeks(fromDate, toDate);
        var allTimeslots = weeks.SelectMany(x => x.Days).SelectMany(x => x.Timeslots).ToList();
        foreach (var timeslot in allTimeslots)
        {
            timeslot.Appointments = appointments.Where(a => a.AppointmentDate == timeslot.DateTime)
                .ToList();
        }

        var calendar = new Calendar
        {
            Weeks = weeks
        };

        return calendar;
    }
    
    public async Task DeleteQuoteLineAsync(int quoteLineId)
    {
        var quoteLine = await context.QuoteLines.SingleAsync(x => x.Id == quoteLineId);
        context.QuoteLines.Remove(quoteLine);
        await context.SaveChangesAsync();
    } 
    
    private static List<CalendarWeek> GetCalendarWeeks(DateTime fromDate, DateTime toDate)
    {
        DateTime startOfWeek = fromDate.GetStartOfWeek();

        // Generate the list of CalendarWeeks
        List<CalendarWeek> weeks = new List<CalendarWeek>();
        while (startOfWeek < toDate)
        {
            DateTime endOfWeek = startOfWeek.GetEndOfWeek();

            CalendarWeek week = new CalendarWeek
            {
                StartDate = startOfWeek,
                EndDate = endOfWeek > toDate ? toDate : endOfWeek,
                Days = new List<CalendarDay> {}
            };

            // Filling in the Days property
            for(int i = 0; i < 6; i++)
            {
                DateTime dayDate = startOfWeek.AddDays(i);
                if (dayDate <= toDate)
                {
                    var newDay = new CalendarDay
                    {
                        Name = dayDate.DayOfWeek.ToString(),
                        Date = dayDate,
                        Timeslots = [
                            new () { Name = "9-11", Time = new TimeSpan(9,0,0)  },
                            new () { Name = "11-1", Time = new TimeSpan(11,0,0) },
                            new () { Name = "2-4",  Time = new TimeSpan(14,0,0) },
                            new () { Name = "4-6",  Time = new TimeSpan(16,0,0) }
                        ]
                    };
                    
                    newDay.Timeslots.ForEach(ts => ts.DateTime = dayDate.Add(ts.Time));
                    
                    week.Days.Add(newDay);
                }
            }

            weeks.Add(week);

            startOfWeek = startOfWeek.AddWeek();
        }

        return weeks;
    }
}