using UnityEngine;
using System.Collections;

public class tutorialControl : MonoBehaviour {
	public panelTutorial[] paneles;
	int indice = 0;
	public string prefTutorial = "tutorial1";
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt(prefTutorial, 0) == 1) skip ();
		else{
			paneles[0].gameObject.SetActive(true);
			Time.timeScale = paneles[0].pausar?0f:1f;
		}
	}

	void evento(int indiceEvento){
		if(indice == indiceEvento && paneles[indice].esperaEvento && (paneles[indice].mensajeTerminado() || paneles [indice].eventoInmediato) ){
			paneles[indice].terminarMensaje();
			print ("evento " + indice + " ev " + indiceEvento );
			next ();
		}
	}

	public void skip(){
		PlayerPrefs.SetInt(prefTutorial, 1);
		Time.timeScale = 1f;
		paneles[indice].mostrarBotones(true);
		gameObject.SetActive(false);
	}

	public void next(){
		if (paneles [indice].mensajeTerminado ()) {
			paneles[indice].mostrarBotones(true);
			print ("botones mostrados");
			paneles[indice].gameObject.SetActive (false);
			print ("panel desactivo");
			indice++;
			if (indice < paneles.Length){
				paneles[indice].gameObject.SetActive (true);
				print("paneles nuevo activo");
			}
			if (indice == paneles.Length - 1) {
					print ("final");
					PlayerPrefs.SetInt (prefTutorial, 1);
			}
			Time.timeScale = paneles [indice].pausar ? 0f : 1f;
		} 
		else {
			paneles[indice].terminarMensaje();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
