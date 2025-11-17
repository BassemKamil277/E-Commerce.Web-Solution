using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class ProductsQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }

        public string? search { get; set; }

        public ProductSortingOptions Sort { get; set; }

        private int _pageIndex = 1 ; // 3mlt manual 34an 3aiz a7ot validation
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = (value <=0) ? 1 : value;
            }
        }

        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;


        private int _pageSize = DefaultPageSize; // 3mlt manual 34an 3aiz a7ot validation
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if(value <= 0)
                    value = DefaultPageSize;
                else if(value >  MaxPageSize) 
                    value -= MaxPageSize;
                else
                    _pageSize = value;
            }
        }
    }
}
