using UnityEngine;
using System.Collections;

public class dragMousePan : MonoBehaviour {
	public Vector3 posicionInicial;
	public Camera cam;
	public Rect limites;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
		//	posicionInicial = Input.touchCount>0?(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0)):Input.mousePosition;
		//}
		if (Input.GetMouseButton (0) || (Input.touchCount > 0)) {
			if (posicionInicial == Vector3.zero)
					posicionInicial = Input.touchCount > 0 ? (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0)) : Input.mousePosition;
			transform.localPosition += ((Input.touchCount > 0 ? (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0)) : Input.mousePosition) - posicionInicial);
			posicionInicial = Input.touchCount > 0 ? (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0)) : Input.mousePosition; 
		} 
		else
			posicionInicial = Vector3.zero;
		Vector3 aux = transform.localPosition;
		if(aux.x > limites.x / cam.orthographicSize) aux = new Vector3(limites.x / cam.orthographicSize, aux.y, aux.z);
		if(aux.x < limites.width / cam.orthographicSize) aux = new Vector3(limites.width / cam.orthographicSize, aux.y, aux.z);
		if(aux.y > limites.y / cam.orthographicSize) aux = new Vector3(aux.x, limites.y / cam.orthographicSize, aux.z);
		if(aux.y < limites.height / cam.orthographicSize) aux = new Vector3(aux.x, limites.height / cam.orthographicSize, aux.z);
		transform.localPosition = aux;
		//if(Input.GetMouseButtonUp()
	}
}
