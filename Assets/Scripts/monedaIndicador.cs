using UnityEngine;
using System.Collections;

public class monedaIndicador : MonoBehaviour {
	//public tk2dTextMesh textMesh;
	float alturaInicial;
	// Use this for initialization
	void Start () {
		alturaInicial = transform.position.y;
	}
	
	void setTexto(string t){
		//textMesh.text = t;
		//textMesh.Commit();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( transform.position.x, Mathf.Lerp(transform.position.y, alturaInicial + 150, 5 * Time.deltaTime), transform.position.z);
		//textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.Lerp(textMesh.color.a, 0f, 1 * Time.deltaTime));
		//textMesh.Commit();
	}
}
