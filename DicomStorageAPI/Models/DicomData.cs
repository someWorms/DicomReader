using System.ComponentModel.DataAnnotations;

namespace DicomStorageAPI.Models
{
    public class DicomData
    {
        [Key]
        public ulong Id { get; set; }
        public string StudyIUID { get; set; }
    }
}
