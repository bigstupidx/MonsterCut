using UnityEngine;
using System.Collections;

public class slot : MonoBehaviour {
	public UISprite[] imagen;
	bool girando = false;
	float tiempoGirando;
	TweenRotation tween;
	float velocidad = 2;
	float anguloAux=0f;
	float anguloReal = 0f;
	// Use this for initialization
	void Start () {
		tween = gameObject.GetComponent<TweenRotation>();
	}
	
	public void setInformacion(string imagenNombre, int posicion){
		imagen[posicion].spriteName = imagenNombre;
	}
	
	public void girar(float delay, float tiempo){
		tiempoGirando = tiempo;
		StartCoroutine("conteoSlot", delay);
	}
	
	IEnumerator conteoSlot(float delay){
		yield return new WaitForSeconds(delay);
		velocidad += delay;
		girando = true;
		yield return new WaitForSeconds(tiempoGirando + 1 * Random.Range(0f, 1.0f));
		girando = false;
		establecerResultado();
	}
	
	void establecerResultado(){
		float anguloObjetivo = 0f;
		int indiceSeleccionado = 0;
		for(int i = 0; i < 6; i++){
			if(anguloReal >= i * 60 && anguloReal < (i + 1) * 60){
				indiceSeleccionado = i;
				anguloObjetivo = i * 60;
				break;	
			}
		}
		print ("indiceSeleccionado " + indiceSeleccionado);
		imagen[indiceSeleccionado].depth = 50;
		tween.from = new Vector3(anguloReal, 0f, 0f);
		tween.to = new Vector3(anguloObjetivo, 0f, 0f);
		tween.Play(true);
		transform.parent.gameObject.SendMessage("resultadoSlot", indiceSeleccionado);
	}
	
	// Update is called once per frame
	void Update () {
		if(girando){
			transform.Rotate(new Vector3(-velocidad * 300 * Time.deltaTime, 0f, 0f));	
			anguloAux += velocidad;
			anguloReal = 360 - anguloAux % 360;
		}
	}
}
