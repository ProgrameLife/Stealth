using SealthModel;
using System.Collections.Generic;

namespace SealthProvider
{
    public interface IQuartzProvider
    {

        /// <summary>
        /// query QueartzEntity list
        /// </summary>
        /// <returns></returns>
        List<QuartzEntity> GetQuartzEntity();
    }
 
}
