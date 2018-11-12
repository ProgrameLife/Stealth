using System;

namespace StealthQuartz.Entity
{
    /// <summary>
    /// SFTPSetting entity
    /// </summary>
    public class SFTPSetting
    {
        public int ID { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string CertificatePath { get; set; }

        public string TransferDirectory { get; set; }

        public string TransferFilePrefix { get; set; }

        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
