namespace DicomReaderAPI.Models
{
    public class DicomData
    {
        public string? StudyIUID { get; set; }

        public override string ToString()
        {
            return $"StudyIUID: {StudyIUID}";
        }
    }
}
