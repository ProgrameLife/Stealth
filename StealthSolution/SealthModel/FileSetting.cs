using System;

namespace SealthModel
{
    /// <summary>
    /// EmailSetting entity
    /// </summary>
    public class FileSetting
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string FileName { get; set; }     
        public string FilePath { get; set; }     
        public string FileEncoding { get; set; }
        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
