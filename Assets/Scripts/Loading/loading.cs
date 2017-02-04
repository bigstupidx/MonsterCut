using UnityEngine;
using System.Collections;

public class loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("cargarEscena");
	}

	IEnumerator cargarEscena(){
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel (PlayerPrefs.GetString("escenaCargar", "Titulo"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
