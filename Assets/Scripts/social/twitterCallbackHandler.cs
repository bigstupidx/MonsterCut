using UnityEngine;
using System.Collections;

public class twitterCallbackHandler : MonoBehaviour {
	string me;
	public UILabel texto;
	// Use this for initialization
	void Start () {
	
	}
	
	public void logged(string informacion){
		me = informacion;
		texto.text = "logueado: " + me;
	}
	
	public void logueando(){
		texto.text = "logueando..";
	}
	
	public void logout(){
		texto.text = "logout";
	}
	
	public void errorLogin(){
		texto.text = "error en login";
	}
	
	public void actionSended(){
		texto.text = "accion enviada";
	}
	
	public void posting(){
		texto.text = "posteando...";
	}
	
	public void postError(string error){
		texto.text = "error en envio: " + error;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
