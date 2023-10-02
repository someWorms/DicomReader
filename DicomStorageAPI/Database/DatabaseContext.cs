using DicomStorageAPI.Database.Interfaces;
using DicomStorageAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DicomStorageAPI.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt) 
        {
            Database.EnsureCreated();
        }
        public DbSet<DicomData> DicomDatas { get; set; }
    }
}
