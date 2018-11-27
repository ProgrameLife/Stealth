namespace StealthBuildData
{
    public interface IBuildData
    {
        T BuildData<T>(string keyName);
    }
}
