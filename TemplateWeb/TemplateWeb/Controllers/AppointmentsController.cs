using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Models.Models;
using Template.Services.Services;

namespace TemplateWeb.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AppointmentsController(AppointmentsService appointmentsService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SaveAppointment(Appointment appointment)
    {
        await appointmentsService.SaveAppointmentAsync(appointment);
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateAppointment(Appointment appointment)
    {
        await appointmentsService.UpdateAppointmentAsync(appointment);
        return Ok();
    }

    [HttpGet("{appointmentId}")]
    public async Task<Appointment> GetAppointment(int appointmentId) =>
        await appointmentsService.GetAppointmentAsync(appointmentId);
    
    [HttpGet("{appointmentId}/order-pdf")]
    public async Task<ActionResult> GetOrderPdf(int appointmentId)
    {
        var appointment = await appointmentsService.GetAppointmentAsync(appointmentId);
        var pdfBytes = OrderPdfGenerator.GenerateOrderPdf(appointment);
        
        return File(pdfBytes, "application/pdf", "appointment.pdf");
    }
    
    [HttpGet("calendar")]
    public async Task<Calendar> GetAppointmentsCalendar([FromQuery]DateTime fromDate, [FromQuery]DateTime toDate) =>
        await appointmentsService.GetAppointmentsCalendar(fromDate, toDate);

    [HttpDelete("{appointmentId}/quote-lines/{quoteLineId}")]
    public async Task<ActionResult> DeleteQuoteLine(int appointmentId, int quoteLineId)
    {
        await appointmentsService.DeleteQuoteLineAsync(quoteLineId);

        return Ok();
    }


}