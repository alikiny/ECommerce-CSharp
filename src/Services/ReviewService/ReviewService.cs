namespace Backend.src.Services.ReviewService
{
    public class ReviewService : BaseService<Review, ReviewDto, ReviewDto, ReviewDto>, IReviewService
    {
        public ReviewService(IMapper mapper, IBaseRepository<Review> repository) : base(mapper, repository)
        {
        }
    }
}