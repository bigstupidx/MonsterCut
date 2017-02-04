using UnityEngine;
using System.Collections;

public class Seleccion : MonoBehaviour {
	public GameObject[] hireButton;
	public GameObject[] unlock;
	int retratoSeleccionado = 0;
	/*public UIButton seleccionarBoton;
	public retrato[] retratos;
	public UILabel nombreLabel;
	public UILabel unlockLabel;
	public UILabel descripcion;
	public UISprite poder1;
	public UILabel poder1descripcion;
	public UISprite poder2;
	public UILabel poder2descripcion;
	int idLenguaje = 0;*/
	// Use this for initialization
	void Start () {
		//Playtomic.Log.CustomMetric("seleccion", "Nivel");
		for(int i = 0; i < 3; i++){
			if(PlayerPrefs.GetInt("peluquero"+i, 0) == 1) unlock[i].SetActive(false);
			else hireButton[i].SetActive(false);
		}
	}

	void swipeBack(){
		gameObject.SendMessage ("playSonido", sonidoManejo.tipoSonido.panel);
	}

	void swipe(){	
		gameObject.SendMessage ("playSonido", sonidoManejo.tipoSonido.panel);
		GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 0, SendMessageOptions.RequireReceiver);}
	}
	
	void seleccionarPeluquero1(){
		seleccionar(0);	
	}
	void seleccionarPeluquero2(){
		seleccionar(1);	
	}
	void seleccionarPeluquero3(){
		seleccionar(2);	
	}
	//void seleccionarPeluquero4(){
	//	seleccionar(3);	
	//}
	
	void seleccionar(int id){
	//	retratos[retratoSeleccionado].seleccionado(false);
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent( "peluqueroSeleccionado" + id, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("peluqueroSeleccionado" + id);
		#endif
		retratoSeleccionado = id;
		peluqueroSeleccionado();
	/*	retratos[retratoSeleccionado].seleccionado(true);
		nombreLabel.text = retratos[retratoSeleccionado].nombre;
		descripcion.text = retratos[retratoSeleccionado].descripcion[idLenguaje];
		poder1.spriteName = retratos[retratoSeleccionado].poder1imagen;
		poder1.transform.localScale = new Vector3(poder1.sprite.outer.width, poder1.sprite.outer.height, 1);
		poder2.spriteName = retratos[retratoSeleccionado].poder2imagen;
		poder2.transform.localScale = new Vector3(poder2.sprite.outer.width, poder2.sprite.outer.height, 1);
		poder1descripcion.text = retratos[retratoSeleccionado].poder1descripcion[idLenguaje];
		poder2descripcion.text = retratos[retratoSeleccionado].poder2descripcion[idLenguaje];*/
	}
	
	void volver(){
		PlayerPrefs.SetString("escenaCargar", "Titulo");
		Application.LoadLevel("Loading");
	}
	
	void peluqueroSeleccionado(){
		//Playtomic.Log.CustomMetric("peluquero"+retratoSeleccionado+"Sel", "Boton");
		PlayerPrefs.SetInt("peluqueroSeleccionado", retratoSeleccionado);
		Application.LoadLevel("SeleccionMundo");	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Titulo");
		}
		if(Input.GetKeyUp(KeyCode.RightArrow)) swipe();
		if (Input.GetKeyUp (KeyCode.LeftArrow))
						swipeBack ();
	}
}
