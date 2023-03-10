namespace Backend.src.Repository.ReviewRepository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
