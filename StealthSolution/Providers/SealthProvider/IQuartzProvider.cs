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
        List<QuartzEntity> GetQuartzSetting();
        /// <summary>
        /// get all quartzsetting
        /// </summary>
        /// <returns>all  quartzsetting</returns>
        List<QuartzEntity> GetAllQuartzSetting();
        /// <summary>
        /// add quartzsetting
        /// </summary>
        /// <param name="quartzEntity">quartz setting</param>
        /// <returns>modify quartzsetting resul</returns>
        bool AddQuartzSetting(QuartzEntity quartzEntity);
        /// <summary>
        /// modify quartzsetting
        /// </summary>
        /// <param name="quartzEntity">quartz setting</param>
        /// <returns>modify quartzsetting resul</returns>
        bool ModifyQuartzSetting(QuartzEntity quartzEntity);
        /// <summary>
        /// remove quartzsetting
        /// </summary>
        /// <param name="id">quartzsetting id</param>
        /// <returns>remove quartzsetting result</returns>
        bool RemoveQuartzSetting(int id);

    }
 
}
