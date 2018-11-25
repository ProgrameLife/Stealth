using System;
using System.Collections.Generic;
using System.Text;

namespace StealthBackHandle
{
    /// <summary>
    /// BackHandle interface
    /// </summary>
    public interface IBackHandle
    {
        bool Handle();
        /// <summary>
        /// task execute method
        /// </summary>
        /// <param name="parmeters"></param>
        /// <returns></returns>
        //bool Handle(string content, Encoding encoding, params object[] parmeters);
    }
}
