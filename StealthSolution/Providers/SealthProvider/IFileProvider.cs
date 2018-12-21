using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    /// <summary>
    /// File Provider Interface
    /// </summary>
    public interface IFileProvider
    {
        /// <summary>
        /// get all fileSetting
        /// </summary>
        /// <returns></returns>
        List<FileSetting> GetAllFileSetting();

        /// <summary>
        /// get a fileSetting by keyname
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        FileSetting GetFileSetting(string keyName);

        /// <summary>
        /// add fileSetting
        /// </summary>
        /// <param name="fileSetting">fileSetting</param>
        /// <returns></returns>
        bool AddFileSetting(FileSetting fileSetting);
        /// <summary>
        /// modify fileSetting
        /// </summary>
        /// <param name="fileSetting">fileSetting</param>
        /// <returns></returns>
        bool ModifyFileSetting(FileSetting  fileSetting);
        /// <summary>
        /// remove fileSetting
        /// </summary>
        /// <param name="id">fileSetting id</param>
        /// <returns></returns>
        bool RemoveFileSetting(int id);
    }
}
