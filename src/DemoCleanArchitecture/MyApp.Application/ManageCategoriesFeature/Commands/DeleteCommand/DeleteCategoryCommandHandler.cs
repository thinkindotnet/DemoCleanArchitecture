﻿using MyApp.Application.Common.Exceptions;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Entities;


namespace MyApp.Application.ManageCategoriesFeature.Commands.DeleteCommand;


public class DeleteCategoryCommandHandler
    : IRequestHandler<DeleteCategoryCommand>
{

    private readonly IApplicationDbContext _dbContext;


    public DeleteCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    #region IRequestHandler members

    public async Task Handle(
        DeleteCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var entity
            = await _dbContext.Categories
                              .FindAsync(keyValues: [request.CategoryId], cancellationToken)
              ?? throw new MyNotFoundException(request.CategoryId.ToString(), nameof(Category));

        _dbContext.Categories.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

}
