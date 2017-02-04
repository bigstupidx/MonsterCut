using UnityEngine;
using System.Collections;

public class Titulo : MonoBehaviour {
	public TweenPosition panelPrincipal;
	public TweenPosition panelNombre;
	public UIInput nombreInput;
	public UIPopupList lenguajeSeleccion;
	//public TweenPosition panelOpciones;
	bool mostrandoNombre = false;
	public GameObject tutorial;

	public GameObject resetBoton;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		//Playtomic.Log.CustomMetric("titulo", "Nivel");
		resetBoton.SetActive(PlayerPrefs.HasKey("puntajeTotal"));
		PlayerPrefs.SetInt ("puntajeTotal", 0);
		PlayerPrefs.SetInt ("poder1exp", 0);
		PlayerPrefs.SetInt ("poder2exp", 0);
		PlayerPrefs.SetInt ("nivelActual", 1);
		PlayerPrefs.SetInt ("peluquero0", 1);
		PlayerPrefs.SetInt ("monstruo0desbloqueado", 1);
		panelNombre.to.x = ((800 * Screen.width) / (2 * Screen.height));
		nombreInput.text = PlayerPrefs.GetString("nombre", "Player");
		if(!PlayerPrefs.HasKey("nombre")){
			mostrarPanelNombre(true);
		}
		else tutorial.SetActive(true);

		//Playtomic.Log.CustomMetric("lenguaje"+PlayerPrefs.GetString("Language", "nada"), "Lenguaje");
	}

	public void reset(){
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel ("Titulo");
	}
	
	/*void CambiarIdioma(){
		print (lenguajeSeleccion.selection);
		
		PlayerPrefs.SetString("Language", lenguajeSeleccion.selection);
	}*/

	public void rate(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("rate", false );
		Application.OpenURL ("https://itunes.apple.com/us/app/monstercut/id952900651?l=es&ls=1&mt=8");
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("rate");
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.nemorisgames.monstercut");
		#endif

	}

	public void botonQuick(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("quickPlay", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("quickPlay");
		#endif
		PlayerPrefs.SetInt("training", 1);
		PlayerPrefs.SetString("escenaCargar", "Nivel");
		Application.LoadLevel("Loading");
	}
	
	public void botonPlay(){
		//Playtomic.Log.Play();
		//if(PlayerPrefs.GetInt("peluquero1", 0) == 1) 
			Application.LoadLevel("Seleccion");
		//else Application.LoadLevel("Mapa");	
	}
	
	public void mostrarPanelNombre(bool adelante){
		mostrandoNombre = adelante;
		if(adelante){
			panelPrincipal.enabled = adelante;
			panelNombre.enabled = adelante;
		}
		else tutorial.SetActive(true);
		panelPrincipal.Play(adelante);
		panelNombre.Play(adelante);
		gameObject.SendMessage ("playSonido", sonidoManejo.tipoSonido.panel);
		//panelOpciones.Play(adelante);
	}
	
	public void showName(){
		if(!mostrandoNombre)
			mostrarPanelNombre(true);
		else nombreOk();
	}
	
	public void nombreOk(){
		if(nombreInput.text != ""){
			PlayerPrefs.SetString("nombre", nombreInput.text);
			mostrarPanelNombre(false);
		}

//		GiftizBinding.missionComplete();
	}

	void botonReleased(){
		UITooltip.ShowText("");	
	}

	public void activarTutorial(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("activarTutorial", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("activarTutorial");
		#endif
		PlayerPrefs.SetInt ("tutorial1", 0);
		PlayerPrefs.SetInt ("tutorial2", 0);
		PlayerPrefs.SetInt ("tutorial3", 0);
		PlayerPrefs.SetInt ("tutorial4", 0);
		PlayerPrefs.SetInt ("tutorial5", 0);
		PlayerPrefs.SetInt ("tutorial6", 0);
		PlayerPrefs.SetInt ("tutorial7", 0);
		Application.LoadLevel (Application.loadedLevelName);
	}
	
	void tutorialPressed(){
		UITooltip.ShowText("Tutorial");	
	}
	void minigamesPressed(){
		if(PlayerPrefs.GetString("Language").Contains("Esp")) 
			UITooltip.ShowText("Minijuegos");
		else
			UITooltip.ShowText("Minigames");	
	}
	void trofeosPressed(){
		Application.LoadLevel ("Store");
		/*if(PlayerPrefs.GetString("Language").Contains("Esp")) 
			UITooltip.ShowText("Trofeos y Desbloqueos");
		else
			UITooltip.ShowText("Trophies and Unlocks");	*/
		
	}
	void rankingPressed(){
		UITooltip.ShowText("Ranking");	
	}

	public void botonGiftiz(){
		//GiftizBinding.buttonClicked(); // Giftiz Button has been clicked
	}
	
	void OnGUI(){
		/*if(GUI.Button(new Rect(0,Screen.height-30, 100, 30), "Reset")){
			PlayerPrefs.DeleteAll();
			Application.LoadLevel(Application.loadedLevelName);

		}*/
		//if (GUI.Button(new Rect(0f, 0f, 200f, 60f), "boton")) {
		//	GiftizBinding.buttonClicked(); // Giftiz Button has been clicked
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape) && !mostrandoNombre) {
			Application.Quit();
		}
	}
}
