using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

#if UNITY_IPHONE
using FB = FacebookBinding;
#endif
#if UNITY_ANDROID
using FB = FacebookAndroid;
#endif

public class FacebookNemoris : MonoBehaviour {
	public UILabel textoDebug;
	public bool showDebugText = true;
	bool logged = false;
	GameObject socialCallbackReceiver;
	facebookCallbackHandler handler;
	public amigo[] amigos;
	
	//almacena el post en caso de no estra loggeado
	string mensajePost = "";
	//deja pendiente el invitar para el caso en que no este logueado
	bool invitando = false;
	
	public UILabel nombreTexto;

	public string[] escenasActivo;
	public GameObject[] elementosFacebook;

	public GameObject listaAmigosObjetos;
	public GameObject botonInvitar;

	public UIPopupList listaAmigos;
	bool amigosActualizados = false;

	string idFacebook;
	static string screenshotFilename = "someScreenshot.png";
	public string linkIos;
	public string linkAndroid;

	public UIToggle changeUser;

	FacebookFriendsResult resultFacebookFriends;
	System.DateTime tiempoPremioLogin;
	System.DateTime tiempoPremioShare;
	public GameObject loginPremio;

	//Localization loc;

	#if UNITY_IPHONE || UNITY_ANDROID
	
	void Start () {
		DontDestroyOnLoad(gameObject);
		
		//loc = Localization.instance;
		
		if(!showDebugText) textoDebug.gameObject.SetActive(false);
		
		FB.init();
		
		FacebookManager.graphRequestCompletedEvent += result =>
		{
			Prime31.Utils.logObject( result );
		};
		print (System.DateTime.Now);
		tiempoPremioLogin = System.DateTime.Parse(PlayerPrefs.GetString("tiempoPremioLogin", "01/01/2015 12:36:18"));
		print (tiempoPremioLogin);
		//tiempoPremioShare = System.DateTime.Parse(PlayerPrefs.GetString("tiempoPremioShare"));
		
		loginPremio.SetActive ((tiempoPremioLogin - System.DateTime.Now) <= System.TimeSpan.Zero);
	}

	void actionSended( string error, bool canceled)
	{
		if( error != null){
			textoDebug.text = error;
			if(handler != null) handler.postError(error);
		}
		else{
			if(canceled) textoDebug.text = "Post cancelado";
			if(handler != null) handler.actionSended(canceled);
		}
	}

