using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShowCaseToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseToDo.Services.DataBaseService
{
    public class SqlDataAccessService<T> : IDataAccessService<T> where T : class, IIdentifiable<string>
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;
        List<T> items;
        bool initialized;


        public SqlDataAccessService(AppDbContext<T> context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            items.Add(entity);
            return entry.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var item = items.FirstOrDefault(i => i.Id == entity.Id);
            item = entity;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public T Get(string id)
        {
            return dbSet.Find(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            if (initialized) return items;
            items = await dbSet.ToListAsync();
            initialized = true;
            return items;
        }

        Task ISortable.MoveToNewPosition(int oldIndex, int newIndex)
        {
            throw new NotImplementedException();
        }
    }

}