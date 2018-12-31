using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    /// <summary>
    /// Quartz Provider
    /// </summary>
    public interface IQuartzProvider
    {

        /// <summary>
        /// query validate quartzsetting 
        /// </summary>
        /// <returns>validate quartzsetting list</returns>
        List<QuartzSetting> GetQuartzSetting();
        /// <summary>
        /// get all quartzsetting
        /// </summary>
        /// <param name="pageIndex">page index</param>
        /// <returns></returns>
        (List<QuartzSetting> list, int total) GetAllQuartzSetting(int pageIndex = 1);
        /// <summary>
        /// add quartzsetting
        /// </summary>
        /// <param name="quartzEntity">quartz setting</param>
        /// <returns>modify quartzsetting resul</returns>
        bool AddQuartzSetting(QuartzSetting quartzEntity);
        /// <summary>
        /// modify quartzsetting
        /// </summary>
        /// <param name="quartzEntity">quartz setting</param>
        /// <returns>modify quartzsetting resul</returns>
        bool ModifyQuartzSetting(QuartzSetting quartzEntity);
        /// <summary>
        /// remove quartzsetting
        /// </summary>
        /// <param name="id">quartzsetting id</param>
        /// <returns>remove quartzsetting result</returns>
        bool RemoveQuartzSetting(int id);

    }
 
}
