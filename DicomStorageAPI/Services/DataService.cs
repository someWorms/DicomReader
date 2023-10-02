using DicomStorageAPI.Database.Interfaces;
using DicomStorageAPI.Models;
using DicomStorageAPI.Services.Interfaces;

namespace DicomStorageAPI.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseContext _db;
        public DataService(IDatabaseContext db)
        {
            _db = db;
        }

        public async Task SaveData(DicomData data)
        {
            await _db.DicomDatas.AddAsync(data);
            await _db.SaveChangesAsync();
        }
    }
}
