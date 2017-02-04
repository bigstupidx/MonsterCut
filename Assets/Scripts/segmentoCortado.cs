using UnityEngine;
using System.Collections;
using SmoothMoves;

public class segmentoCortado : MonoBehaviour {
	float tiempoVida = 5f;
	// Use this for initialization
	void Start () {
		tiempoVida += Time.time;
		GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4000000, 4000000), Random.Range(2000000, 4000000), 0));
		GetComponent<Rigidbody>().AddTorque(new Vector3(0,0,Random.Range(500, 4000)));
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -200 || Time.time >= tiempoVida) Destroy(gameObject);
	}
}
