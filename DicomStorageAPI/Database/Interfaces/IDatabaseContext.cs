using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using DicomStorageAPI.Models;

namespace DicomStorageAPI.Database.Interfaces
{
    public interface IDatabaseContext
    {
        public DbSet<DicomData> DicomDatas { get; set; }


        /// <summary>
        ///     Entry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry Entry([NotNull] object entity);

        /// <summary>
        ///     Entry
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

        /// <summary>
        ///     Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        ///     Save changes async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
