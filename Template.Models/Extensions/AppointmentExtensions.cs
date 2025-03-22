using System.Linq.Expressions;
using Template.Models.Models;

namespace Template.Models.Extensions;

public static class AppointmentExtensions
{
    public static ProductTable ToProductTable(this Appointment appointment)
    {
        var productTable = new ProductTable();
        foreach (var productTypeGroup in appointment.QuoteLines.GroupBy(x => x.ProductType))
        {
            var section = new ProductTableSection
            {
                ProductType = productTypeGroup.Key,
                PropertyNames = []
            };
            var applicableHeaders = ProductFieldMap[productTypeGroup.Key].ToList();
            section.PropertyNames.Add(GetPropertyName<QuoteLine, string>(l => l.RoomRef));
            section.PropertyNames.Add(GetPropertyName<QuoteLine, string>(l => l.Measurements));
            var sizeTypePropertyName = GetPropertyName<QuoteLine, string>(l => l.SizeType);
            if(applicableHeaders.Contains(sizeTypePropertyName))
            {
                section.PropertyNames.Add(sizeTypePropertyName);
            }
            section.PropertyNames.Add(GetPropertyName<QuoteLine, string>(l => l.RoomRef));
            section.PropertyNames.AddRange(applicableHeaders.Where(x => x != sizeTypePropertyName));
            
            section.Lines.AddRange(productTypeGroup.ToList());
            
            productTable.Sections.Add(section);
        }

        return productTable;
    }
    
    public static object GetPropertyValue(this QuoteLine line, string propertyName)
    {
        var property = typeof(QuoteLine).GetProperty(propertyName);
        if (property != null)
        {
            return property.GetValue(line, null);
        }

        return null;
    }
    
    private static readonly Dictionary<ProductType, List<string>> ProductFieldMap = new()
    {
        { ProductType.Vertical, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.HeadrailType, 
            l => l.ControlType, 
            l => l.ControlSide, 
            l => l.Stacking, 
            l => l.Brackets, 
            l => l.WeightsChain, 
            l => l.Fabric
        )},
        { ProductType.VerticalSlats, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.NumberOfSlats, 
            l => l.SampleRetained, 
            l => l.WeightsChain, 
            l => l.Fabric
        )},
        { ProductType.VerticalHeadrail, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.HeadrailType, 
            l => l.ControlType, 
            l => l.ControlSide, 
            l => l.Stacking
        )},
        { ProductType.Roller, GetPropertyNames<QuoteLine>(
            l => l.Hardware, 
            l => l.Control, 
            l => l.Brackets, 
            l => l.Scallop, 
            l => l.Braid, 
            l => l.Endcaps, 
            l => l.Fabric
        )},
        { ProductType.AluminiumVenetian, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.Controls, 
            l => l.HoldDownBrackets, 
            l => l.Slatting, 
            l => l.SlatColour
        )},
        { ProductType.WoodenVenetian, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.Slatting, 
            l => l.Controls, 
            l => l.Brackets, 
            l => l.HoldDownBrackets, 
            l => l.SlatColour, 
            l => l.Tape
        )},
        { ProductType.Fauxwood, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.Controls, 
            l => l.Slatting, 
            l => l.HoldDownBrackets, 
            l => l.SlatColour, 
            l => l.Tape
        )},
        { ProductType.Roman, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.Controls, 
            l => l.Lining
        )},
        { ProductType.Pleated, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.System, 
            l => l.Hardware, 
            l => l.Controls, 
            l => l.Brackets, 
            l => l.Fabric
        )},
        { ProductType.IntuVenetian, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.System, 
            l => l.BeadDepth, 
            l => l.Profile, 
            l => l.Hardware, 
            l => l.SlatSize, 
            l => l.Control, 
            l => l.SlatColour
        )},
        { ProductType.IntuPleated, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.BeadDepth, 
            l => l.Profile, 
            l => l.System, 
            l => l.Hardware, 
            l => l.Fabric
        )},
        { ProductType.PerfectFitVenetian, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.System, 
            l => l.Hardware, 
            l => l.Control, 
            l => l.FittingBrackets, 
            l => l.SlatSize, 
            l => l.SlatColour
        )},
        { ProductType.PerfectFitPleated, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.System, 
            l => l.Hardware, 
            l => l.FittingBrackets, 
            l => l.Fabric
        )},
        { ProductType.PerfectFitRoller, GetPropertyNames<QuoteLine>(
            l => l.SizeType, 
            l => l.Hardware, 
            l => l.FittingBrackets, 
            l => l.Fabric
        )},
        { ProductType.VeluxSkylight, GetPropertyNames<QuoteLine>(
            l => l.WindowType, 
            l => l.WindowSizeRef, 
            l => l.Fabric
        )},
        { ProductType.CurtainPole, GetPropertyNames<QuoteLine>(
            l => l.PoleColour, 
            l => l.PoleSize, 
            l => l.RingsRequired, 
            l => l.HooksRequired, 
            l => l.Finial
        )},
        { ProductType.CurtainTrack, GetPropertyNames<QuoteLine>(
            l => l.TrackWidth, 
            l => l.TrackColour, 
            l => l.Draw, 
            l => l.HooksRequired
        )},
        { ProductType.RepairSpecialRequest, GetPropertyNames<QuoteLine>(
            l => l.UnderGuarantee, 
            l => l.Description
        )}
    };
    
    private static bool IsFieldApplicable(ProductType productType, string fieldName)
    {
        return ProductFieldMap.TryGetValue(productType, out var applicableFields) && applicableFields.Contains(fieldName);
    }
    
    public static List<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] expressions)
    {
        return expressions.Select(GetPropertyName).ToList();
    }

    public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> property)
    {
        if (property.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        if (property.Body is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
            if(memberExpression != null)
            {
                return memberExpression.Member.Name;
            }
        }

        throw new ArgumentException("Invalid property expression", nameof(property));
    }
}

public class ProductTable
{
    public List<ProductTableSection> Sections { get; set; } = [];
}

public class ProductTableSection
{
    public ProductType ProductType { get; set; }
    public List<string> PropertyNames = [];
    public List<QuoteLine> Lines = []; 
}