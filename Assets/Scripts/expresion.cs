using UnityEngine;
using System.Collections;
using SmoothMoves;

public class expresion : MonoBehaviour {
	
	public enum expresiones{ idle, sorpresa, enojado1, enojado2, enojado3, enojadoFinal}
	
	public BoneAnimation animacion;
	public expresiones expresionActual = expresiones.idle;
	public float duracionSorpresa = 1f;
	
	void Start(){
		expresar (expresionActual);
	}
	
	private void expresar(expresiones e, bool forceLoop = false){
		if(animacion == null) return;
		switch(e){
		case expresiones.idle:
			animacion.Play("idle");
			expresionActual = e;
			break;
		case expresiones.sorpresa:
			animacion.Play("sorpresa");
			if(!forceLoop) StartCoroutine("conteoExpresion", duracionSorpresa);
			break;
		case expresiones.enojado1:
			GetComponent<Animation>().Play("enojado1");
			expresionActual = e;
			break;
		case expresiones.enojado2:
			animacion.Play("enojado2");
			expresionActual = e;
			break;
		case expresiones.enojado3:
			animacion.Play("enojado3");
			expresionActual = e;
			break;
		case expresiones.enojadoFinal:
			animacion.Play("enojadoFinal");
			expresionActual = e;
			//StartCoroutine("conteoExpresion", 2f);
			break;
		}
	}
	
	public void setExpresionActual(expresiones exp, bool forceLoop = false){
		if(expresionActual != exp){ 
			expresar (exp, forceLoop);
		}
	}

	/*void Update(){
		if (Input.GetButtonUp ("Jump")) {
			print ("press");
			//if(gameObject.layer == 8) setExpresionActual(expresiones.enojado3, true);
			if(gameObject.layer == 10) setExpresionActual(expresiones.enojadoFinal, true);
		}
		if (Input.GetButtonUp ("Fire2")) {
			print ("press");
			//if(gameObject.layer == 8) setExpresionActual(expresiones.enojado3, true);
			if(gameObject.layer == 10) setExpresionActual(expresiones.sorpresa, true);
		}
		if (Input.GetButtonUp ("Fire3")) {
			print ("press");
			//if(gameObject.layer == 8) setExpresionActual(expresiones.enojado3, true);
			if(gameObject.layer == 10) setExpresionActual(expresiones.idle, true);
		}
		if (Input.GetButtonUp ("Fire1")) {
			print ("press");
			if(gameObject.layer == 8) setExpresionActual(expresiones.enojado3, true);
			//if(gameObject.layer == 10) setExpresionActual(expresiones.enojadoFinal, true);
		}
	}*/
	
	IEnumerator conteoExpresion(float tiempoExpresion){
		yield return new WaitForSeconds(tiempoExpresion);
		expresar (expresionActual);
	}
	
	/*public BoneAnimation boca;
	public BoneAnimation ojos;
	public Sprite cara;
	public Sprite cuerpo;
	public Color[] coloresExpresion;
	public AudioClip sonidosExpresionSorpresa;
	public AudioClip sonidosExpresionEnojado;
	public AudioClip sonidosExpresionFeliz;
	public int tipo;
	public float tiempoExpresion = 1.0f;
	public int expresionActual = 0;
	
	// Use this for initialization
	void Start () {
		//print(boca.tex);
		expresar (0);
	}
	
	private void expresar(int t){
		switch(t){
		case -1:
			ojos.Play("enojado");
			boca.Play("enojado");
			if(cara != null) cara.SetColor(coloresExpresion[2]);
			if(cuerpo != null) cuerpo.SetColor(coloresExpresion[2]);
			if(sonidosExpresionEnojado != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionEnojado);
			break;
		case 0:
			ojos.Play("normal");
			boca.Play("normal");
			if(cara != null) cara.SetColor(coloresExpresion[1]);
			if(cuerpo != null) cuerpo.SetColor(coloresExpresion[1]);
			break;
		case 1:
			ojos.Play("feliz");
			boca.Play("feliz");
			if(cara != null) cara.SetColor(coloresExpresion[0]);
			if(cuerpo != null) cuerpo.SetColor(coloresExpresion[0]);
			if(sonidosExpresionFeliz != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionFeliz);
			break;
		case 2:
			ojos.Play("sorpresa");
			boca.Play("sorpresa");
			StartCoroutine("conteoExpresion");
			//if(sonidosExpresionSorpresa != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionSorpresa);
			break;
		case 10:
			ojos.Play("normal");
			boca.Play("preocupado1");
			//if(sonidosExpresionSorpresa != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionSorpresa);
			break;
		case 11:
			ojos.Play("preocupado1");
			boca.Play("preocupado2");
			//if(sonidosExpresionSorpresa != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionSorpresa);
			break;
		case 12:
			ojos.Play("sorpresa");
			boca.Play("preocupado3");
			//if(sonidosExpresionSorpresa != null && PlayerPrefs.GetInt("sonido", 1) == 1) audio.PlayOneShot(sonidosExpresionSorpresa);
			break;
		}
	}
	
	public void setExpresionActual(int expres){
		if(expresionActual != expres){
			expresionActual = expres;
			expresar (expresionActual);
		}
	}
	
	IEnumerator conteoExpresion(){
		yield return new WaitForSeconds(tiempoExpresion);
		expresar (expresionActual);
	}*/
}
