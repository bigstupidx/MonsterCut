using UnityEngine;
using System.Collections;

public class seleccionRetrato : MonoBehaviour {
	string[] retratosNombre = {"peluquero1", "peluquero2", "peluquero3"};
	public int forzarSeleccion = -1;
	UISprite sp;
	// Use this for initialization
	void Start () {
		sp = gameObject.GetComponent<UISprite>();
		if(forzarSeleccion != -1) sp.spriteName = retratosNombre[forzarSeleccion];
		else sp.spriteName = retratosNombre[PlayerPrefs.GetInt("peluqueroSeleccionado", 0)];
		print (PlayerPrefs.GetInt("peluqueroSeleccionado", 0) + retratosNombre[PlayerPrefs.GetInt("peluqueroSeleccionado", 0)]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
