using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class IndexViewModel<TEntity>
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
