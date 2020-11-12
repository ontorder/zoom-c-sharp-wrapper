using System.Xml;

namespace zoom_sdk_demo
{
    public class ConfigReaderXml : ConfigReaderDriver
    {
        private string _configFilename;
        private XmlDocument _configXml;
        private XmlAttributeCollection _rootAttributes;

        public ConfigReaderXml(string filename) => _configFilename = filename;

        public override void InitDriver()
        {
            _configXml = new XmlDocument();
            _configXml.Load(_configFilename);
            _rootAttributes = _configXml.DocumentElement.Attributes;
        }

        public override string ReadValue(string name) =>
            _rootAttributes[name].Value;
    }
}