	public void recompensarLogin(string num){
		int monedas = int.Parse (num);
		for (int i = 0; i < monedas; i++)
			GetComponent<AudioSource>().PlayDelayed (i * 0.5f);
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + monedas);
		PlayerPrefs.SetString("tiempoPremioLogin", System.DateTime.Now.AddHours(12).ToString());
		loginPremio.SetActive (false);
		//StartCoroutine (sonidoRecompensa (monedas));
	}

	public void recompensarShare(string num){
		int monedas = int.Parse (num);
		for (int i = 0; i < monedas; i++)
			GetComponent<AudioSource>().PlayDelayed (i * 0.5f);
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + monedas);
		GameObject.FindGameObjectWithTag("facebookPublicarBoton").SendMessage("publicacionExitosa");
		//StartCoroutine (sonidoRecompensa (monedas));
	}


	void logueado(string error, FacebookMeResult result)
	{
		if (error != null){
			textoDebug.text = error;
			if(handler != null) handler.errorLogin(error);
		}
		else{
			//IDictionary me = (IDictionary)result;          
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent("facebookUserLog", false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("facebookUserLog");
			#endif
			if((tiempoPremioLogin - System.DateTime.Now) <= System.TimeSpan.Zero) 
				recompensarLogin("3");
			textoDebug.text = "Hola\n" + result.first_name;
			nombreTexto.text = "" + result.first_name;
			nombreTexto.gameObject.GetComponent<UILocalize>().enabled = false;
			idFacebook = result.id;
			Debug.Log(result.name + " " + result.id);
			if(handler != null) handler.logged(result.toDictionary());
			logged = true;
			StartCoroutine("usuarioAddBD", result.id);

			//para el caso de que quiera publicar y no se habia loggeado
			if(mensajePost != "") publicarEnMuro(mensajePost);
			//carga los amigos
			if(!amigosActualizados){
				getFriends();
				listaAmigos.items.Add(" Loading friends...");
				listaAmigos.value = " Loading friends...";
				//StartCoroutine("actualizarAmigosBD");
			}
			if(invitando){
				inviteFriends();
			}
		}
	}

	IEnumerator usuarioAddBD(string id){

		WWWForm form = new WWWForm();
		form.AddField( "campo", "idFacebook" );
		form.AddField( "playerPref", id );
		form.AddField( "pk_usuario", SystemInfo.deviceUniqueIdentifier );
		Debug.Log ("playerPref " + id + ", pk_usuario = " + SystemInfo.deviceUniqueIdentifier);
		WWW download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=1", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			if(retorno == "-1"){
				//error :(
				//mostrarError("Error de conexion");
				print ("error conexion");
			}
			else{
				//exito!
				print("activado");
				//Application.LoadLevel(Application.loadedLevelName);
			}
		}	
	}

	IEnumerator actualizarAmigosBD(){
		foreach (amigo a in amigos) {
			WWWForm form = new WWWForm();
			form.AddField( "idFacebook", idFacebook );
			form.AddField( "idFacebookAmigo", a.idFacebookAmigo );
			WWW download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=3", form);
			yield return download;
			if(download.error != null) {
				print( "Error downloading: " + download.error );
				//mostrarError("Error de conexion");
				yield return false;
			} else {
				if(download.text == "1"){
					a.noActivo = false;
				}
				else{
					a.noActivo = true;
				}
			}
		}
	}
	
	void getFriends(){
		Facebook.instance.getFriends(mostrarAmigos);
	}

	public void publicarFoto(){
		//no funciona
		Application.CaptureScreenshot( screenshotFilename );
		var pathToImage = Application.persistentDataPath + "/" + FacebookComboUI.screenshotFilename;
		if( !System.IO.File.Exists( pathToImage ) )
		{
			Debug.LogError( "there is no screenshot avaialable at path: " + pathToImage );
			return;
		}
		
		var bytes = System.IO.File.ReadAllBytes( pathToImage );
		Facebook.instance.postImage( bytes, "Check out my Barbershot at MonsterCut!", fotoPublicada );
	}

	void fotoPublicada(string error, object result){
		if (error != "") {
			Debug.Log(error);
		}
		else print ("publicacion ok");
	}

	void mostrarAmigos(string error, FacebookFriendsResult result){
		//IDictionary data = result as IDictionary;
		//IList friends = data["data"] as IList;
		amigosActualizados = true;
		textoDebug.text = "mostrando amigos...";
		mostrarAmigosRutina(result);
	}

	void mostrarAmigosRutina(FacebookFriendsResult result){
		resultFacebookFriends = result;
		Handheld.StartActivityIndicator ();
		//amigos = null;
		//amigos = new amigo[result.data.Count];//Mathf.Clamp(result.data.Count, 0, 300)];
		Debug.Log("namigos " + result.data.Count);
		string lista = "";
		textoDebug.text = "";
		for (int i = 0; i < result.data.Count; i++) {
			lista += (string)result.data[i].id + "|";
			textoDebug.text += (string)result.data[i].id + "| ";
		}
		//textoDebug.text = (lista);
		StartCoroutine(obtenerAmigosActivos(lista.Substring(0, lista.Length - 1)));
		//StartCoroutine("buscarAmigosActivos");
		//actualizarListaAmigos();
		//yield return false;
	}

	//IEnumerator buscarAmigosActivos(){
	//	string lista = "";
	//	foreach (amigo a in amigos) {
	//		lista += a.idFacebookAmigo + "|";
	//	}
	//	yield return StartCoroutine("obtenerAmigosActivos", lista.Substring(0, lista.Length - 1));
	//	textoDebug.text = (lista.Substring(0, 100));

		//actualizarListaAmigos();
	//}

	IEnumerator obtenerAmigosActivos(string listaAmigos){
		WWWForm form = new WWWForm();
		form.AddField( "idFacebook", idFacebook );
		form.AddField( "idFacebookAmigos", listaAmigos );
		textoDebug.text = "testing";
		WWW download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=5", form);
		yield return download;
		textoDebug.text = "retorno " + download.text;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			textoDebug.text = download.error;
			//mostrarError("Error de conexion");
			Handheld.StopActivityIndicator ();
			yield return false;
		} else {
			string retorno = download.text;
			if(retorno == ""){
				textoDebug.text = "vacio";
				listaAmigosObjetos.SetActive(false);
				botonInvitar.SetActive(true);
				
				Handheld.StopActivityIndicator ();
				//error :(
				//mostrarError("Error de conexion");
				//a.noActivo = true;
				//print ("amigo " + a.nombre + " no activo");
			}
			else{
				//exito!
				//prefAux = retorno;
				//a.noActivo = false;
				//print ("amigo " + a.nombre + " activo");
				//Application.LoadLevel(Application.loadedLevelName);
				retorno = retorno.Substring(0, retorno.Length - 1);
				textoDebug.text = "ret: " + (retorno);
				string[] amigosFacebookID = retorno.Split(new char[]{'|'});
				amigos = null;
				amigos = new amigo[amigosFacebookID.Length];
				int contador = 0;

				for (int i = 0; i < resultFacebookFriends.data.Count; i++) {
					for(int j = 0; j < Mathf.Clamp(amigosFacebookID.Length, 0, 30); j++){
						if(amigosFacebookID[j] == (string)resultFacebookFriends.data[i].id){
							crearAmigo(contador, (string)resultFacebookFriends.data[i].id, (string)resultFacebookFriends.data[i].name);
							amigos[contador].noActivo = false;
							textoDebug.text += " " + ((string)resultFacebookFriends.data[i].name);
							contador++;
						}
					}
					if(contador > 30) break;
				}

				actualizarListaAmigos();
				listaAmigosObjetos.SetActive(true);
				botonInvitar.SetActive(false);
				textoDebug.text += " lista ok";
				Handheld.StopActivityIndicator ();
			}
		}
	}


	IEnumerator obtenerAmigoActivo(amigo a){
		WWWForm form = new WWWForm();
		form.AddField( "idFacebook", idFacebook );
		form.AddField( "idFacebookAmigo", a.idFacebookAmigo );
		WWW download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=3", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			if(retorno == "0"){
				//error :(
				//mostrarError("Error de conexion");
				a.noActivo = true;
				print ("amigo " + a.nombre + " no activo");
			}
			else{
				//exito!
				//prefAux = retorno;
				a.noActivo = false;
				print ("amigo " + a.nombre + " activo");
				//Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}


	void crearAmigo(int i, string id, string nombre){
		amigos [i] = new amigo ();
		amigos[i].nombre = nombre;
		string[] nombres = nombre.Split(' ');
		amigos[i].primerNombre = nombres[0];
		print("amigo " + amigos [i].primerNombre);
		amigos[i].idFacebookAmigo = id;
	}

	void publicarEnMuro(string mensaje){
		mensajePost = "";
		if(handler != null) handler.posting();
		if(!logged){ 
			login ();
			mensajePost = mensaje;
		}
		else{ 
			//Facebook.instance.postMessage( mensaje, actionSended);
			//var pathToImage = Application.persistentDataPath + "/Assets/" + "gameImage.jpg";
			//var bytes = System.IO.File.ReadAllBytes( pathToImage );
			//gameImage = Resources.Load("imagen", byte[]);
			//Facebook.instance.postImage( bytes , mensaje, actionSended );
			
			// parameters are optional. See Facebook's documentation for all the dialogs and paramters that they support https://developers.facebook.com/docs/reference/dialogs/feed/
			var parameters = new Dictionary<string,string>
			{
				#if UNITY_ANDROID
				{ "link", linkAndroid },
				#endif
				#if UNITY_IPHONE
				{ "link", linkIos },
				#endif
				{ "name", Localization.Get("I'm playing MonsterCut") },
				{ "picture", "http://www.nemorisgames.com/medusa/monsterCutIcono.png" },
				{ "caption", Localization.Get("We're the new barbershop in MonsterCity and everybody wants a haircut. Luckily, we have a team of barbers who specialize in cutting monster hair!") },
				{ "description", mensaje }
			};
			//FB.showDialog("stream.publish", parameters);
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent("facebookPublicar", false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("facebookPublicar");
			#endif
		}
	}
	
	void inviteFriends()
	{
		invitando = false;
		if(!logged){
			invitando = true;
			login ();
		}
		else{ 
			Dictionary<string, string> lParam = new Dictionary<string, string>();
			lParam["message"] = "Download it for free and join me!";
			lParam["title"] = Localization.Get("I'm playing MonsterCut");
			#if UNITY_ANDROID
			FacebookAndroid.showDialog("apprequests", lParam);
			#endif
			#if UNITY_IPHONE
			//FacebookBinding.showDialog("apprequests", lParam);
			#endif
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent("facebookInvitarAmigo", false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("facebookInvitarAmigo");
			#endif
		}
	}
	
	void actualizarListaAmigos(){
		print ("actualizando");
		//listaAmigos.items.Clear();
		textoDebug.text = "actualizando namigos: " + amigos.Length + " ";
		List<string> lista = new List<string>();

		lista.Add(Localization.Get("- Visit your friends -"));
		//listaAmigos.items.Add(Localization.Get("- Visit your friends -"));
		int nAmigosActivos = 0;
		foreach (amigo a in amigos)
		{
			print ("revisando " + a.nombre + " " + a.noActivo);
			textoDebug.text += (" revisando " + a.nombre + " " + a.noActivo);
			if(!a.noActivo){ 
				lista.Add(a.nombre);
				textoDebug.text += "OK";
				//listaAmigos.items.Add(a.nombre);
				nAmigosActivos++;
			}
		}
		textoDebug.text += " activos: " + nAmigosActivos;
		listaAmigos.items = lista;
		listaAmigos.value = Localization.Get ("- Visit your friends -");
		if (nAmigosActivos <= 0) {
			listaAmigos.items[0] += " " + Localization.Get("(no friends)");
			listaAmigos.value = " " + Localization.Get("(no friends)");
		}
		textoDebug.text += " lista: " + listaAmigos.items.Count;
	}
	
	void visitarAmigo(){
		//getFriends ();
		foreach (amigo a in amigos)
		{
			if(a.nombre == listaAmigos.selection){
				Debug.Log( "visitando " + a.idFacebookAmigo + " " + a.nombre);
				#if UNITY_IPHONE
				//FlurryAnalytics.logEvent("facebookVisitarAmigo", false );
				#endif		
				#if UNITY_ANDROID
				FlurryAndroid.logEvent("facebookVisitarAmigo");
				#endif
				PlayerPrefs.SetString("idFacebookAmigo", a.idFacebookAmigo);
				PlayerPrefs.SetString("nombreAmigo", a.primerNombre);
				Application.LoadLevel("Peluqueria");
			}
		}
	}

	void login(){

		#if UNITY_ANDROID
//		FacebookAndroid.loginWithReadPermissions( new string[] { "email", "user_birthday" } );
		#endif
		#if UNITY_IPHONE
//		FacebookBinding.loginWithReadPermissions( new string[] { "email", "user_birthday" } );
		#endif

		#if UNITY_ANDROID
		//if(changeUser.isChecked)
			FB.setSessionLoginBehavior (FacebookSessionLoginBehavior.SSO_ONLY);
		//else
		//	FB.setSessionLoginBehavior (FacebookSessionLoginBehavior.SSO_WITH_FALLBACK);
		#endif
		#if UNITY_IPHONE
		//if(changeUser.isChecked)
		//	FB.setSessionLoginBehavior (FacebookSessionLoginBehavior.WithNoFallbackToWebView);
		//else
			FB.setSessionLoginBehavior (FacebookSessionLoginBehavior.ForcingWebView);
		#endif
		FB.loginWithReadPermissions( new string[] { "email", "user_birthday" });
		if(handler != null) handler.logueando();
		textoDebug.text = "Logging...";
	}
	
	void logout(){
		#if UNITY_ANDROID
		FacebookAndroid.logout();
		#endif
		#if UNITY_IPHONE
		FacebookBinding.logout();
		#endif
		logged = false;
		if(handler != null) handler.logout();
		textoDebug.text = "Logged out";
		nombreTexto.text = Localization.Get("Not Connected");
	}

	void activar(bool b){
		foreach(GameObject g in elementosFacebook){
			g.SetActive(b);	
		}
		//CASO ESPECIAL!!
		foreach(GameObject g in elementosFacebook){
			if(g.name == "ListaAmigos"){ 
				if (Application.loadedLevelName == "Mapa") g.SetActive(true);
				else g.SetActive(false);
			}
		}
		
	}
	
	void OnLevelWasLoaded(int level){
		socialCallbackReceiver = null;
		handler = null;
		socialCallbackReceiver = GameObject.FindWithTag("socialCallbackReceiver");
		if(socialCallbackReceiver != null) handler = socialCallbackReceiver.GetComponent<facebookCallbackHandler>();
		bool encontrado = false;
		foreach(string i in escenasActivo){
			if(Application.loadedLevelName == i) encontrado = true;
		}
		activar(encontrado);

		if (Application.loadedLevelName != "Mapa") {
			GameObject c = GameObject.FindWithTag ("camaraRedesSociales");
			GameObject p = GameObject.FindWithTag ("panelRedesSociales");
			Camera camaraRedesSociales = c.GetComponent<Camera>();
			Transform panelRedesSociales = p.transform;
			if (camaraRedesSociales != null)
				camaraRedesSociales.orthographicSize = 1f;
			if (panelRedesSociales != null)
				panelRedesSociales.localScale = Vector3.one * camaraRedesSociales.orthographicSize; 
		}
		//caso especial
		if(Application.loadedLevelName != "Peluqueria") PlayerPrefs.SetString("idFacebookAmigo", "-1");
	}

	#if UNITY_IPHONE || UNITY_ANDROID
	// Listens to all the events.  All event listeners MUST be removed before this object is disposed!
	void OnEnable()
	{
		FacebookManager.sessionOpenedEvent += sessionOpenedEvent;
		FacebookManager.loginFailedEvent += loginFailedEvent;

		FacebookManager.dialogCompletedWithUrlEvent += dialogCompletedEvent;
		FacebookManager.dialogFailedEvent += dialogFailedEvent;

		FacebookManager.graphRequestCompletedEvent += graphRequestCompletedEvent;
		FacebookManager.graphRequestFailedEvent += facebookCustomRequestFailed;

		FacebookManager.facebookComposerCompletedEvent += facebookComposerCompletedEvent;
		
		FacebookManager.reauthorizationFailedEvent += reauthorizationFailedEvent;
		FacebookManager.reauthorizationSucceededEvent += reauthorizationSucceededEvent;
		
		FacebookManager.shareDialogFailedEvent += shareDialogFailedEvent;
		FacebookManager.shareDialogSucceededEvent += shareDialogSucceededEvent;

		//FacebookManager.restRequestCompletedEvent += restRequestCompletedEvent;
		//FacebookManager.restRequestFailedEvent += restRequestFailedEvent;
		//FacebookManager.facebookComposerCompletedEvent += facebookComposerCompletedEvent;

		//FacebookManager.reauthorizationFailedEvent += reauthorizationFailedEvent;
		//FacebookManager.reauthorizationSucceededEvent += reauthorizationSucceededEvent;
	}


	void OnDisable()
	{
		// Remove all the event handlers when disabled
		FacebookManager.sessionOpenedEvent -= sessionOpenedEvent;
		FacebookManager.loginFailedEvent -= loginFailedEvent;

		FacebookManager.dialogCompletedWithUrlEvent -= dialogCompletedEvent;
		FacebookManager.dialogFailedEvent -= dialogFailedEvent;

		FacebookManager.graphRequestCompletedEvent -= graphRequestCompletedEvent;
		FacebookManager.graphRequestFailedEvent -= facebookCustomRequestFailed;

		FacebookManager.facebookComposerCompletedEvent -= facebookComposerCompletedEvent;
		
		FacebookManager.reauthorizationFailedEvent -= reauthorizationFailedEvent;
		FacebookManager.reauthorizationSucceededEvent -= reauthorizationSucceededEvent;
		
		FacebookManager.shareDialogFailedEvent -= shareDialogFailedEvent;
		FacebookManager.shareDialogSucceededEvent -= shareDialogSucceededEvent;

		//FacebookManager.restRequestCompletedEvent -= restRequestCompletedEvent;
		//FacebookManager.restRequestFailedEvent -= restRequestFailedEvent;
		//FacebookManager.facebookComposerCompletedEvent -= facebookComposerCompletedEvent;

		//FacebookManager.reauthorizationFailedEvent -= reauthorizationFailedEvent;
		//FacebookManager.reauthorizationSucceededEvent -= reauthorizationSucceededEvent;
	}
	#endif
	//lanzado cuando se loguea a FB
	void sessionOpenedEvent()
	{
		textoDebug.text = ( "Successfully logged in to Facebook" );
		//Facebook.instance.graphRequest("me", logueado); 
		Facebook.instance.getMe(logueado);
	}


	void loginFailedEvent( P31Error error )
	{
		textoDebug.text = ( "Facebook login failed: " + error );
	}


	void dialogCompletedEvent( string url )
	{
		actionSended(null, url=="");
		textoDebug.text = ( "dialogCompletedEvent: " + url );
		if(url != "" && url.Contains("post_id"))
			recompensarShare ("2");
	}


	void dialogFailedEvent( P31Error error )
	{
		actionSended(error.ToString(), false);
		textoDebug.text = ( "dialogFailedEvent: " + error );
	}


	void facebokDialogCompleted()
	{
		actionSended(null, false);
		textoDebug.text = ( "facebokDialogCompleted" );
	}


	void graphRequestCompletedEvent( object obj )
	{
		textoDebug.text = ( "graphRequestCompletedEvent" );
		Prime31.Utils.logObject( obj );
	}

	void facebookCustomRequestFailed( P31Error error )
	{
		textoDebug.text = ( "facebookCustomRequestFailed failed: " + error );
	}
	
	
	void facebookComposerCompletedEvent( bool didSucceed )
	{
		textoDebug.text = ( "facebookComposerCompletedEvent did succeed: " + didSucceed );
	}
	
	
	void reauthorizationSucceededEvent()
	{
		textoDebug.text = ( "reauthorizationSucceededEvent" );
	}
	
	
	void reauthorizationFailedEvent( P31Error error )
	{
		textoDebug.text = ( "reauthorizationFailedEvent: " + error );
	}
	
	
	void shareDialogFailedEvent( P31Error error )
	{
		textoDebug.text = ( "shareDialogFailedEvent: " + error );
	}
	
	
	void shareDialogSucceededEvent( Dictionary<string,object> dict )
	{
		textoDebug.text = ( "shareDialogSucceededEvent" );
		Prime31.Utils.logObject( dict );
	}

#endif

}

[System.Serializable]
public class amigo{
	public string idFacebookAmigo;
	public string nombre;
	public string primerNombre;
	public bool noActivo;
}
