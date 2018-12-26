using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    public interface ISFTPProvider
    {       /// <summary>
            /// get all sftpsetting
            /// </summary>
            /// <param name="pageIndex">page index</param>
            /// <returns></returns>   
        (List<SFTPSetting> list, int total) GetAllSFTPSetting(int pageIndex = 1);
        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        List<SFTPSetting> GetSFTPSettings();
        /// <summary>
        /// get sftpsetting
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        SFTPSetting GetSFTPSetting(string keyName);
        /// <summary>
        /// add sftpsetting
        /// </summary>
        /// <param name="sftpSetting">sftpsetting</param>
        /// <returns></returns>
        bool AddSFTPSetting(SFTPSetting sftpSetting);
        /// <summary>
        /// modify sftpsetting
        /// </summary>
        /// <param name="sftpSetting">sftpsetting</param>
        /// <returns></returns>
        bool ModifySFTPSetting(SFTPSetting sftpSetting);
        /// <summary>
        /// remove sftpsetting
        /// </summary>
        /// <param name="id">sftpsetting id</param>
        /// <returns></returns>
        bool RemoveSFTPSetting(int id);
    }
}
