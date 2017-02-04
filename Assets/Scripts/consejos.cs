using UnityEngine;
using System.Collections;

public class consejos : MonoBehaviour {
	public string[] mensajes;
	UILabel texto;
	// Use this for initialization
	void Start () {
		texto = gameObject.GetComponent<UILabel>();
		reset();
	}
	
	void reset(){
		//Localization loc = Localization.instance;
		int rand = Random.Range (0, mensajes.Length);
		mensajes [rand] = Localization.Get(mensajes [rand]);
		texto.text = mensajes[rand];	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
