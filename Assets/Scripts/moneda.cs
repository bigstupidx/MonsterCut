using UnityEngine;
using System.Collections;

public class moneda : MonoBehaviour {
	public float rate = 0.1f;
	public Vector3 eje = new Vector3(0, 1, 0);
	//float alturaInicial;
	//public Transform monedaIndicador;
	Vector3 posicionFinal;
	public int monedasEntregadas = 1;
	//public int monedasBonus = 2;
	central centralScript;
	// Use this for initialization
	void Start () {
		
		GameObject c = GameObject.Find(".central");
		centralScript = c.GetComponent<central>();
		 //new Vector3(Camera.main.transform.position.x, 600f, transform.position.z);
		
		//alturaInicial = transform.position.y;
		//Transform t = (Transform)Instantiate(monedaIndicador, transform.position + new Vector3(30f, 0f, 0f), Quaternion.identity);
		//t.gameObject.SendMessage("setTexto", "+"+monedasEntregadas);
		
		addMonedas(monedasEntregadas);
		
	}
	
	// Update is called once per frame
	void Update () {
		posicionFinal = 500f * Camera.main.transform.up + new Vector3(Camera.main.transform.position.x, 0f, -30f) + 240f * Camera.main.transform.right;
		transform.Rotate(rate * eje * Time.deltaTime);
		//transform.position = new Vector3( transform.position.x, Mathf.Lerp(transform.position.y, alturaInicial + 150, 5 * Time.deltaTime), transform.position.z);
		transform.position = Vector3.Lerp(transform.position, posicionFinal, 5 * Time.deltaTime);
	}
	
	/*void OnTouch(){
		RaycastHit hit = new RaycastHit();
		if(this.collider.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 9999f))
		{
			Transform t = (Transform)Instantiate(monedaIndicador, transform.position + new Vector3(20f, 0f, 0f), Quaternion.identity);
			t.gameObject.SendMessage("setTexto", "+"+monedasBonus);
			addMonedas(monedasBonus);
			Destroy (gameObject);
		}
	}*/
	
	void addMonedas(int m){
		centralScript.addMonedas(m);	
	}
}