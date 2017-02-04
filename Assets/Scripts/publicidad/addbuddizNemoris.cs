using UnityEngine;
using System.Collections;

public class addbuddizNemoris : MonoBehaviour {
	//public string[] escenasInterstitial;
	bool activateAds = true;
	public int conteoMuestra = 5;
	public int incremento = 2;
	int conteo = 0;
	// Use this for initialization
		void Start() { 
		AdBuddizBinding.SetAndroidPublisherKey("4a13f0ed-3677-4970-a9b5-6cb4daa2ea4d");
		AdBuddizBinding.SetIOSPublisherKey("71544b76-37cf-4e8e-b9e2-d947444c0426");
		AdBuddizBinding.CacheAds();
		if (PlayerPrefs.HasKey ("activateAdsBuddiz") && activateAds)
			PlayerPrefs.GetInt ("activateAdsBuddiz", 1);
		else PlayerPrefs.GetInt ("activateAdsBuddiz", 0);

		if(activateAds)
			DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int idEscena){
		conteo++;
		if (conteo > conteoMuestra) {
			AdBuddizBinding.ShowAd();
			conteo = 0;
			conteoMuestra += incremento;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

}
