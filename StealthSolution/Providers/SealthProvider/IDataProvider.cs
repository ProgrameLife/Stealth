using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    /// <summary>
    /// data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// get all datasetting
        /// </summary>
        /// <returns></returns>
        List<DataSetting> GetAllDataSetting();

        /// <summary>
        /// get all datasetting
        /// </summary>
        /// <returns></returns>
        List<DataSetting> GetDataSettings();

        /// <summary>
        /// get a datasetting by keyname
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        IEnumerable<DataSetting> GetDataSettings(string keyName);

        /// <summary>
        /// add datasetting
        /// </summary>
        /// <param name="dataSetting">data setting</param>
        /// <returns></returns>
        bool AddDataSetting(DataSetting dataSetting);
        /// <summary>
        /// modify datasetting
        /// </summary>
        /// <param name="dataSetting">data setting</param>
        /// <returns></returns>
        bool ModifyDataSetting(DataSetting dataSetting);
        /// <summary>
        /// remove datasetting
        /// </summary>
        /// <param name="id">datasetting id</param>
        /// <returns></returns>
        bool RemoveDataSetting(int id);
    }
}
