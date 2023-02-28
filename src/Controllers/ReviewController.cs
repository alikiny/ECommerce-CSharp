using Backend.src.Services.ReviewService;

namespace Backend.src.Controllers
{
    public class ReviewController : GenericController<Review, ReviewDto>
    {
        public ReviewController(IReviewService service) : base(service)
        {
        }
    }
}