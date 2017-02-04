using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class leadboltPublicidad : MonoBehaviour {

    /*public string id = "356406014";
    static AndroidJavaObject adController;
     
    void Start ()
    {
     
    Invoke ("Initialize",3);
    Invoke ("LoadAd",5);
    }
     
    void Initialize ()
    {
	    using(AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
	    {
		    using(AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity"))
		    {
		    	adController = new AndroidJavaObject ("com.unity.wrapper.LeadBoltUnity", activity);
		    }
	    }
    }
    void LoadAd()
    {
	    adController.Call ("setAsynchTask", true);
	    adController.Call ("loadAd", id);
    }
    public void OnApplicationQuit()
    {
	    adController.Call ("destroyAd");
    }*/
    
	string interstitialID = "215281267";
	
	string bannerAutomatico = "356406014";
	string banner320x50 = "850752025";
	string banner468x60 = "677090654";
	string banner640x100 = "226165821";
	string banner728x90 = "129558310";
	
	private string bannerMostrar;
	
	public bool activarInterstitial = true;
	public bool activarBanner = false;
	public bool activarBannerAutomatico = false;
#if UNITY_ANDROID
	AndroidJavaClass jc;
	AndroidJavaObject jo;
	static AndroidJavaObject adController;
#endif	
	// Use this for initialization
	void Start(){
#if UNITY_ANDROID
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");	
#endif
		if(Screen.width < 468) bannerMostrar = banner320x50;
		else
			if(Screen.width < 640) bannerMostrar = banner468x60;
			else
				if(Screen.width < 728) bannerMostrar = banner640x100;
				else
					bannerMostrar = banner728x90;
		
		if(activarInterstitial) verInterstitial();
		if(activarBanner) verBanner();
		if(activarBannerAutomatico) verBannerAutomatico();
	}
	
	void verInterstitial(){
#if UNITY_ANDROID
		using(adController = new AndroidJavaObject("com.unity.wrapper.LeadBoltUnity", jo))
		{
			adController.Call("loadAd", interstitialID);
		
			// for Quick Start Ads
			//ad.Call("loadStartAd", "356406014", "YOUR_LB_AUDIO_ID", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Re-Engagement
			// ad.Call("loadReEngagement", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Audio Ad
			// ad.Call("loadAudioAd", "YOUR_LB_SECTION_ID");
		
			// for Audio Track
			// ad.Call("loadAudioTrack", "YOUR_LB_SECTION_ID", 2);
		}
#endif
	}
	
	void verBanner(){
#if UNITY_ANDROID
					
		using(adController = new AndroidJavaObject("com.unity.wrapper.LeadBoltUnity", jo))
		{
			adController.Call("loadAd", bannerMostrar);
		
			// for Quick Start Ads
			//ad.Call("loadStartAd", "356406014", "YOUR_LB_AUDIO_ID", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Re-Engagement
			// ad.Call("loadReEngagement", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Audio Ad
			// ad.Call("loadAudioAd", "YOUR_LB_SECTION_ID");
		
			// for Audio Track
			// ad.Call("loadAudioTrack", "YOUR_LB_SECTION_ID", 2);
		}
#endif
	}
		
	void verBannerAutomatico(){
#if UNITY_ANDROID
		using(adController = new AndroidJavaObject("com.unity.wrapper.LeadBoltUnity", jo))
		{
			adController.Call("loadAd", bannerAutomatico);
			
			// for Quick Start Ads
			//ad.Call("loadStartAd", "356406014", "YOUR_LB_AUDIO_ID", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Re-Engagement
			// ad.Call("loadReEngagement", "YOUR_LB_REENGAGEMENT_ID");
		
			// for Audio Ad
			// ad.Call("loadAudioAd", "YOUR_LB_SECTION_ID");
		
			// for Audio Track
			// ad.Call("loadAudioTrack", "YOUR_LB_SECTION_ID", 2);
		}
#endif
	}
#if UNITY_EDITOR || UNITY_WEBPLAYER
	void OnGUI(){
		if(activarBanner){
			Vector2 bannerDim;
			if(Screen.width < 468) bannerDim = new Vector2(320, 50);
			else
				if(Screen.width < 640) bannerDim = new Vector2(468, 60);
				else
					if(Screen.width < 728) bannerDim = new Vector2(640, 100);
					else
						bannerDim = new Vector2(728, 90);
			GUI.Box(new Rect(Screen.width / 2 - bannerDim.x / 2, 0, bannerDim.x, bannerDim.y), "Banner");
		}
	}
#endif
	
	void OnDestroy(){
#if UNITY_ANDROID
		adController.Call ("destroyAd");
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
