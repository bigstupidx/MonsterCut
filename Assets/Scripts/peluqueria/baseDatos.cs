using UnityEngine;
using System.Collections;

public class baseDatos : MonoBehaviour {
	public string pk_usuario = "";
	string campoAux = "";
	string playerPrefAux = "";
	bool retornoAux = false;
	public string prefAux = "";
	// Use this for initialization
	void Start () {
		pk_usuario = SystemInfo.deviceUniqueIdentifier;
	}
	
	public void registrarPlayerPref(string campo, string playerPref){
		campoAux = campo;
		playerPrefAux = playerPref;
		StartCoroutine("activar");
	}
	
	public IEnumerator obtenerParametroAmigo(string campo){
		WWWForm form = new WWWForm();
	    form.AddField( "campo", campo );
	    form.AddField( "idFacebook", PlayerPrefs.GetString("idFacebookAmigo", "-1") );
	    var download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=2", form);
	    yield return download;
	    if(download.error != null) {
	        print( "Error downloading: " + download.error );
			//mostrarError("Error de conexion");
	        yield return false;
	    } else {
			string retorno = download.text;
			print (retorno);
			if(retorno == "-1"){
				//error :(
				//mostrarError("Error de conexion");
			}
			else{
				//exito!
				prefAux = retorno;
				//Application.LoadLevel(Application.loadedLevelName);
			}
	    }	
	}
	
	IEnumerable activarBase(objetoBasePeluqueria obj){
		WWWForm form = new WWWForm();
	    form.AddField( "campo", obj.nombreBaseDatos );
	    form.AddField( "playerPref", "" );
	    form.AddField( "pk_usuario", pk_usuario );
	    var download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=1", form);
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
			}
			else{
				//exito!
				print("activado");
				//Application.LoadLevel(Application.loadedLevelName);
			}
	    }	
	}
	
	IEnumerator activar(objetoPeluqueria obj){
		WWWForm form = new WWWForm();
	    form.AddField( "campo", obj.nombreBaseDatos );
	    form.AddField( "playerPref", obj.playerPref );
	    form.AddField( "pk_usuario", pk_usuario );
	    var download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=1", form);
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
			}
			else{
				//exito!
				//print("activado");
				//Application.LoadLevel(Application.loadedLevelName);
			}
	    }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
