﻿using System;

namespace SealthModel
{
    /// <summary>
    /// EmailSetting entity
    /// </summary>
    public class DataSetting
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string ConnectionString{ get; set; }
        public string  Sql { get; set; }
        public string TransactionNo { get; set; }
        public string GroupNo { get; set; }
        public bool Validate { get; set; }
        public DateTime CreateOn { get; set; }
    }
}