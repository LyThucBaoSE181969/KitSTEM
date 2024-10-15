using Microsoft.EntityFrameworkCore;
using SWP.KitStem.API.Data;
using SWP.KitStem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Repository;

public class GenericRepository<TEntity>
    where TEntity : class
{
    internal DataContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(DataContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task<TEntity?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

    //public virtual async Task InsertAsync(TEntity entity) => await dbSet.AddAsync(entity);
    
    public virtual async Task<bool> InsertAsync(TEntity entity)
    {
        context.Add(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public virtual void Delete(object id)
    {
        TEntity? entityToDelete = dbSet.Find(id);
        if (entityToDelete is not null)
            Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual async Task<bool> Update(TEntity entityToUpdate)
    {
        var tracker = context.Attach(entityToUpdate);
        tracker.State = EntityState.Modified;

        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> IsExist(object id)
    {
        return (await GetByIdAsync(id)) is not null;
    }

    public virtual async Task<(IEnumerable<TEntity>, int)> GetFilterAsync(
             Expression<Func<TEntity, bool>>? filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
             int? skip = null,
             int? take = null,
             params Func<IQueryable<TEntity>, IQueryable<TEntity>>[]? includes)
    {
        IQueryable<TEntity> query = dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        // Apply the filter if provided
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply ordering if provided
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Apply pagination: Skip and Take values
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return (await query.ToListAsync(), CountTotalPagesAsync(filter, take));
    }
        
    private int CountTotalPagesAsync(Expression<Func<TEntity, bool>>? filter, int? take)
    {
        IQueryable<TEntity> query = dbSet;

        // Apply the filter if provided
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Count total number of records
        int totalRecords = query.Count();

        // Ensure 'take' has a value and is greater than 0, otherwise assume all records fit on one page
        if (!take.HasValue || take.Value <= 0)
        {
            return 1; // If no 'take' value or invalid 'take', return 1 page
        }

        // Calculate total pages based on total records and take (items per page)
        int totalPages = (int)Math.Ceiling((double)totalRecords / take.Value);

        return totalPages;
    }
}
