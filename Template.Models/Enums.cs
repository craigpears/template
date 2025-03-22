using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Humanizer;

namespace Template.Models
{
    public enum ProductType
    {
        [Display(Name = "Vertical")]
        Vertical,
        [Display(Name = "Vertical Slats")]
        VerticalSlats,
        [Display(Name = "Vertical Headrail Only")]
        VerticalHeadrail,
        [Display(Name = "Roller")]
        Roller,
        [Display(Name = "Aluminium Venetian")]
        AluminiumVenetian,
        [Display(Name = "Wooden Venetian")]
        WoodenVenetian,
        [Display(Name = "Fauxwood")]
        Fauxwood,
        [Display(Name = "Roman")]
        Roman,
        [Display(Name = "Pleated")]
        Pleated,
        [Display(Name = "INTU Venetian")]
        IntuVenetian,
        [Display(Name = "INTU Pleated")]
        IntuPleated,
        [Display(Name = "Perfect Fit Venetian")]
        PerfectFitVenetian,
        [Display(Name = "Perfect Fit Pleated")]
        PerfectFitPleated,
        [Display(Name = "Perfect Fit Roller")]
        PerfectFitRoller,
        [Display(Name = "Velux / Skylight")]
        VeluxSkylight,
        [Display(Name = "Curtain Pole")]
        CurtainPole,
        [Display(Name = "Curtain Track")]
        CurtainTrack,
        [Display(Name = "Repair / Special Request")]
        RepairSpecialRequest
    }

    public enum PaymentMethod
    {
        Cash,
        Cheque,
        Card,
        BACS,
        Paypal
    }

    public enum PaymentType
    {
        [Display(Name = "Deposit")]
        Deposit,
        [Display(Name="Part Payment")]
        PartPayment,
        [Display(Name="Final Balance")]
        FinalBalance
    }

    public enum CustomerType
    {
        [Display(Name = "Retail - SWB")]
        RetailSWB,
        [Display(Name = "Retail - Thistle")]
        RetailThistle,
        [Display(Name = "Online")]
        Online,
        [Display(Name = "Trade - SWB")]
        TradeSWB
    }

    public enum OrderStatus
    {
        [Display(Name = "work-in-progress")]
        WorkInProgress,
        [Display(Name = "part-ready")]
        PartReady,
        [Display(Name = "ready-to-be-fit")]
        ReadyToBeFit
    }

    public enum OrdersFilter
    {
        [Display(Name = "All Ordered")]
        Ordered,
        [Display(Name = "Ready To Fit")]
        ReadyToFit,
        [Display(Name = "Work In Progress")]
        WorkInProgress,
        [Display(Name = "Not Seen By Office")]
        NotSeenByOffice,
        [Display(Name = "Not Seen By Factory")]
        NotSeenByFactory,
        [Display(Name = "First Payment Pending")]
        NotMadeDeposit,
        [Display(Name = "Final Balance Pending")]
        NotFullyPaid,
        [Display(Name = "Order Pending")]
        Pending
    }

    public enum Title
    {
        [Display(Name ="Mr")]
        Mr,
        [Display(Name = "Mrs")]
        Mrs,
        [Display(Name = "Miss")]
        Miss,
        [Display(Name = "Ms")]
        Ms,
        [Display(Name = "Mr & Mrs")]
        MrAndMrs
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute != null ? displayAttribute.GetName() : enumValue.ToString().Humanize();
        }
    }
}