namespace StealthBackHandle
{
    /// <summary>
    /// BackHandle interface
    /// </summary>
    public interface IBackHandle
    {

        /// <summary>
        /// task execute method
        /// </summary>  
        /// <param name="keyName">key name</param>
        /// <returns></returns>
        bool Handle(string keyName);

    }
}
