using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.BLL.ProductSpecifications;
using Route.DAL.Entities;
using RouteApi.Dto;
using RouteApi.Errors;
using RouteApi.Helper;

namespace RouteApi.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepositary<Product> _productRepo;
        private readonly IGenericRepositary<ProductType> _productTypeRepo;
        private readonly IGenericRepositary<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositary<Product> productRepo, IGenericRepositary<ProductType> productTypeRepo, IGenericRepositary<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductsSpecParams productsSpec)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productsSpec);

            var countSpec = new ProductsWithFiltersForCountSpecification(productsSpec);

            var totolItems = await _productRepo.GetCountAsync(countSpec);

            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productsSpec.PageIndex, productsSpec.PageSize, totolItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product is null)
            {
                return NotFound(new ApiException(404));
            }
            var data = _mapper.Map<Product, ProductDto>(product);

            return Ok(data);

        }


        [HttpGet("brands")]
        public async Task<IReadOnlyList<ProductBrand>> GetBrands()
        {
            var brands = await _productBrandRepo.GetAllAsync();
            return brands;
        }

        [HttpGet("types")]
        public async Task<IReadOnlyList<ProductType>> GetTypes()
        {
            var types = await _productTypeRepo.GetAllAsync();
            return types;
        }
    }
}
