using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    public interface IEmailProvider
    {
        /// <summary>
        /// get all emailsetting
        /// </summary>
        /// <returns></returns>
        (List<EmailSetting> list, int total) GetAllEmailSetting(int pageIndex = 1);

        /// <summary>
        /// get all emailsetting
        /// </summary>
        /// <returns></returns>
        List<EmailSetting> GetEmailSettings();

        /// <summary>
        /// get a emailsetting by keyname
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        EmailSetting GetEmailSetting(string keyName);

        /// <summary>
        /// add emailsetting
        /// </summary>
        /// <param name="emailSetting">emailsetting</param>
        /// <returns></returns>
        bool AddEmailSetting(EmailSetting emailSetting);
        /// <summary>
        /// modify emailsetting
        /// </summary>
        /// <param name="sFTPSetting">emailsetting</param>
        /// <returns></returns>
        bool ModifyEmailSetting(EmailSetting emailSetting);
        /// <summary>
        /// remove emailsetting
        /// </summary>
        /// <param name="id">emailsetting id</param>
        /// <returns></returns>
        bool RemoveEmailSetting(int id);
    }
}
