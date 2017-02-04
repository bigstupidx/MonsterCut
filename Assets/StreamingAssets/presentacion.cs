using UnityEngine;
using System.Collections;

public class presentacion : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		Application.LoadLevel("PresentacionImagenes");	
#else
	#if UNITY_IPHONE
			Handheld.PlayFullScreenMovie ("logoVertical2.m4v", Color.black, FullScreenMovieControlMode.Hidden);
			Application.LoadLevel("CargaInicial");
	#endif
	#if UNITY_ANDROID
			Application.LoadLevel("PresentacionImagenes");	
	#endif
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
