using Template.Data.Entities;

namespace Template.Services;

public static class MappingProfile
{
    public static Models.Models.Appointment MapToModel(this Appointment appointment)
    {
        return new Models.Models.Appointment
        {
            Id = appointment.Id,
            CustomerType = appointment.CustomerType,
            OrderStatus = appointment.OrderStatus,
            AppointmentDate = appointment.AppointmentDate,
            Title = appointment.Title,
            Surname = appointment.Surname,
            CompanyName = appointment.CompanyName,
            Address1 = appointment.Address1,
            Address2 = appointment.Address2,
            Town = appointment.Town,
            County = appointment.County,
            Postcode = appointment.Postcode,
            Telephone = appointment.Telephone,
            AlternativeTelephone = appointment.AlternativeTelephone,
            Email = appointment.Email,
            DeliveryAddressSameAsOrderAddress = appointment.DeliveryAddressSameAsOrderAddress,
            DeliveryAddress1 = appointment.DeliveryAddress1,
            DeliveryAddress2 = appointment.DeliveryAddress2,
            DeliveryTown = appointment.DeliveryTown,
            DeliveryCounty = appointment.DeliveryCounty,
            DeliveryPostcode = appointment.DeliveryPostcode,
            SpecialInstructions = appointment.SpecialInstructions,
            PriceQuoted = appointment.PriceQuoted,
            QuoteLines = appointment.QuoteLines.Select(x => x.MapToViewModel()).ToList(),
            IsDeleted = appointment.IsDeleted
        };
    }
    
    public static Appointment MapToEntity(this Models.Models.Appointment appointment)
    {
        return new Appointment
        {
            Id = appointment.Id,
            CustomerType = appointment.CustomerType,
            OrderStatus = appointment.OrderStatus,
            AppointmentDate = appointment.AppointmentDate,
            Title = appointment.Title,
            Surname = appointment.Surname,
            CompanyName = appointment.CompanyName,
            Address1 = appointment.Address1,
            Address2 = appointment.Address2,
            Town = appointment.Town,
            County = appointment.County,
            Postcode = appointment.Postcode,
            Telephone = appointment.Telephone,
            AlternativeTelephone = appointment.AlternativeTelephone,
            Email = appointment.Email,
            DeliveryAddressSameAsOrderAddress = appointment.DeliveryAddressSameAsOrderAddress,
            DeliveryAddress1 = appointment.DeliveryAddress1,
            DeliveryAddress2 = appointment.DeliveryAddress2,
            DeliveryTown = appointment.DeliveryTown,
            DeliveryCounty = appointment.DeliveryCounty,
            DeliveryPostcode = appointment.DeliveryPostcode,
            SpecialInstructions = appointment.SpecialInstructions,
            PriceQuoted = appointment.PriceQuoted,
            QuoteLines = appointment.QuoteLines.Select(x => x.MapToEntity()).ToList(),
            IsDeleted = appointment.IsDeleted
        };
    }
    
