using UnityEngine;
using System.Collections;
using SmoothMoves;

public class tijerasV2 : MonoBehaviour {
	/*Vector2 touchPosition;
	Vector2 touchPositionStart;
	Rect tijerasInicio;
	Rect tijerasFin;*/
	public Transform tijerasDer;
	public Transform tijerasIzq;
	SmoothMoves.Sprite tijerasDerScript;
	SmoothMoves.Sprite tijerasIzqScript;
	//public bool cortarHabilitado=true;
	public bool enviarEvento = false;
	public float timeClosed = 0.2f;
	float timeClosedActual = 0.0f;
	bool cortando = false;
	public bool activado = false;
	public float cooldownActualIzq = 0f;
	public float cooldownActualDer = 0f;
	public float cooldown = 1.0f;
	
	private bool corteAutomatico = false;
	private bool corteFuerza = false;
	
	//-1: nada
	// 0: abierto
	// 1: cerrado
//	int estado=-1;
	// Use this for initialization
	void Start () {
		tijerasDerScript = tijerasDer.gameObject.GetComponent<SmoothMoves.Sprite>();
		tijerasIzqScript = tijerasIzq.gameObject.GetComponent<SmoothMoves.Sprite>();
		//tijerasInicio=new Rect(3*Screen.width/4, Screen.width/8, Screen.width/4, Screen.width/4);
		//tijerasFin=new Rect(Screen.width/2, 0, Screen.width/4, Screen.width/4);
	}
	
	void activar(bool a){
		activado = a;	
	}
	
	void setCorteCooldown(float f){
		cooldown = f;
	}
	
	void activarAutomatico(bool activar){
		corteAutomatico = activar;
		cooldown *= activar?0.5f:2f;
		tijerasDerScript.SetColor(new Color(activar?0.5f:1f, activar?0.5f:1f, activar?1f:1f));
		tijerasIzqScript.SetColor(new Color(activar?0.5f:1f, activar?0.5f:1f, activar?1f:1f));
	}
	
	void activarFuerza(bool activar){
		corteFuerza = activar;
		tijerasDerScript.SetColor(new Color(activar?1f:1f, activar?1f:1f, activar?0f:1f));
		tijerasIzqScript.SetColor(new Color(activar?1f:1f, activar?1f:1f, activar?0f:1f));
	}
	
	void cerrarTijerasDer(){
		if(!activado) return;
		
		#if UNITY_EDITOR || UNITY_WEBPLAYER
		//segmento de prueba dentro del editor
		if(cooldownActualDer < Time.time){
			cooldownActualDer = Time.time + cooldown;
			if(PlayerPrefs.GetInt("sonido", 1) == 1)
				GetComponent<AudioSource>().Play();
			tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
			tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
			gameObject.SendMessage("cortarPeloTilt");
		}
		#else
		if(cooldownActualDer < Time.time){
			if(cortando){
					cooldownActualDer = Time.time + cooldown;
					if(PlayerPrefs.GetInt("sonido", 1) == 1)
						GetComponent<AudioSource>().Play();
					tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
					tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
					cortando = false;
					timeClosedActual = 0f;
					if(enviarEvento) gameObject.SendMessage("cortarPeloTilt");
				
			}
			else{ 
				cortando = true;
				tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
			}
		}
		#endif
		
	}
	void cerrarTijerasIzq(){
		if(!activado) return;
		
		#if UNITY_EDITOR || UNITY_WEBPLAYER
		//segmento de prueba dentro del editor
		if(cooldownActualIzq < Time.time){
			cooldownActualIzq = Time.time + cooldown;
			if(PlayerPrefs.GetInt("sonido", 1) == 1) 
				GetComponent<AudioSource>().Play();
			tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
			tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
			gameObject.SendMessage("cortarPeloTilt");
		}
		#else
		if(cooldownActualIzq < Time.time){
			if(cortando){
					cooldownActualIzq = Time.time + cooldown;
					if(PlayerPrefs.GetInt("sonido", 1) == 1) 
						GetComponent<AudioSource>().Play();
					tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
					tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
					cortando = false;
					timeClosedActual = 0f;
					if(enviarEvento) gameObject.SendMessage("cortarPeloTilt");
				
			}
			else{ 
				tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
				cortando = true;
			}
		}
		#endif
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && activado) {
			if(cooldownActualDer < Time.time && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetTouch(0).phase == TouchPhase.Ended)){
				cooldownActualDer = Time.time + cooldown;
				if(PlayerPrefs.GetInt("sonido", 1) == 1)
					GetComponent<AudioSource>().Play();
				tijerasDer.localEulerAngles = new Vector3(0, 0, 0);
				tijerasIzq.localEulerAngles = new Vector3(0, 180, 0);
				gameObject.SendMessage("cortarPeloTilt");
			}
		}

		transform.position = Vector3.Lerp (transform.position, new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z), 100 * Time.deltaTime);
		
		Vector3 angActualDer = tijerasDer.localEulerAngles;
		angActualDer = Vector3.Lerp(angActualDer, new Vector3(0f, 0f, 40f), 5 * Time.deltaTime);
		tijerasDer.localEulerAngles = angActualDer;
		Vector3 angActualIzq = tijerasIzq.localEulerAngles;
		angActualIzq = Vector3.Lerp(angActualIzq, new Vector3(0f, 180f, 40f), 5 * Time.deltaTime);
		tijerasIzq.localEulerAngles = angActualIzq;
		
		if(cortando){
			timeClosedActual += Time.deltaTime;
			if(timeClosedActual > timeClosed){ 
				cortando = false;
				timeClosedActual = 0f;
			}
		}
		
		if(corteAutomatico){
			cerrarTijerasIzq();
			cerrarTijerasDer();
		}
	}
	
	void OnGUI(){
		
		//GUI.Box(new Rect(tijerasInicio.x, Screen.height-tijerasInicio.y, tijerasInicio.width, -tijerasInicio.height), ""+touchPositionStart);
		//GUI.Box(new Rect(tijerasFin.x, Screen.height-tijerasFin.y, tijerasFin.width, -tijerasFin.height), estado+" "+touchPosition);
		//GUI.Label(new Rect(tijerasFin.x, Screen.height-tijerasFin.y-100, tijerasFin.width, tijerasFin.height), estado+" "+touchPosition);
	}
}
