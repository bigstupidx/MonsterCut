using UnityEngine;
using System.Collections;

public class estrella : MonoBehaviour {
	public string[] titulo;
	public string[] mensaje;
	public int puntaje;
	public string nombrePref;
	public string imagen;
	public int idLenguaje = 0;
	// Use this for initialization
	void Start () {
		setIdioma ();
	}
	
	public void setIdioma(){
		if(PlayerPrefs.GetString("Language").Contains("Eng")) idLenguaje = 0;
		if(PlayerPrefs.GetString("Language").Contains("Esp")) idLenguaje = 1;
	}
	
	void presionado(){
		if(idLenguaje == 0) UITooltip.ShowText("[ff540f]"+titulo[idLenguaje]+" ("+puntaje+" points)[-]\n"+mensaje[idLenguaje]);	
		if(idLenguaje == 1) UITooltip.ShowText("[ff540f]"+titulo[idLenguaje]+" ("+puntaje+" puntos)[-]\n"+mensaje[idLenguaje]);	
	}
	
	public string getTitulo(){
		return titulo[idLenguaje];	
	}
	
	public string getMensaje(){
		return mensaje[idLenguaje];	
	}
	
	void soltado(){
		UITooltip.ShowText("");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}