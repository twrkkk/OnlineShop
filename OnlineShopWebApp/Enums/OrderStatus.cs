using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "В процессе")]
        Processed,
        [Display(Name = "В доставке")]
        Delivering,
        [Display(Name = "Доставлен")]
        Delivered,
        [Display(Name = "Отменён")]
        Canceled
    }
}
