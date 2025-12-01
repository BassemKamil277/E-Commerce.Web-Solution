using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{


    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [RedisChach]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetProducts([FromQuery]ProductsQueryParams queryParams)
        {
            var Products = await _productService.GetAlProductsAsync(queryParams);
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
                //throw new Exception();
                var Product = await _productService.GetProductByIdAsync(id);
                return HandleResult<ProductDTO>(Product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandDTO>> GetAllBrands()
        {
            var Brands = await _productService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<TypeDTO>> GetAllTypes()
        {
            var Types = await _productService.GetAllTypesAsync();
            return Ok(Types);
        }
    }
}
