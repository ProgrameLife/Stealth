using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{ 
    public interface IStatuProvider
    {
        /// <summary>
        /// get all stealths statu
        /// </summary>
        /// <returns></returns>
        List<StealthsStatu> GetAllStealthsStatus();
        /// <summary>
        /// set status success
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool SetSuccess(string keyName);
        /// <summary>
        /// set status start
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool SetStart(string keyName);
        /// <summary>
        /// set status failed
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool SetFailed(string keyName);
    }
}
