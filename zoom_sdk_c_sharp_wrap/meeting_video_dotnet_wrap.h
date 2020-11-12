#pragma once
using namespace System;
#include "zoom_sdk_dotnet_wrap_def.h"
namespace ZOOM_SDK_DOTNET_WRAP {

	public enum class VideoStatus : int
	{
		Video_ON,
		Video_OFF,
	};

	public delegate void onUserVideoStatusChange(unsigned int userId, VideoStatus status);
	public delegate void onSpotlightVideoChangeNotification(bool bSpotlight, unsigned int userid);
	public delegate void onActiveSpeakerVideoUserChanged(unsigned int userid);
	public delegate void onActiveVideoUserChanged(unsigned int userid);

	public interface class IMeetingVideoControllerDotNetWrap
	{
	public:
		SDKError MuteVideo();
		SDKError UnmuteVideo();
		SDKError PinVideo(bool bPin, bool bFirstView, unsigned int userid);
		SDKError SpotlightVideo(bool bSpotlight, unsigned int userid);
		SDKError HideOrShowNoVideoUserOnVideoWall(bool bHide);

		void Add_CB_onUserVideoStatusChange(onUserVideoStatusChange^ cb);
		void Remove_CB_onUserVideoStatusChange(onUserVideoStatusChange^ cb);

		void Add_CB_onSpotlightVideoChangeNotification(onSpotlightVideoChangeNotification^ cb);
		void Remove_CB_onSpotlightVideoChangeNotification(onSpotlightVideoChangeNotification^ cb);

		void Add_CB_onActiveSpeakerVideoUserChanged(onActiveSpeakerVideoUserChanged^ cb);
		void Remove_CB_onActiveSpeakerVideoUserChanged(onActiveSpeakerVideoUserChanged^ cb);

		void Add_CB_onActiveVideoUserChanged(onActiveVideoUserChanged^ cb);
		void Remove_CB_onActiveVideoUserChanged(onActiveVideoUserChanged^ cb);
	};

	private ref class CMeetingVideoControllerDotNetWrap sealed : public IMeetingVideoControllerDotNetWrap
	{
	public:
		static property CMeetingVideoControllerDotNetWrap^ Instance
		{
			CMeetingVideoControllerDotNetWrap^ get() { return m_Instance; }
		}

		void BindEvent();

		virtual SDKError MuteVideo();
		virtual SDKError UnmuteVideo();
		virtual SDKError PinVideo(bool bPin, bool bFirstView, unsigned int userid);
		virtual SDKError SpotlightVideo(bool bSpotlight, unsigned int userid);
		virtual SDKError HideOrShowNoVideoUserOnVideoWall(bool bHide);

		virtual void Add_CB_onUserVideoStatusChange(onUserVideoStatusChange^ cb)
		{
			event_onUserVideoStatusChange += cb;
		}

		virtual void Remove_CB_onUserVideoStatusChange(onUserVideoStatusChange^ cb)
		{
			event_onUserVideoStatusChange -= cb;
		}

		virtual void Add_CB_onSpotlightVideoChangeNotification(onSpotlightVideoChangeNotification^ cb)
		{
			event_onSpotlightVideoChangeNotification += cb;
		}

		virtual void Remove_CB_onSpotlightVideoChangeNotification(onSpotlightVideoChangeNotification^ cb)
		{
			event_onSpotlightVideoChangeNotification -= cb;
		}

		virtual void Add_CB_onActiveSpeakerVideoUserChanged(onActiveSpeakerVideoUserChanged^ cb)
		{
			event_onActiveSpeakerVideoUserChanged += cb;
		}

		virtual void Remove_CB_onActiveSpeakerVideoUserChanged(onActiveSpeakerVideoUserChanged^ cb)
		{
			event_onActiveSpeakerVideoUserChanged -= cb;
		}

		virtual void Add_CB_onActiveVideoUserChanged(onActiveVideoUserChanged^ cb)
		{
			event_onActiveVideoUserChanged += cb;
		}

		virtual void Remove_CB_onActiveVideoUserChanged(onActiveVideoUserChanged^ cb)
		{
			event_onActiveVideoUserChanged -= cb;
		}

		void ProcUserVideoStatusChange(unsigned int userId, VideoStatus status);
		void ProcSpotlightVideoChangeNotification(bool bSpotlight, unsigned int userid);
		void ProcActiveSpeakerVideoUserChanged(unsigned int userid);
		void ProcActiveVideoUserChanged(unsigned int userid);

	private:
		event onUserVideoStatusChange^ event_onUserVideoStatusChange;
		event onSpotlightVideoChangeNotification^ event_onSpotlightVideoChangeNotification;
		event onActiveSpeakerVideoUserChanged^ event_onActiveSpeakerVideoUserChanged;
		event onActiveVideoUserChanged^ event_onActiveVideoUserChanged;
		static CMeetingVideoControllerDotNetWrap^ m_Instance = gcnew CMeetingVideoControllerDotNetWrap;
	};
}