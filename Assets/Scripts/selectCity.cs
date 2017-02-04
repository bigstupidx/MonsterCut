using UnityEngine;
using System.Collections;

public class selectCity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void selectCity1(){
		Application.LoadLevel("Mapa");
	}

	void volver(){
		Application.LoadLevel ("Seleccion");
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Seleccion");
		}
	}
}
