﻿using Magic_Villa_Api.Models;
using System.Linq.Expressions;

namespace Magic_Villa_Api.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null , string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true , string? includeProperties = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
