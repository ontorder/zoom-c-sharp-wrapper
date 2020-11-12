using System;

namespace zoom_sdk_demo
{
    public class ConfigReader
    {
        private ConfigReaderDriver _driver;

        public ConfigReader(ConfigReaderDriver driver) => _driver = driver;

        private ZoomDemoConfiguration ReadFromXml()
        {
            _driver.InitDriver();

            ZoomDemoConfiguration config = new ZoomDemoConfiguration();

            config.SdkKey = _driver.ReadValue("SdkKey");
            config.SdkSecret = _driver.ReadValue("SdkSecret");
            config.MeetingNumber = UInt64.Parse(_driver.ReadValue("MeetingNumber"));
            config.MeetingPassword = _driver.ReadValue("MeetingPassword");
            config.UserName = _driver.ReadValue("UserName");

            config.EnableAutoFullScreenVideoWhenJoinMeeting = Boolean.Parse(_driver.ReadValue("EnableAutoFullScreenVideoWhenJoinMeeting"));
            config.HideSettingsDialog = Boolean.Parse(_driver.ReadValue("HideSettingsDialog"));
            config.EnableAlwaysMuteMicWhenJoinVoip = Boolean.Parse(_driver.ReadValue("EnableAlwaysMuteMicWhenJoinVoip"));
            config.EnableAutoJoinAudio = Boolean.Parse(_driver.ReadValue("EnableAutoJoinAudio"));
            config.EnableAutoTurnOffVideoWhenJoinMeeting = Boolean.Parse(_driver.ReadValue("EnableAutoTurnOffVideoWhenJoinMeeting"));
            config.TurnAudioOff = Boolean.Parse(_driver.ReadValue("TurnAudioOff"));
            config.TurnVideoOff = Boolean.Parse(_driver.ReadValue("TurnVideoOff"));

            return config;
        }

        public ZoomDemoConfiguration ReadConfig() =>
            ReadFromXml();
    }
}
