using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Exceptions
{
    public abstract class NotFoundException(string Message) : Exception(Message)
    {
        // classic CTOR
        //public NotFoundException(string Message) : base(Message)
        //{
            
        //}
    }

    // sealed => bywrs 3ady msh mynf3sh ay 7d ywrs mno  
    public sealed class ProductNotFoundException(int Id) : NotFoundException($"Product with Id {Id} is not found")
    {

    }

    public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket with Id {Id} is not found")
    {

    }

}
