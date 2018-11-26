using System;

namespace SealthModel
{
    /// <summary>
    /// EmailSetting entity
    /// </summary>
    public class EmailSetting
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddresses { get; set; }
        public string ToAddresses { get; set; }
        public bool IsCompress { get; set; }
        public string CompressFile { get; set; }
        public string CompressPassword { get; set; }
        public bool IsAttachment { get; set; }
        public bool AttachmentName { get; set; }
        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
