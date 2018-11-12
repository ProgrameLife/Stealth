using System;

namespace StealthQuartz.Entity
{
    /// <summary>
    /// exec statu
    /// </summary>
    public class StealthsStatu
    {
        /// <summary>
        /// key name
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 1-beging  2-success 3-failed
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// update status time
        /// </summary>
        public DateTime Modifytime { get; set; }
    }
}
