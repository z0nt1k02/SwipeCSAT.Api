using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using Microsoft.Extensions.Logging;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Repositories
{
    public class ReviewsRepository
    {
        private readonly SwipeCSATDbContext _context;
        private readonly ILogger _logger;


        public ReviewsRepository(SwipeCSATDbContext context, ILogger<ReviewsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ReviewEntity>> GetAll()
        {
            return await _context.Reviews.Include(c => c.ProductEntity).Include(x=>x.CriterionRatings).AsNoTracking().ToListAsync();
        }

        
        
        public async Task<ReviewEntity> Add(string productName, List<int> ratings)
        {
            var product = await _context.Products.Include(x=>x.Category).ThenInclude(x=>x.Criterions).FirstOrDefaultAsync(x => x.Name == productName)
                ?? throw new Exception("Данный продукт не найден");
            // Создаём новый отзыв
            var newReview = new ReviewEntity
            {
                Id = Guid.NewGuid(),
                ProductEntity = product,
                CriterionRatings = new List<CriterionRatingEntity>()
            };
            _context.Reviews.Add(newReview);
            
            for (int i = 0; i < ratings.Count; i++)
            {
                _logger.LogInformation("Новый рейтинг критерия создается");
                var criterionRating = new CriterionRatingEntity
                {
                    Id = Guid.NewGuid(),       // Генерируем уникальный идентификатор для каждого рейтинга
                    Rating = ratings[i],
                    CriterionName = product.Category!.Criterions[i].Name, // Связываем с соответствующим критерием продукта         // Связываем с продуктом
                    Review = newReview,        // Указываем, что данный рейтинг относится к новому отзыву

                };
                _context.CriterionRatings.Add(criterionRating);
                _logger.LogInformation("Новый рейтинг критерия добавлен в отзыв");



            }
            _logger.LogInformation("Отзыв добавлен");
            // Добавляем новый отзыв в контекст и сохраняем изменения

            await _context.SaveChangesAsync();

            return newReview;

        }

        public async Task DeleteCategory(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name)
                ?? throw new Exception("Данная категория не найдена");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
