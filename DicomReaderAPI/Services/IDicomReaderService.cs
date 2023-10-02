using DicomReaderAPI.Models;

namespace DicomReaderAPI.Services
{
    public interface IDicomReaderService
    {
        Task<DicomData> ReadFile(IFormFile file);   
    }
}
