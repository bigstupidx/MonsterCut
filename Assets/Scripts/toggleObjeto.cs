using UnityEngine;
using System.Collections;

public class toggleObjeto : MonoBehaviour {
	public float tiempo = 1.0f;
	public int randPorcentaje = 70;
	public float tiempoApagado = 0.2f;
	public float tiempoActual = 0f;
	public float tiempoReal = 0f;
	public UISprite script;
	
	// Use this for initialization
	void Start () {
		definirTiempo(tiempo);
	}
	
	void definirTiempo(float t){
		tiempoActual = Time.timeSinceLevelLoad;
		tiempoReal = tiempoActual + t + Random.Range(- 1f, 1f) * (randPorcentaje / 100f) * t;
	}
	
	// Update is called once per frame
	void Update () {
		if(script.enabled){
			if(Time.timeSinceLevelLoad > tiempoReal){
				script.enabled = false;	
				definirTiempo(tiempoApagado);
			}
		}
		else{
			if(Time.timeSinceLevelLoad > tiempoReal){
				script.enabled = true;	
				definirTiempo(tiempo);
			}
		}
	}
}
