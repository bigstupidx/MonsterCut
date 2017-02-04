using UnityEngine;
using System.Collections;

public class controlPaneles : MonoBehaviour {
	TweenPosition tw;
	// Use this for initialization
	void Start () {
		tw = gameObject.GetComponent<TweenPosition>();
	}
	
	void abrirPanel(){
		tw.Play(true);	
	}
	
	void cerrarPanel(){
		tw.Play(false);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
