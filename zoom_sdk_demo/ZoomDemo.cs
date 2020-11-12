using System;
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    public class ZoomDemo
    {
        private ZoomDemoConfiguration _config;

        // ---------------

        private void SdkInit()
        {
            ZOOM_SDK_DOTNET_WRAP.InitParam param = new ZOOM_SDK_DOTNET_WRAP.InitParam();
            param.web_domain = "https://zoom.us";
            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.Initialize(param);

            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
            }
            else
            {
                //error handle.todo
            }
        }

        private void SdkClear()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.CleanUp();
        }

        public ZoomDemo(ZoomDemoConfiguration config)
        {
            _config = config;
            SdkInit();
        }

        ~ZoomDemo()
        {
            CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().LogOut();
            SdkClear();
        }

        public void Start()
        {
            Auth();
            // JoinMeeting (in onAuthenticationReturn)
        }

        public void Stop()
        {
            // TODO chiudi meeting
        }

        public void onAuthenticationReturn(AuthResult ret)
        {
            if (AuthResult.AUTHRET_SUCCESS == ret)
            {
                JoinMeeting();
                return;
            }

            throw new Exception("Authentication FAILED");
        }

        public void onLoginRet(LOGINSTATUS ret, IAccountInfo pAccountInfo)
        {
        }

        public void onLogout()
        {
            //todo
        }

        public bool SetLayout(int id)
        {
            switch (id)
            {
                case 1: CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().SwitchToVideoWall(); return true;
                case 2: CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().SwitchSplitScreenMode(true); return true;
                case 3: CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().SwitchSplitScreenMode(false); return true;
                case 4: CZoomSDKeDotNetWrap.Instance.GetSettingServiceWrap().GetVideoSettings().EnableHideNoVideoUsersOnWallView(true); return true;
                case 5: CZoomSDKeDotNetWrap.Instance.GetSettingServiceWrap().GetVideoSettings().EnableSpotlightSelf(true); return true;
                case 6: CZoomSDKeDotNetWrap.Instance.GetSettingServiceWrap().GetGeneralSettings().EnableSplitScreenMode(true); return true;
                case 7: CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().SwitchFloatVideoToActiveSpkMod(); return true;
                case 8: CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().SwtichToAcitveSpeaker(); return true;
            }

            return false;
        }

        //ZOOM_SDK_DOTNET_WRAP.onMeetingStatusChanged
        private void onMeetingStatusChanged(MeetingStatus status, int iResult)
        {
            switch (status)
            {
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_ENDED:
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_FAILED:
                {
                    // TODO DEINIZIALIZZARE?
                }
                break;

                case MeetingStatus.MEETING_STATUS_INMEETING:
                    break;

                default:
                    break;
            }
        }

        private void AuthRegisterCallback()
        {
            CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onAuthenticationReturn(onAuthenticationReturn);
            CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLoginRet(onLoginRet);
            CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().Add_CB_onLogout(onLogout);
        }

        private void Auth()
        {
            AuthRegisterCallback();

            AuthParam param = new AuthParam();
            param.appKey = _config.SdkKey;
            param.appSecret = _config.SdkSecret;
            SDKError sdkerr = CZoomSDKeDotNetWrap.Instance.GetAuthServiceWrap().SDKAuth(param);

            if (sdkerr != SDKError.SDKERR_SUCCESS)
                throw new Exception("Auth not successful");
        }

        private void RegisterCallBack()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Add_CB_onMeetingStatusChanged(onMeetingStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onHostChangeNotification(onHostChangeNotification);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onLowOrRaiseHandStatusChanged(onLowOrRaiseHandStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserJoin(onUserJoin);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserLeft(onUserLeft);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserNameChanged(onUserNameChanged);
        }

        private void SetMeetingOptions()
        {
            ISettingServiceDotNetWrap settings = CZoomSDKeDotNetWrap.Instance.GetSettingServiceWrap();

            //CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController().EnterFullScreen(true, false);
            if (_config.EnableAutoFullScreenVideoWhenJoinMeeting.HasValue)
                settings.GetGeneralSettings().EnableAutoFullScreenVideoWhenJoinMeeting(_config.EnableAutoFullScreenVideoWhenJoinMeeting.Value);

            if (_config.HideSettingsDialog)
                settings.HideSettingDlg();

            if (_config.EnableAlwaysMuteMicWhenJoinVoip.HasValue)
                settings.GetAudioSettings().EnableAlwaysMuteMicWhenJoinVoip(_config.EnableAlwaysMuteMicWhenJoinVoip.Value);

            if (_config.EnableAutoJoinAudio.HasValue)
                settings.GetAudioSettings().EnableAutoJoinAudio(_config.EnableAutoJoinAudio.Value);

            if (_config.EnableAutoTurnOffVideoWhenJoinMeeting.HasValue)
                settings.GetVideoSettings().EnableAutoTurnOffVideoWhenJoinMeeting(_config.EnableAutoTurnOffVideoWhenJoinMeeting.Value);

            //CZoomSDKeDotNetWrap.Instance.GetSettingServiceWrap().GetAudioSettings().SelectSpeaker("", "");
        }

        private void JoinMeeting()
        {
            SetMeetingOptions();
            RegisterCallBack();

            ZOOM_SDK_DOTNET_WRAP.JoinParam param = new ZOOM_SDK_DOTNET_WRAP.JoinParam();
            param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_APIUSER;
            ZOOM_SDK_DOTNET_WRAP.JoinParam4APIUser join_api_param = new ZOOM_SDK_DOTNET_WRAP.JoinParam4APIUser();
            join_api_param.meetingNumber = _config.MeetingNumber;
            join_api_param.userName = _config.UserName;
            join_api_param.psw = _config.MeetingPassword;

            if (_config.TurnAudioOff.HasValue)
                join_api_param.isAudioOff = _config.TurnAudioOff.Value;
            if (_config.TurnVideoOff.HasValue)
                join_api_param.isVideoOff = _config.TurnVideoOff.Value;

            param.apiuserJoin = join_api_param;

            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Join(param);
            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
            }
            else
            {
                throw new Exception("Join Meeting FAILED");
            }
        }

        public void onUserJoin(Array lstUserID)
        {
            if (null == lstUserID)
                return;

            //for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            //{
            //    UInt32 userid = (UInt32)lstUserID.GetValue(i);
            //    ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance
            //        .GetMeetingServiceWrap().
            //        GetMeetingParticipantsController()
            //        .GetUserByUserID(userid);
            //    if (null != user)
            //        Console.Write(user.GetUserNameW());
            //}
        }

        public void onUserLeft(Array lstUserID)
        {
        }

        public void onHostChangeNotification(UInt32 userId)
        {
        }

        public void onLowOrRaiseHandStatusChanged(bool bLow, UInt32 userid)
        {
        }

        public void onUserNameChanged(UInt32 userId, string userName)
        {
        }
    }
}
