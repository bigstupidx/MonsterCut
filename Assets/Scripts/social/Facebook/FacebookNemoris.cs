using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Prime31;

/*#if UNITY_IPHONE
using FB = FacebookBinding;
#endif
#if UNITY_ANDROID
using FB = FacebookAndroid;
#endif
*/
public class FacebookNemoris : MonoBehaviour {
	/*public UILabel textoDebug;
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

	//FacebookFriendsResult resultFacebookFriends;
	System.DateTime tiempoPremioLogin;
	System.DateTime tiempoPremioShare;
	public GameObject loginPremio;

	//Localization loc;

	#if UNITY_IPHONE || UNITY_ANDROID
	
	void Start () {
		DontDestroyOnLoad(gameObject);
		
		//loc = Localization.instance;
		
		if(!showDebugText) textoDebug.gameObject.SetActive(false);
		
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
		//Facebook.instance.getFriends(mostrarAmigos);
	}

	public void publicarFoto(){
		//no funciona
    }

	void fotoPublicada(string error, object result){
		if (error != "") {
			Debug.Log(error);
		}
		else print ("publicacion ok");
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
		}
	}
	
	void inviteFriends()
	{
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
		
	}

	void login(){
	}
	
	void logout(){
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
	}


	void OnDisable()
	{
	}
	#endif
	//lanzado cuando se loguea a FB
	void sessionOpenedEvent()
	{
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
*/
}

[System.Serializable]
public class amigo{
	public string idFacebookAmigo;
	public string nombre;
	public string primerNombre;
	public bool noActivo;
}
