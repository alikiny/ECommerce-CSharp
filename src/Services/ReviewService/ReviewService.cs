namespace Backend.src.Services.ReviewService
{
    public class ReviewService : BaseService<Review, ReviewDto, ReviewDto, ReviewDto>, IReviewService
    {  
        public ReviewService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}