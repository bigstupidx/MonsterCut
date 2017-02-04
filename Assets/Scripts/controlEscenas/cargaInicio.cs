using UnityEngine;
using System.Collections;

public class cargaInicio : MonoBehaviour {
	public string escenaSiguiente;
	// Use this for initialization
	void Start () {
		
		PlayerPrefs.SetInt("ejecuciones", PlayerPrefs.GetInt("ejecuciones", 0) + 1);
		/*PlayerPrefs.SetInt ("monedas", 587);
		PlayerPrefs.SetInt("monstruo0desbloqueado", 1);
		PlayerPrefs.SetInt("monstruo1desbloqueado", 1);
		PlayerPrefs.SetInt("monstruo2desbloqueado", 1);
		PlayerPrefs.SetInt("monstruo3desbloqueado", 1);*/
	}
	
	// Update is called once per frame
	void Update () {
		//if(Time.timeSinceLevelLoad > 1f) 
			Application.LoadLevel(escenaSiguiente);
	}
}
