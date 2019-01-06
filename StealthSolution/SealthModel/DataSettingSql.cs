using System;
using System.Collections.Generic;
using System.Text;

namespace SealthModel
{
    /// <summary>
    /// data setting and sqls
    /// </summary>
    public class DataSettingSql
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string ConnectionString { get; set; }
        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
        public int SqlID { get; set; }
        public int DataSettingID { get; set; }
        public string Sql { get; set; }
        public string TransactionNo { get; set; }
        public string GroupNo { get; set; }
        public bool SqlValidate { get; set; }
        public DateTime SqlCreateOn { get; set; }
    }
}
