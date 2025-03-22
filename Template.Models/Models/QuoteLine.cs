using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Models.Models;

public class QuoteLine
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; }
    public int AppointmentId { get; set; }
    public string RoomRef { get; set; }
    public string Width { get; set; }
    public string Drop { get; set; }
    public string InstallationHeight { get; set; }
    public string? SizeType { get; set; }
    public string? HeadrailType { get; set; }
    public string? ControlType { get; set; }
    public string? ControlSide { get; set; }
    public string? Stacking { get; set; }
    public string? Brackets { get; set; }
    public string? WeightsChain { get; set; }
    public string? Fabric { get; set; }
    public int? NumberOfSlats { get; set; }
    public bool? SampleRetained { get; set; }
    public string? System { get; set; }
    public string? Controls { get; set; }
    public string? Lining { get; set; }
    public string? BeadDepth { get; set; }
    public string? Profile { get; set; }
    public string? Hardware { get; set; }
    public string? Tape { get; set; }
    public string? SlatSize { get; set; }
    public string? FittingBrackets { get; set; }
    public string? Control { get; set; }
    public string? SlatColour { get; set; }
    public string? Draw { get; set; }
    public string? UnderGuarantee { get; set; }
    public string? Description { get; set; }
    public string? TrackColour { get; set; }
    public string? TrackWidth { get; set; }
    public string? TracksRequired { get; set; }
    public string? Finial { get; set; }
    public string? PoleColour { get; set; }
    public string? PoleSize { get; set; }
    public string? WindowType { get; set; }
    public string? WindowSizeRef { get; set; }
    public string? HooksRequired { get; set; }
    public string? RingsRequired { get; set; }
    public string? Slatting { get; set; }
    public string? Braid { get; set; }
    public string? Endcaps { get; set; }
    public string? Scallop { get; set; }
    public string? HoldDownBrackets { get; set; }
    
    public string Measurements => $"{Width} wide x {Drop} drop";
}