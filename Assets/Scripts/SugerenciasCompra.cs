using UnityEngine;
using System.Collections;

public class SugerenciasCompra : MonoBehaviour {
	public GameObject fondo;
	public GameObject panel;
	public GameObject explosionEstrellasPrefab;

	public UILabel titulo;
	public UILabel descripcion;
	public UISprite imagen;
	public UILabel labelComprar;
	public UILabel labelComprarAntiguo;

	public objetoSugerido[] objetos;
	public int indiceActual = 0;
	
	public GameObject panelComprado;
	public GameObject fondoComprado;
	public UILabel tituloComprado;
	public UISprite imagenComprado;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt ("monedas", 1000); 
		DontDestroyOnLoad (gameObject);

		indiceActual = PlayerPrefs.GetInt ("sugerenciaIndiceActual", 0);
		//yield return new WaitForSeconds (5f);
		//mostrarPanel ();
	}

	public void mostrarPanelComprado(string titulo, UISprite sprite){
		print (titulo + " " + sprite.spriteName);
		tituloComprado.text = titulo;
		imagenComprado.atlas = sprite.atlas;
		imagenComprado.spriteName = sprite.spriteName;
		
		fondoComprado.SetActive (true);
		panelComprado.gameObject.SetActive (true);
		//panel.PlayForward ();

		iniciarExplosion ();
	}

	void iniciarExplosion(){
		GameObject g = (GameObject)Instantiate (explosionEstrellasPrefab);
		g.GetComponent<AudioSource>().PlayDelayed (0.5f);
		g.transform.parent = panel.transform.parent;
		g.transform.localScale = Vector3.one;
	}

	public void mostrarPanel(){
		for(int i = indiceActual; i < objetos.Length; i++){
			//0: no utilizada
			//1: rechazada
			//2: utilizada
			if(PlayerPrefs.GetInt ("sugerenciaUtilizada" + i, 0) >= 1){
				print ("inspeccion " + i);
				indiceActual++;
				if(indiceActual >= objetos.Length) indiceActual = 0;
			}
			else break;
		}
		if (objetos [indiceActual].costo > PlayerPrefs.GetInt ("monedas", 0) || PlayerPrefs.GetInt(objetos[indiceActual].nombrePrefab, 0) != objetos[indiceActual].valorRequisito || (objetos[indiceActual].nombrePrefabRequisito != "" && PlayerPrefs.GetInt(objetos[indiceActual].nombrePrefabRequisito, 0) <= 0)){
			print ("no cumple requisitos");
			return;
		}

		titulo.text = Localization.Get(objetos [indiceActual].nombre);
		descripcion.text = Localization.Get(objetos [indiceActual].descripcion);
		imagen.atlas = objetos [indiceActual].atlas;
		imagen.spriteName = objetos [indiceActual].sprite;
		labelComprar.text = "" + objetos [indiceActual].costo;
		labelComprarAntiguo.text = "" + objetos [indiceActual].costoAntiguo;

		fondo.SetActive (true);
		panel.gameObject.SetActive (true);
		//panel.PlayForward ();
		iniciarExplosion ();

	}

	public void cerrarPanelComprado(){
		panelComprado.gameObject.SetActive (false);
		fondoComprado.SetActive (false);
	}

	public void cerrarPanel(){
		//panel.ResetToBeginning ();
		
		panel.gameObject.SetActive (false);
		fondo.SetActive (false);

		indiceActual++;
		if(indiceActual >= objetos.Length) indiceActual = 0;
		PlayerPrefs.SetInt ("sugerenciaIndiceActual", indiceActual);
	}

	public void comprar(){
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) - objetos [indiceActual].costo);
		GameObject m = GameObject.FindGameObjectWithTag ("monedasLabel");
		if (m != null)
			m.GetComponent<UILabel> ().text = "" + PlayerPrefs.GetInt ("monedas", 0);
		PlayerPrefs.SetInt (objetos [indiceActual].nombrePrefab, PlayerPrefs.GetInt (objetos [indiceActual].nombrePrefab, 0) + 1);
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("sugerenciaTomada"+indiceActual, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("sugerenciaTomada"+indiceActual);
		#endif
		PlayerPrefs.SetInt ("sugerenciaUtilizada" + indiceActual, 2);
		if (objetos [indiceActual].callbackCentral != "") {
			GameObject g = GameObject.FindGameObjectWithTag("central");
			if(g != null) g.SendMessage(objetos [indiceActual].callbackCentral);
		}
		mostrarPanelComprado (titulo.text, imagen);
		cerrarPanel ();
	}

	public void cancelar(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("sugerenciaCancelada"+indiceActual, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("sugerenciaCancelada"+indiceActual);
		#endif
		cerrarPanel ();
	}

	public void verEnStore(){
		PlayerPrefs.SetInt (objetos [indiceActual].nombrePrefab, PlayerPrefs.GetInt (objetos [indiceActual].nombrePrefab, 0) + 1);
		cerrarPanel ();
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("sugerenciaIrStore"+indiceActual, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("sugerenciaIrStore"+indiceActual);
		#endif
		Application.LoadLevel ("Store");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class objetoSugerido{
	public string nombre;
	public string descripcion;
	public string nombrePrefab;
	public string nombrePrefabRequisito;
	public int valorRequisito = 0;
	public UIAtlas atlas;
	public string sprite;
	public int costo;
	public int costoAntiguo;
	public string callbackCentral;

}