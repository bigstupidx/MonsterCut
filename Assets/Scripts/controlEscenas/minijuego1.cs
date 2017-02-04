using UnityEngine;
using System.Collections;

public class minijuego1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Playtomic.Log.CustomMetric("minijuego1", "Nivel");
	}
	
	void Skip(){
		
		//Playtomic.Log.CustomMetric("skipMinijuego1", "Boton");
		
		siguienteEscena();
	}
	
	void siguienteEscena(){
		if(PlayerPrefs.GetInt ("monstruo1desbloqueado", 0) == 1 && globalVariables.estacionesNivel[PlayerPrefs.GetInt("nivelActual", 1) - 1] > 1){
			Application.LoadLevel ("Ruleta");
		}
		else Application.LoadLevel("Loading");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
