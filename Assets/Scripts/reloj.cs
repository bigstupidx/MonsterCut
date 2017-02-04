using UnityEngine;
using System.Collections;

public class reloj : MonoBehaviour {
	//tk2dTextMesh textMesh;
	float tiempoVida = -1f;
	
	float numeroAnterior = 0f;
	float numeroActual = 0f;
	// Use this for initialization
	void Start () {
	//	textMesh = gameObject.GetComponent<tk2dTextMesh>();
	}
	
	void setTiempoVida(float tiempo){
		tiempoVida = tiempo + Time.time;
	}
	
	void setTexto(float t){
		numeroActual = Mathf.CeilToInt(t * 10f) / 10f;
		if(numeroActual != numeroAnterior){
			numeroAnterior = numeroActual;
	//		textMesh.text = "" + Mathf.FloorToInt(t) + "." + ((int)(t * 10f) - (int)(t) * 10);
	//		textMesh.Commit();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tiempoVida != -1){
			if( Time.time >= tiempoVida) Destroy (gameObject);
			setTexto(tiempoVida - Time.time);
		}
	}
}
