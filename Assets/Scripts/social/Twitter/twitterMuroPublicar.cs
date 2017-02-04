using UnityEngine;
using System.Collections;

public class twitterMuroPublicar : MonoBehaviour {
	public string mensaje;
	GameObject redesSociales;
	// Use this for initialization
	void Start () {
		redesSociales = GameObject.Find("RedesSociales");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void twitterPublicar(){
		redesSociales.SendMessage("twitterPublicarEnMuro", mensaje);
	}
}
