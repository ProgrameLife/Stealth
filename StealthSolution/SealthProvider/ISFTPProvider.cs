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
        /// <param name="sFTPSetting">sftpsetting</param>
        /// <returns></returns>
        bool AddSFTPSetting(SFTPSetting sFTPSetting);
        /// <summary>
        /// modify sftpsetting
        /// </summary>
        /// <param name="sFTPSetting">sftpsetting</param>
        /// <returns></returns>
        bool ModifySFTPSetting(SFTPSetting sFTPSetting);
        /// <summary>
        /// remove sftpsetting
        /// </summary>
        /// <param name="id">sftpsetting id</param>
        /// <returns></returns>
        bool RemoveSFTPSetting(int id);
    }
}
