using DicomStorageAPI.Models;

namespace DicomStorageAPI.Services.Interfaces
{
    public interface IDataService
    {
        Task SaveData(DicomData data);
    }
}
