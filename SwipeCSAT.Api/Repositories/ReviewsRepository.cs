﻿using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Repositories;

public class ReviewsRepository
{
    private readonly SwipeCsatDbContext _context;
    private readonly ILogger _logger;


    public ReviewsRepository(SwipeCsatDbContext context, ILogger<ReviewsRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ReviewEntity>> GetAll()
    {
        return await _context.Reviews.Include(c => c.ProductEntity).Include(x => x.CriterionRatings).AsNoTracking()
            .ToListAsync();
    }


    public async Task<ReviewEntity> Add(string productName, List<int> ratings,Guid userId)
    {
        var product = await _context.Products.Include(x => x.Category).ThenInclude(x => x!.Criterions)
                          .FirstOrDefaultAsync(x => x.Name == productName)
                      ?? throw new Exception("Данный продукт не найден");

        // Создаём новый отзыв
        var newReview = new ReviewEntity
        {
            Id = Guid.NewGuid(),
            ProductEntity = product,
            CriterionRatings = new List<CriterionRatingEntity>(),
            UserId = userId
        };
        _context.Reviews.Add(newReview);

        for (var i = 0; i < ratings.Count; i++)
        {
            _logger.LogInformation("Новый рейтинг критерия создается");
            var criterionRating = new CriterionRatingEntity
            {
                Id = Guid.NewGuid(), // Генерируем уникальный идентификатор для каждого рейтинга
                Rating = ratings[i],
                CriterionName =
                    product.Category!.Criterions[i]
                        .Name, // Связываем с соответствующим критерием продукта         // Связываем с продуктом
                Review = newReview // Указываем, что данный рейтинг относится к новому отзыву
            };
            _context.CriterionRatings.Add(criterionRating);
            _logger.LogInformation("Новый рейтинг критерия добавлен в отзыв");
        }

        _logger.LogInformation("Отзыв добавлен");
        // Добавляем новый отзыв в контекст и сохраняем изменения

        await _context.SaveChangesAsync();

        return newReview;
    }

    public async Task<ReviewEntity> GetById(string reviewId)
    {
        var review = await _context.Reviews.Include(c=>c.CriterionRatings).FirstAsync(r=> r.Id==Guid.Parse(reviewId));
        return review;
    }
}