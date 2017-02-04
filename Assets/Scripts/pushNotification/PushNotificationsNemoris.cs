using UnityEngine;
using System.Collections;

public class PushNotificationsNemoris : MonoBehaviour {
	public int pk_juego = 1;
	public string ProjectID;
	public string APIkey;
	string _registrationId;

	bool tokenSent = false;
	int esIOS = 0;
#if UNITY_ANDROID


	void Start(){
		DontDestroyOnLoad (gameObject);
		GoogleCloudMessaging.cancelAll();
		GoogleCloudMessaging.checkForNotifications();
		GoogleCloudMessagingManager.registrationSucceededEvent += regId =>
		{
			_registrationId = regId;
			StartCoroutine (registrarUsuario());
		};
		GoogleCloudMessaging.register( ProjectID );
	}

#endif

#if UNITY_IOS
	void Start() {
		esIOS = 1;
		/*if (NotificationServices.remoteNotificationCount > 0 || NotificationServices.localNotificationCount > 0)
		{
			RemoteNotification l = new RemoteNotification ();
			l.applicationIconBadgeNumber = -1;
			NotificationServices.p (l);
			
			NotificationServices.CancelAllLocalNotifications ();
			NotificationServices.ClearLocalNotifications (); 
			NotificationServices.ClearRemoteNotifications();
		} */
		DontDestroyOnLoad (gameObject);
		UnityEngine.iOS.NotificationServices.RegisterForNotifications (UnityEngine.iOS.NotificationType.Alert |  UnityEngine.iOS.NotificationType.Badge |  UnityEngine.iOS.NotificationType.Sound);
	}

	void  FixedUpdate () {
		if (!tokenSent) { // tokenSent needs to be defined somewhere (bool tokenSent = false)
			byte[] token   = UnityEngine.iOS.NotificationServices.deviceToken;
			if(token != null) {
				string tokenString =  System.BitConverter.ToString(token).Replace("-", "").ToLower();
				Debug.Log ("OnTokenReived");
				Debug.Log (tokenString);
				tokenSent=true;
				_registrationId = tokenString;
				StartCoroutine (registrarUsuario());
			}
		}
		
	}

#endif
	
	IEnumerator registrarUsuario(){
		WWWForm form = new WWWForm();
		form.AddField( "param0", _registrationId);
		form.AddField( "param1", pk_juego);
		form.AddField( "param2", esIOS);
		WWW download = new WWW( "http://nemorisgames.com/juegos/pushNotificationsNemoris.php?operacion=2", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			Destroy (gameObject);
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			if(retorno == ""){
				//error :(
				//mostrarError("Error de conexion");
				print("error conexion");
				Destroy (gameObject);
			}
			else{
				//exito!
				print ( retorno );
				//Application.LoadLevel(Application.loadedLevelName);
				Destroy (gameObject);
			}
		}	
	}

	void OnGUI(){
		//GUI.Box (new Rect (0f, 0f, 100f, 100f), _registrationId);
	}
}
