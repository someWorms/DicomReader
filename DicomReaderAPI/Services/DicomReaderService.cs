using DicomReaderAPI.Models;
using FellowOakDicom;
using System.Net;

namespace DicomReaderAPI.Services
{
    public class DicomReaderService : IDicomReaderService
    {
        public async Task<DicomData> ReadFile(IFormFile file)
        {
            //save
            var filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            //read
            var dicom = await DicomFile.OpenAsync(filePath);
            var result = new DicomData 
            {
                StudyIUID = dicom.Dataset.GetString(DicomTag.StudyInstanceUID) 
            };

            //delete 
            File.Delete(filePath);

            return result;
        }
    }
}
