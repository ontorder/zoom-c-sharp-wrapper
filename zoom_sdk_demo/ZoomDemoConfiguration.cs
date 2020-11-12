namespace zoom_sdk_demo
{
    public class ZoomDemoConfiguration
    {
        public string SdkKey;
        public string SdkSecret;
        public ulong MeetingNumber;
        public string UserName;
        public string MeetingPassword;

        public bool HideSettingsDialog;
        public bool? EnableAlwaysMuteMicWhenJoinVoip;
        public bool? EnableAutoJoinAudio;
        public bool? EnableAutoTurnOffVideoWhenJoinMeeting;
        public bool? EnableAutoFullScreenVideoWhenJoinMeeting;
        public bool? TurnAudioOff;
        public bool? TurnVideoOff;
    }
}