    public static Template.Models.Models.QuoteLine MapToViewModel(this QuoteLine quoteLine)
    {
        return new Models.Models.QuoteLine
        {
            Id = quoteLine.Id,
            ProductType = quoteLine.ProductType,
            AppointmentId = quoteLine.AppointmentId,
            RoomRef = quoteLine.RoomRef,
            Width = quoteLine.Width,
            Drop = quoteLine.Drop,
            InstallationHeight = quoteLine.InstallationHeight,
            SizeType = quoteLine.SizeType,
            HeadrailType = quoteLine.HeadrailType,
            ControlType = quoteLine.ControlType,
            ControlSide = quoteLine.ControlSide,
            Stacking = quoteLine.Stacking,
            Brackets = quoteLine.Brackets,
            WeightsChain = quoteLine.WeightsChain,
            Fabric = quoteLine.Fabric,
            NumberOfSlats = quoteLine.NumberOfSlats,
            SampleRetained = quoteLine.SampleRetained,
            System = quoteLine.System,
            Controls = quoteLine.Controls,
            Lining = quoteLine.Lining,
            BeadDepth = quoteLine.BeadDepth,
            Profile = quoteLine.Profile,
            Hardware = quoteLine.Hardware,
            Tape = quoteLine.Tape,
            SlatSize = quoteLine.SlatSize,
            FittingBrackets = quoteLine.FittingBrackets,
            Control = quoteLine.Control,
            SlatColour = quoteLine.SlatColour,
            Draw = quoteLine.Draw,
            UnderGuarantee = quoteLine.UnderGuarantee,
            Description = quoteLine.Description,
            TrackColour = quoteLine.TrackColour,
            TrackWidth = quoteLine.TrackWidth,
            TracksRequired = quoteLine.TracksRequired,
            Finial = quoteLine.Finial,
            PoleColour = quoteLine.PoleColour,
            PoleSize = quoteLine.PoleSize,
            WindowType = quoteLine.WindowType,
            WindowSizeRef = quoteLine.WindowSizeRef,
            HooksRequired = quoteLine.HooksRequired,
            RingsRequired = quoteLine.RingsRequired,
            Slatting = quoteLine.Slatting,
            Braid = quoteLine.Braid,
            Endcaps = quoteLine.Endcaps,
            Scallop = quoteLine.Scallop,
            HoldDownBrackets = quoteLine.HoldDownBrackets
        };
    }
    
    public static QuoteLine MapToEntity(this Template.Models.Models.QuoteLine quoteLine)
    {
        return new QuoteLine
        {
            Id = quoteLine.Id,
            ProductType = quoteLine.ProductType,
            AppointmentId = quoteLine.AppointmentId,
            RoomRef = quoteLine.RoomRef,
            Width = quoteLine.Width,
            Drop = quoteLine.Drop,
            InstallationHeight = quoteLine.InstallationHeight,
            SizeType = quoteLine.SizeType,
            HeadrailType = quoteLine.HeadrailType,
            ControlType = quoteLine.ControlType,
            ControlSide = quoteLine.ControlSide,
            Stacking = quoteLine.Stacking,
            Brackets = quoteLine.Brackets,
            WeightsChain = quoteLine.WeightsChain,
            Fabric = quoteLine.Fabric,
            NumberOfSlats = quoteLine.NumberOfSlats,
            SampleRetained = quoteLine.SampleRetained,
            System = quoteLine.System,
            Controls = quoteLine.Controls,
            Lining = quoteLine.Lining,
            BeadDepth = quoteLine.BeadDepth,
            Profile = quoteLine.Profile,
            Hardware = quoteLine.Hardware,
            Tape = quoteLine.Tape,
            SlatSize = quoteLine.SlatSize,
            FittingBrackets = quoteLine.FittingBrackets,
            Control = quoteLine.Control,
            SlatColour = quoteLine.SlatColour,
            Draw = quoteLine.Draw,
            UnderGuarantee = quoteLine.UnderGuarantee,
            Description = quoteLine.Description,
            TrackColour = quoteLine.TrackColour,
            TrackWidth = quoteLine.TrackWidth,
            TracksRequired = quoteLine.TracksRequired,
            Finial = quoteLine.Finial,
            PoleColour = quoteLine.PoleColour,
            PoleSize = quoteLine.PoleSize,
            WindowType = quoteLine.WindowType,
            WindowSizeRef = quoteLine.WindowSizeRef,
            HooksRequired = quoteLine.HooksRequired,
            RingsRequired = quoteLine.RingsRequired,
            Slatting = quoteLine.Slatting,
            Braid = quoteLine.Braid,
            Endcaps = quoteLine.Endcaps,
            Scallop = quoteLine.Scallop,
            HoldDownBrackets = quoteLine.HoldDownBrackets
        };
    }
}