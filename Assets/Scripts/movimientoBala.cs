using UnityEngine;
using System.Collections;

public class movimientoBala : MonoBehaviour {
	//public Vector3 direccion;
	public float velocidad = 1f;
	public float tiempoVida = 2f;
	bool activado = false;
	// Use this for initialization
	void Start () {
		//setAngulo(0);
	}
	
	void setAngulo(float a){
		transform.Rotate(0, -a, 0);
		transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
		transform.position += transform.forward * 500;
		activado = true;
	}

	void activar(){
		activado = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!activado)
			return;
		if(tiempoVida > 0f){
			transform.position += transform.forward * velocidad * Time.deltaTime;
			tiempoVida -= Time.deltaTime;
		}
		else Destroy (gameObject);
	}
}
