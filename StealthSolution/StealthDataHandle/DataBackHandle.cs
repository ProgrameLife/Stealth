using Microsoft.Extensions.Logging;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using StealthQuartz;
using System;
using System.Text;

namespace StealthDataHandle
{
    /// <summary>
    /// handle background database operations
    /// </summary>
    public class DataBackHandle : IBackHandle
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<DataBackHandle> _logger;
        /// <summary>
        /// data builder
        /// </summary>
        readonly IBuildData _buildData;

        /// <summary>
        /// data provider
        /// </summary>
        readonly IDataProvider _dataProvider;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger">dependency injection logger</param>
        /// <param name="buildData">dependency injection  builddate</param>   
        public DataBackHandle(ILogger<DataBackHandle> logger, IBuildData buildData, IDataProvider dataProvider)
        {
            _buildData = buildData;
            _logger = logger;
            _dataProvider = dataProvider;
        }

        public bool Handle(string keyName)
        {
            
            throw new NotImplementedException();
        }       
    }
}
