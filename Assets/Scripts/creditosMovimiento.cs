using UnityEngine;
using System.Collections;

public class creditosMovimiento : MonoBehaviour {
	public Transform[] elementos; 
	Vector3[] posicionesIniciales;
	public float velocidad;
	public float extension;
	bool activado = false;
	TweenPosition padre;
	public GameObject[] objetosEsconder;
	//bool reseteado = true;
	// Use this for initialization
	void Start () {
		padre = transform.parent.gameObject.GetComponent<TweenPosition> ();
		posicionesIniciales = new Vector3[elementos.Length];
		for(int i = 0; i < elementos.Length; i++){
			posicionesIniciales[i] = elementos[i].localPosition;
		}
	}

	public void reset(){
		activado = false;
		StartCoroutine (reset2 ());
	}

	IEnumerator reset2(){
		yield return new WaitForSeconds (0.5f); 
		for(int i = 0; i < elementos.Length; i++){
			elementos[i].localPosition = posicionesIniciales[i];
		}
		//reseteado = true;
	}

	public void comenzar(){
		//if(reseteado)
		if (padre.direction == AnimationOrTween.Direction.Forward) {
			esconderObjetos (true);
			StartCoroutine (comenzar2 ());
		}
		else 
			esconderObjetos (false);
	}

	void esconderObjetos(bool esconder){
		for (int i = 0; i < objetosEsconder.Length; i++) {
			objetosEsconder[i].SetActive(!esconder);
		}
	}

	public IEnumerator comenzar2(){
		activado = false;
		yield return new WaitForSeconds(3f);
		activado = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!activado)
			return;
		for (int i = 0; i < elementos.Length; i++) {
			elementos[i].localPosition += new Vector3(0f, velocidad * Time.deltaTime, 0f);
			if(elementos[i].localPosition.y > 1000f) elementos[i].localPosition -= new Vector3(0f, extension, 0f);
		}
	}
}
