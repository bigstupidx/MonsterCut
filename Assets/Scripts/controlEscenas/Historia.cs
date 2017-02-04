using UnityEngine;
using System.Collections;

public class Historia : MonoBehaviour {
	int nivelActual = 0;
	// Use this for initialization
	void Start () {
		//Playtomic.Log.CustomMetric("historia", "Nivel");
		nivelActual = PlayerPrefs.GetInt ("nivelActual", 1);
	}
	
	void Skip(){
		
		//Playtomic.Log.CustomMetric("skipHistoria", "Boton");
		
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