using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entites.BasketModule
{
    // msh h5lehom ywrso mn el baseEntity 34an hayt3amlo m3 DB tania 34an h7tag a3ml lehom caching
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
