using UnityEngine;
using System.Collections;

public class peluquero : MonoBehaviour {
	public int vidas = 0;
	public string nombre = "Ronnie";
	public int id = 0;
	public expresion expresionScript;
	public string retrato = "retrato";
	//public poder[] poderes;
	
	public Transform[] poderes;
	Transform[] poderesObj;
	Transform panelIzqAbajo;
	Transform panelDerAbajo;
	
	GameObject centralObj;
	
	public float experiencia = 0;
	public int nivel = 1; //[1, 10]
	public int poderCorte = 0; //[0, 100]
	public float corteCooldown = 0.5f;
	
	//0 - 2
	public int corteVelocidad = 0;
	public int corteFuerza = 0;
	
	public string[] spritePoderes;
	
	
	//bool activo = false;
	
	void Start(){
		centralObj = GameObject.Find(".central");
        vidas = PlayerPrefs.GetInt("vidas", 0);
		
		componer();
	}
	
	public void iniciar(){
		nivel = PlayerPrefs.GetInt("peluquero" + id + "nivel", 1);
		experiencia = PlayerPrefs.GetInt("peluquero" + id + "exp", 0);
		
		print ("peluquero nivel: " + nivel);
		
		poderCorte = Mathf.Clamp(nivel * 3 + (corteFuerza * 10), 0, 100);
		corteCooldown = Mathf.Clamp(0.5f * (1 / (corteVelocidad + 1)) - 0.04f * nivel, 0f, 1f);
		
		int mejoraVelocidad = PlayerPrefs.GetInt("mejoraVelocidad", 0);
		corteCooldown *= 1f - 0.04f * mejoraVelocidad;
		
		int mejoraFuerza = PlayerPrefs.GetInt("mejoraFuerza", 0);
		poderCorte = Mathf.Clamp(poderCorte + 5 * mejoraFuerza, 0, 100);
		
		int objetoFuerza = PlayerPrefs.GetInt("objetoTijeraFuerza", 0);
		if(objetoFuerza > 0){
			PlayerPrefs.SetInt("objetoTijeraFuerza", PlayerPrefs.GetInt("objetoTijeraFuerza", 0) - 1);
			poderCorte = Mathf.Clamp(poderCorte + (int)(poderCorte * 0.5f), 0, 100);
		}
		
		int objetoVelocidad = PlayerPrefs.GetInt("objetoTijeraVelocidad", 0);
		if(objetoVelocidad > 0){
			PlayerPrefs.SetInt("objetoTijeraVelocidad", PlayerPrefs.GetInt("objetoTijeraVelocidad", 0) - 1);
			corteCooldown = Mathf.Clamp(corteVelocidad - corteVelocidad * 0.5f, 0f, 1f);
		}
		
		int objetoDorada = PlayerPrefs.GetInt("objetoTijeraDorada", 0);
		if(objetoDorada > 0){
			PlayerPrefs.SetInt("objetoTijeraDorada", PlayerPrefs.GetInt("objetoTijeraDorada", 0) - 1);
			corteCooldown = Mathf.Clamp(corteVelocidad - corteVelocidad * 0.5f, 0f, 1f);
			poderCorte = Mathf.Clamp(poderCorte + (int)(poderCorte * 0.5f), 0, 100);
		}
		
	}
	
	public int getPoderCorte(){ return poderCorte; }
	public float getCorteCooldown(){ return corteCooldown; }
	
	public void setAnchor(Transform panelIzqAbajo, Transform panelDerAbajo){
		this.panelIzqAbajo = panelIzqAbajo;
		this.panelDerAbajo = panelDerAbajo;
		
		poderesObj = new Transform[poderes.Length];
		poderesObj[0] = (Transform) Instantiate (poderes[0]);
		poderesObj[0].parent = panelIzqAbajo;
		poderesObj[0].localPosition = poderesObj[0].localPosition / 400;
		poderesObj[0].localScale = poderesObj[0].localScale / 400;
		poderesObj[1] = (Transform) Instantiate (poderes[1]);
		poderesObj[1].parent = panelDerAbajo;
		poderesObj[1].localPosition = poderesObj[1].localPosition / 400;
		poderesObj[1].localScale = poderesObj[1].localScale / 400;

		StartCoroutine("setNivelPoderes");
	}

	IEnumerator setNivelPoderes(){
		yield return new WaitForSeconds(0.2f);
		poderesObj[0].gameObject.SendMessage("setNivel", PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder1", 0));
		poderesObj[1].gameObject.SendMessage("setNivel", PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder2", 0));
	}
	
	public void reducirCooldownPorcentaje(float p){
		poderesObj[0].gameObject.SendMessage("reducirCooldownPorcentaje", p);	
		poderesObj[1].gameObject.SendMessage("reducirCooldownPorcentaje", p);	
	}
	
	public void componer(){
	}
	
	public void terminar(){
		PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel", nivel);
		PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"exp", (int)experiencia);
	}
	
	public void activar(bool b){
		//activo = b;
		for(int i = 0; i < poderesObj.Length; i++){
			poderesObj[i].gameObject.SendMessage("activar", b);	
		}
	}
	
	public void peloCortado(float exp){
		//for(int i = 0; i < poderesObj.Length; i++){
			//poderesObj[i].gameObject.SendMessage("addExp", exp);
			addExperiencia(exp);
			centralObj.SendMessage("addExp", exp);
		//}
	}
	
	void addExperiencia(float exp){
		experiencia += exp;
		nivel = 1;
		for(int i = 0; i < globalVariables.niveles.Length; i++){
			if(experiencia >= globalVariables.niveles[i]) nivel = i + 1;
		}
	}
	
	public Vector2 getExpPoderes(){
		Vector2 pod;
		poder p = poderesObj[0].gameObject.GetComponent<poder>();
		pod.x = p.exp;
		p = poderesObj[1].gameObject.GetComponent<poder>();
		pod.y = p.exp;
		
		return pod;
	}
	
	void cambiarPelo(){}
	
	public void setExpresion(int e, bool forceLoop = false){
		switch(e){
			case 1: expresionScript.setExpresionActual(expresion.expresiones.idle); break;
			case 2: expresionScript.setExpresionActual(expresion.expresiones.sorpresa, forceLoop); break;
			case 3: expresionScript.setExpresionActual(expresion.expresiones.enojado1); break;
			case 4: expresionScript.setExpresionActual(expresion.expresiones.enojado2); break;
			case 5: expresionScript.setExpresionActual(expresion.expresiones.enojado3); break;
			case 6: expresionScript.setExpresionActual(expresion.expresiones.enojadoFinal); break;
		}
	}
	
	void poderCongelar(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderCongelar", nivel);
		
	}
	void poderCortarCompleto(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderCortarPeloCompleto", nivel);
	}
	void poderCortarPuntas(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderCortarPuntas", nivel);
	}
	void poderAutoCorte(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderAutoCorte", nivel);
	}
	void poderDetenerTiempo(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderDetenerTiempo", nivel);
		
	}
	void poderFuerza(int nivel){
		setExpresion(2);
		centralObj.SendMessage("poderFuerza", nivel);
		
	}
	
}
