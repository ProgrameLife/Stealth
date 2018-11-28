using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    public interface ISFTPProvider
    {       /// <summary>
            /// get all sftpsetting
            /// </summary>
            /// <returns></returns>
        List<SFTPSetting> GetAllSFTPSetting();

        /// <summary>
        /// get all sftpsetting
        /// </summary>
        /// <returns></returns>
        List<SFTPSetting> GetSFTPSettings();
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
