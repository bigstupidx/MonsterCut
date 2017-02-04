using UnityEngine;
using System.Collections;

public class amigoPeluqueria : MonoBehaviour {
	public int idAmigo = -1;
	public GameObject[] objetosInvitar;
	public GameObject[] objetosVisitar;
	// Use this for initialization
	void Start () {
		cargarLayer();
	}
	
	void cargarLayer(){
		foreach(GameObject g in objetosInvitar){
			g.SetActive(idAmigo < 0);
		}
		foreach(GameObject g in objetosVisitar){
			g.SetActive(idAmigo >= 0);
		}
	}
	
	void invitarAmigo(){
		gameObject.SendMessage("inviteFriends");
	}
	
	void visitarAmigo(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
