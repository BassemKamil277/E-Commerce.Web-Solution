using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product , int>
    {
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        public ProductWithTypeAndBrandSpecification(ProductsQueryParams queryParams) 
            : base(ProductSpecificationHelper.GetProductCriteria(queryParams)) 
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch(queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderByDescending(P => P.Price);
                    break;


                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;

                default:
                    AddOrderBy(P => P.Id);
                    break;


            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
