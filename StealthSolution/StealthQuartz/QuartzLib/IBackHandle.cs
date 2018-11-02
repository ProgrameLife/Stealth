using System;
using System.Collections.Generic;
using System.Text;

namespace StealthQuartz
{
    public interface IBackHandle
    {
        bool Handle(params object[] parmeters);
    }
}
