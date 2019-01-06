using System;
using System.Collections.Generic;
using System.Text;

namespace SealthModel
{
    /// <summary>
    /// data sqls
    /// </summary>
    public class DataSql
    {
        public int ID { get; set; }
        public int DataSettingID { get; set; }
        public string Sql { get; set; }
        public string TransactionNo { get; set; }
        public string GroupNo { get; set; }
        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
