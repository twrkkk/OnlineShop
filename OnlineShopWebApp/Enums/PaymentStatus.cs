using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Не оплачен")]
        NotPaid,
        [Display(Name = "Оплачен")]
        Paid
    }
}
