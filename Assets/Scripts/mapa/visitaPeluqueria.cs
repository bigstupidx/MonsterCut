using UnityEngine;
using System.Collections;

public class visitaPeluqueria : MonoBehaviour {
	public int idUsuario;
	// Use this for initialization
	void Start () {
		
	}
	
	void setId(int id){
		idUsuario = id;
	}
	
	void visitarPeluqueria(){
		PlayerPrefs.SetInt("idUsuarioPeluqueria", idUsuario);
		Application.LoadLevel("Peluqueria");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
