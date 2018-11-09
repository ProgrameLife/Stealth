using System.Collections.Generic;

namespace StealthQuartz
{
    public interface IProvider
    {
        List<QuartzEntity> GetQuartzEntity();
    }
}
