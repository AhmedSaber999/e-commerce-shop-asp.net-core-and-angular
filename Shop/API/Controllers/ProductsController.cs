using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specification;
using AutoMapper;
using API.DataShape;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Helpers.Pagination;
using System;
namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> productBrandsRepository;
        private readonly IGenericRepository<ProductType> productTypesRepository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandsRepository,
        IGenericRepository<ProductType> productTypesRepository,
        IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productBrandsRepository = productBrandsRepository;
            this.productTypesRepository = productTypesRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ProductReturnedData>> GetProducts([FromQuery]ProductSpecificationParams productSpecificationParams)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productSpecificationParams);
            var products = productRepository.GetListWithSpecification(specification);
            
            var count_specification = new ProductWithFiltersForCountSpecification(productSpecificationParams);
            var total_items = productRepository.Count(count_specification);

            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnedData>>(products); 
            return Ok(new Pagination<ProductReturnedData>
            (productSpecificationParams.PageIndex, productSpecificationParams.PageSize, total_items, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public ActionResult<ProductReturnedData> GetProduct(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            var product = productRepository.GetEntityWithSpecification(specification);
            if(product == null){return NotFound(new ApiResponse(404));}
            return Ok(mapper.Map<Product, ProductReturnedData>(product));
        }
         [HttpGet("brands")]
        public ActionResult<Product> GetProductBrands()
        {
            var product = productBrandsRepository.GetAll();
            return Ok(product);
        }
         [HttpGet("types")]
        public ActionResult<Product> GetProductTypes()
        {
            var product = productTypesRepository.GetAll();
            return Ok(product);
        }
        
    }
}