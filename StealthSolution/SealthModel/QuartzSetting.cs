namespace SealthModel
{
    /// <summary>
    /// Quartz Core Entity
    /// </summary>
    public class QuartzSetting
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Key Name
        /// </summary>
        public string KeyName
        { get; set; }
        /// <summary>
        /// Type Name
        /// </summary>
        public string TypeName
        { get; set; }
        /// <summary>
        /// Corn Expression
        /// </summary>
        public string CronExpression
        { get; set; }
        /// <summary>
        /// Validate
        /// </summary>
        public bool Validate { get; set; }

    }
}
