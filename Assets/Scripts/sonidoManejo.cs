using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class sonidoManejo : MonoBehaviour {
	public enum tipoSonido {panel};
	public AudioClip panelSlide;
	// Use this for initialization
	void Start () {
	
	}

	void playSonido(tipoSonido sonido){
		switch (sonido){
		case tipoSonido.panel:
			GetComponent<AudioSource>().PlayOneShot(panelSlide);
			break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
