using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Prime31;

public class twitterNemoris : MonoBehaviour {
	/*public UILabel textoDebug;
	public bool showDebugText = true;
	bool logged = false;
	string mensajePost = "";
	GameObject socialCallbackReceiver;
	twitterCallbackHandler handler;

	void Start () {
		DontDestroyOnLoad(gameObject);
		
		if(!showDebugText) textoDebug.gameObject.SetActive(false);
		#if UNITY_ANDROID
		TwitterAndroid.init("x8U66wyGqgnheaiGIPKLag", "jk1qKzcT9HXLKTi5OLJkf0Xw8mcdxkcrwHrbMTV0U" );
		#endif
		#if UNITY_IPHONE
		TwitterBinding.init("x8U66wyGqgnheaiGIPKLag", "jk1qKzcT9HXLKTi5OLJkf0Xw8mcdxkcrwHrbMTV0U" );
		#endif
	}
	
	void OnLevelWasLoaded(int level){
		socialCallbackReceiver = null;
		handler = null;
		socialCallbackReceiver = GameObject.FindWithTag("socialCallbackReceiver");
		if(socialCallbackReceiver != null) handler = socialCallbackReceiver.GetComponent<twitterCallbackHandler>();
	}
	
	void twitterLogin(){
		#if UNITY_ANDROID
		TwitterAndroid.showLoginDialog();
		#endif
		#if UNITY_IPHONE
		//TwitterBinding.showOauthLoginDialog();
		#endif
		if(handler != null) handler.logueando();
		textoDebug.text = "Logging...";
	}
	
	void twitterLogout(){
		#if UNITY_ANDROID
		TwitterAndroid.logout();
		#endif
		#if UNITY_IPHONE
		//TwitterBinding.logout();
		#endif
		logged = false;
		if(handler != null) handler.logout();
		textoDebug.text = "Logged out";
	}
	
	void logueado(string usuario)
    {
        if (usuario == null){
            textoDebug.text = "error";
			if(handler != null) handler.errorLogin();
        }
        else{        
            textoDebug.text = "Hola\n" + usuario;
			if(handler != null) handler.logged(usuario);
            logged = true;
			//para el caso de que quiera publicar y no se habia loggeado
			if(mensajePost != "") twitterPublicarEnMuro(mensajePost);
        }
    }
	
	void twitterPublicarEnMuro(string mensaje){
		mensajePost = "";
		if(handler != null) handler.posting();
		if(!logged){ 
			twitterLogin ();
			mensajePost = mensaje;
		}
		else{ 
			// parameters are optional. See Facebook's documentation for all the dialogs and paramters that they support https://developers.facebook.com/docs/reference/dialogs/feed/
			var dict = new Dictionary<string, string>();
			dict.Add("status", mensaje + " http://www.nemorisgames.com " + "via @nemorisgames");
			#if UNITY_ANDROID
			TwitterAndroid.performRequest("post", "1.1/statuses/update.json", dict);
			#endif
			#if UNITY_IPHONE
			TwitterBinding.performRequest( "POST", "1.1/statuses/update.json", dict );
			#endif
		}
	}
	
	void actionSended( object response, string error)
	{
		if( error != null){
			textoDebug.text = error;
			if(handler != null) handler.postError(error);
		}
		else{
			textoDebug.text = "Post publicado";
			if(handler != null) handler.actionSended();
		}
	}
	
	void Update () {
	
	}
    */

}
