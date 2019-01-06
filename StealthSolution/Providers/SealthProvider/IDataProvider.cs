using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    /// <summary>
    /// data provider interface
    /// </summary>
    public interface IDataProvider
    {
        #region datasetting
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
        #endregion

        #region datasql
        /// <summary>
        /// get all dataSql
        /// </summary>
        /// <returns></returns>
        List<DataSql> GetAllDataSql();

        /// <summary>
        /// get all dataSql
        /// </summary>
        /// <returns></returns>
        List<DataSql> GetDataSqls();

        /// <summary>
        /// add dataSql
        /// </summary>
        /// <param name="dataSql">data setting</param>
        /// <returns></returns>
        bool AddDataSql(DataSql dataSql);
        /// <summary>
        /// modify dataSql
        /// </summary>
        /// <param name="dataSetting">data setting</param>
        /// <returns></returns>
        bool ModifyDataSql(DataSql  dataSql);
        /// <summary>
        /// remove dataSql
        /// </summary>
        /// <param name="id">dataSql id</param>
        /// <returns></returns>
        bool RemoveDataSql(int id);

        #endregion


        /// <summary>
        /// get  datasetting and datasql
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        DataSettingSql GetDataSettingSqls(string keyName);
    }
}
