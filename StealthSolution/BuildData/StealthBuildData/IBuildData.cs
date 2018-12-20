namespace StealthBuildData
{
    public interface IBuildData
    {
        T BuildData<T>(string keyName);
    }

    //todo  this is a demo class,will remove it;这只是一个演示的类，正式后会移除
    public class DemoBuildData : IBuildData
    {
        public T BuildData<T>(string keyName)
        {
            return default(T);
        }
    }
}
