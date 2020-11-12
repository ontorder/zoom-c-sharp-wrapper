namespace zoom_sdk_demo
{
    public abstract class ConfigReaderDriver
    {
        public abstract string ReadValue(string name);
        public abstract void InitDriver();
    }
}
