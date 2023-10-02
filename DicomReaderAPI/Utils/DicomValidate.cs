namespace DicomReaderAPI.Utils
{
    public static class DicomValidate
    {
        public static bool IsDicomFile(this Stream s)
        {
            byte[] dba = new byte[4];

            //Seek to 0x80 and read
            s.Seek(128, SeekOrigin.Begin);
            s.Read(dba, 0, 4);
            s.Close();

            return dba.SequenceEqual(new byte[4] { 68, 73, 67, 77 });
        }

        public static bool IsDicomFile(this MemoryStream ms)
        {
            return ((Stream)ms).IsDicomFile();
        }

        public static bool IsDicomFile(this FileStream fs)
        {
            return ((Stream)fs).IsDicomFile();
        }

        public static bool IsDicomFile(this FileInfo fi)
        {
            return fi.OpenRead().IsDicomFile();
        }
    }
}
