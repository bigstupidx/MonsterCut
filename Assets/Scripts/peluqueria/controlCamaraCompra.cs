using UnityEngine;
using System.Collections;

public class controlCamaraCompra : MonoBehaviour {
	public Vector3 acc = Vector3.zero;
	public float aceleracion = 10f;
	float startPos = 0f;
	bool couldBeSwipe = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void moverDerecha(){
		
	}
	
	void moverIzquierda(){
		
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_WEBPLAYER
		//acc.y += aceleracion * Input.GetAxis("Vertical");
		acc.x += aceleracion * Input.GetAxis("Horizontal");
#else
		acc = Vector3.Lerp(acc, 20*aceleracion*Input.acceleration, Time.deltaTime);
#endif
		//acc.y = Mathf.Clamp(acc.y, -20f, -30f);
		acc.x = Mathf.Clamp(acc.x, -30f, 30f);
		
		transform.eulerAngles = new Vector3(0f, -acc.x, 0f);
		
#if UNITY_EDITOR || UNITY_WEBPLAYER
		if(Input.GetMouseButton(0)){
			if(!couldBeSwipe){
				startPos = transform.position.x + 2 * Input.mousePosition.x;
				couldBeSwipe=true;
			}
			else{
				transform.position = new Vector3(Mathf.Clamp(- 2 * Input.mousePosition.x + startPos, 515, 3850), transform.position.y, transform.position.z);	
			}
		}
		if(Input.GetMouseButtonUp(0)) couldBeSwipe=false;
		
#else
		if (Input.touchCount == 1) {
	        Touch touch = Input.touches[Input.touchCount-1];
	        switch (touch.phase) {
	            case TouchPhase.Began:
					couldBeSwipe=true;
	                startPos = transform.position.x + 2 * touch.position.x;
	                break;
	            case TouchPhase.Moved:
					if(couldBeSwipe)
	                	transform.position = new Vector3(Mathf.Clamp(- 2 * touch.position.x + startPos, 515, 3850), transform.position.y, transform.position.z);
	                break;
				case TouchPhase.Ended:
					couldBeSwipe=false;
					break;
	        }
	    }
#endif
	}
	
	void OnGUI(){
		//GUI.Box(new Rect(100, 100, 100, 100), "" + acc);
	}
}
