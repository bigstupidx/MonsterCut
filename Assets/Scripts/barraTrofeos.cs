using UnityEngine;
using System.Collections;

public class barraTrofeos : MonoBehaviour {
	public estrella[] estrellas;
	
	public int maxPuntaje = 10000;
	public UISlider barraPrincipal;
	public UISlider barraPrincipalAux;
	public UISlider barraLogro;
	public UISlider barraLogroAux;
	public TweenPosition transicion;
	public int puntajeLogroSiguiente = 0;
	public int puntajeLogroAnterior = 0;
	public bool mostrando = false;
	public estrella botonLogroSiguiente;
	public GameObject estrellaBoton;
	public GameObject musicaBoton;
	
	public TweenPosition logroPanel;
	public UISprite logroImagen;
	public UILabel logroTitulo;
	public UILabel logroMensaje;
	
	public int indiceEstrellaActual = 0;
	
	central centralScript;
	public bool sonidoActivado = true;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.root.gameObject);
		
		musicaBoton.SetActive(false);
		
		sonidoActivado = (PlayerPrefs.GetInt("sonido", 1) == 1) ? true : false;
		sonidoActualizar();
		
		for(int i = 0; i < estrellas.Length; i++){
			float porcentaje = (float) estrellas[i].puntaje / (float) maxPuntaje;
			estrellas[i].transform.localPosition = new Vector2( estrellas[i].transform.localPosition.x , barraPrincipal.transform.localPosition.y + 1f * porcentaje);
			estrellas[i].gameObject.SetActive(false);
		}
		actualizarPuntajeTotal();
		actualizarPuntajeTotalAux(0);
		actualizarPanelLogro();
		//eliminar
		GameObject c = GameObject.FindWithTag("central");
		if(c != null){
			print ("encontrado");
			centralScript = c.GetComponent<central>();	
		}
		//hasta aqui
		transicion.Play(false);
		logroPanel.Play(false);
	}
	
	public Vector2 obtenerLimitesBarra(){
		return new Vector2(puntajeLogroAnterior, puntajeLogroSiguiente);
	}
	
	void logroCerrar(){
		PlayerPrefs.SetInt(estrellas[indiceEstrellaActual].nombrePref + "Cerrado", 1);
		
		//Playtomic.Log.CustomMetric("logro"+estrellas[indiceEstrellaActual].nombrePref, "Logro");
		
		actualizarPanelLogro();
		logroPanel.Play(false);	
	}
	
	void actualizarPuntajeTotalAux(int puntajeNivel){
		barraPrincipalAux.sliderValue = (float) (PlayerPrefs.GetInt("puntajeTotalJuego", 0) + puntajeNivel) / (float) maxPuntaje;
		//print (PlayerPrefs.GetInt("puntajeTotalJuego", 0) +"-"+ puntajeLogroAnterior +"+"+ puntajeNivel +"/"+ puntajeLogroSiguiente +"-"+ puntajeLogroAnterior);
		barraLogroAux.sliderValue = (float) (PlayerPrefs.GetInt("puntajeTotalJuego", 0) - puntajeLogroAnterior + puntajeNivel) / (float)(puntajeLogroSiguiente - puntajeLogroAnterior);	
	}
	
	
	void actualizarPuntajeTotal(){
		barraPrincipal.sliderValue = (float) PlayerPrefs.GetInt("puntajeTotalJuego", 0) / (float) maxPuntaje;
		puntajeLogroAnterior = 0;
		puntajeLogroSiguiente = maxPuntaje;
		for(int i = 0; i < estrellas.Length; i++){
			if(estrellas[i].puntaje > PlayerPrefs.GetInt("puntajeTotalJuego", 0)){
				puntajeLogroSiguiente = estrellas[i].puntaje;
				botonLogroSiguiente.puntaje = estrellas[i].puntaje;
				botonLogroSiguiente.titulo = estrellas[i].titulo;
				botonLogroSiguiente.mensaje = estrellas[i].mensaje;
				if(i > 0) puntajeLogroAnterior = estrellas[i - 1].puntaje;
				break;
			}
		}
		//revisa si hay un desbloqueo
		for(int i = 0; i < estrellas.Length; i++){
			if(estrellas[i].puntaje < PlayerPrefs.GetInt("puntajeTotalJuego", 0)){
				if(PlayerPrefs.GetInt(estrellas[i].nombrePref+ "Cerrado", 0) == 0){
					PlayerPrefs.SetInt(estrellas[i].nombrePref, 1);
					indiceEstrellaActual = i;
					actualizarPuntajeTotalAux(0);
					
					logroPanel.Play(true);
					//estrellaBoton.SendMessage("Play", true);
				}	
			}
		}	
		if(puntajeLogroSiguiente == maxPuntaje){
			botonLogroSiguiente.gameObject.SetActive(false);
			barraLogro.gameObject.SetActive(false);
			barraLogroAux.gameObject.SetActive(false);
		}
		else{
			print (PlayerPrefs.GetInt("puntajeTotalJuego", 0) +"-"+ puntajeLogroAnterior +"/"+ puntajeLogroSiguiente +"-"+ puntajeLogroAnterior);
			barraLogro.sliderValue = (float) (PlayerPrefs.GetInt("puntajeTotalJuego", 0) - puntajeLogroAnterior) / (float)(puntajeLogroSiguiente - puntajeLogroAnterior);	
		}
	}
	
	void actualizarPanelLogro(){
		bool encontrado = false;
		for(int i = 0; i < estrellas.Length; i++){
			if(estrellas[i].puntaje > PlayerPrefs.GetInt("puntajeTotalJuego", 0)){
				//if(PlayerPrefs.GetInt(estrellas[i].nombrePref + "Cerrado", 0) == 0){
					indiceEstrellaActual = i;
					logroImagen.spriteName = estrellas[i].imagen;
					logroTitulo.text = estrellas[i].getTitulo();
					logroMensaje.text = estrellas[i].getMensaje();
					encontrado = true;	
				
					break;
				//}	
			}
		}
		
		if(!encontrado){
			logroImagen.spriteName = "retrato";
			logroImagen.color = Color.black;
			if(PlayerPrefs.GetString("Language").Contains("Eng")){
				logroTitulo.text ="Awesome!";
				logroMensaje.text = "You had unlock it all! New features very soon.";
			}
			if(PlayerPrefs.GetString("Language").Contains("Esp")){
				logroTitulo.text = "Asombroso!";
				logroMensaje.text = "Lo lograste todo! muy pronto nuevos premios.";	
			}
		}
	}
	
	void sonidoActualizar(){
		AudioSource a = Camera.main.gameObject.GetComponent<AudioSource>();
		if(a != null)
			a.volume = sonidoActivado ? 1 : 0;
		if(centralScript){
			
		}
	}
	
	void toggleSonido(){
		sonidoActivado = !sonidoActivado;
		
		/*if(sonidoActivado)
			Playtomic.Log.CustomMetric("sonidoActivado", "Boton");
		else
			Playtomic.Log.CustomMetric("sonidoDesactivado", "Boton");
		*/
		PlayerPrefs.SetInt("sonido", sonidoActivado ? 1 : 0);
		sonidoActualizar();
	}
	
	void toggleBarras(){
		if(centralScript != null && centralScript.estado == 0) return;
		if(centralScript != null && centralScript.pausaPreguntaGUI.direction != AnimationOrTween.Direction.Reverse && centralScript.pausaPreguntaGUI.gameObject.activeSelf) return;
		mostrando = !mostrando;
		
		/*if(mostrando)
			Playtomic.Log.CustomMetric("logrosActivado", "Boton");
		else
			Playtomic.Log.CustomMetric("logrosDesactivado", "Boton");
		*/
		if(mostrando){
			print ("aqui");
			estrellaBoton.transform.localEulerAngles = Vector3.zero;
			TweenRotation tr = estrellaBoton.GetComponent<TweenRotation>();
			tr.enabled = false;
			TweenColor tc = estrellaBoton.GetComponent<TweenColor>();
			tc.enabled = false;
		
			if(centralScript != null){
				if(centralScript.estado == 0 || centralScript.estado == 1)
					actualizarPuntajeTotalAux(centralScript.puntajeNivel);
				//centralScript.pausar();
			}
		}
		if(centralScript!=null) centralScript.pausar();
		for(int i = 0; i < estrellas.Length; i++){ 
			estrellas[i].gameObject.SetActive(mostrando);
			estrellas[i].setIdioma();
		}
		botonLogroSiguiente.setIdioma();
		
		transicion.Play(mostrando);
	}
	
	void OnLevelWasLoaded(){
		actualizarPuntajeTotal();
		GameObject c = GameObject.FindWithTag("central");
		if(c != null){
			print ("encontrado");
			centralScript = c.GetComponent<central>();	
		}
		sonidoActualizar();
		if(mostrando) toggleBarras();
		Time.timeScale = 1.0f;
		
		if(Application.loadedLevelName == "Titulo"){
			musicaBoton.SetActive(false);	
		}
		else musicaBoton.SetActive(true);
		
		//actualiza el idioma del panel de logros
		if(Application.loadedLevelName == "Seleccion"){
			for(int i = 0; i < estrellas.Length; i++){
				if(estrellas[i].puntaje > PlayerPrefs.GetInt("puntajeTotalJuego", 0)){
					estrellas[i].setIdioma();
					logroTitulo.text = estrellas[i].getTitulo();
					logroMensaje.text = estrellas[i].getMensaje();
					break;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

