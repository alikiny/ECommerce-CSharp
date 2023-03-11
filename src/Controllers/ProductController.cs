using System.Security.Claims;
using Backend.src.Repository.ProductRepository;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class ProductController
        : GenericController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductController(
            IProductService productService,
            IAuthorizationService authorizationService,
            IProductRepository productRepository
        )
            : base(productService)
        {
            _authorizationService = authorizationService;
            _productService = productService;
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<List<ProductReadDto>>> GetAll(
            [FromQuery] GetAllQueryOptions options
        )
        {
            return await base.GetAll(options);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            return await base.GetById(id);
        }

        [Authorize(Policy = "SellerOnlyPolicy")]
        public override async Task<ActionResult<ProductReadDto>> AddOne(ProductCreateDto dto)
        {
            var sellerId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return await _productService.AddOneAsync(dto, sellerId);
        }

        public override async Task<ActionResult<ProductReadDto>> UpdateOne(
            int id,
            ProductUpdateDto update
        )
        {
            return await base.UpdateOne(id, update);
        }

        public override async Task<ActionResult<bool>> DeleteById(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);
            var user = HttpContext.User;
            var authorizationResut = await _authorizationService.AuthorizeAsync(
                user,
                entity,
                "ProductDeletePolicy"
            );
            if (authorizationResut.Succeeded)
            {
                return await base.DeleteById(id);
            }
            else if (user.Identity!.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else{
                Console.WriteLine("challenge");
                return new ChallengeResult();
            }
        }
    }
}
