using UnityEngine;
using System.Collections;

public class Ruleta : MonoBehaviour {
	public Transform slotObj;
	public Transform maquinaIzq;
	public Transform maquinaDer;
	public Transform fondo;
	public Transform luz;
	Transform[] luces;
	Transform[] lucesAbajo;
	slot[] slotScript; 
	int nivelActual;
	//float tiempoSlot = 3f;
	int nEstaciones;
	public string[] monstruos;
	string[] monstruosDesbloqueados;
	int nMonstruosDesbloqueados = 0;
	int resultadoIndice = 0;
	
	public GameObject botonGo;
	public GameObject botonPlay;
	// Use this for initialization
	void Start () {
		//Playtomic.Log.CustomMetric("ruleta", "Nivel");
		
		botonPlay.SetActive(false);
		
		PlayerPrefs.SetInt("monstruo0desbloqueado", 1);
		//PlayerPrefs.SetInt("monstruo1desbloqueado", 1);
		nivelActual = PlayerPrefs.GetInt ("nivelActual", 1);
		nEstaciones = globalVariables.estacionesNivel[Mathf.Clamp(nivelActual - 1, 0, 14)];
		
		//nEstaciones = 2;
		
		slotScript = new slot[nEstaciones];
		for(int i = 0; i < monstruos.Length; i++){
			if(PlayerPrefs.GetInt("monstruo" + i + "desbloqueado", 0) == 1) 
				nMonstruosDesbloqueados++;
		}
		if(nMonstruosDesbloqueados <= 1 || nEstaciones <= 1){
			PlayerPrefs.SetInt("asiento0", 0);
			PlayerPrefs.SetInt("asiento1", 0);
			PlayerPrefs.SetInt("asiento2", 0);
			PlayerPrefs.SetInt("asiento3", 0);
			PlayerPrefs.SetInt("asiento4", 0);
			PlayerPrefs.SetInt("asiento5", 0);
			Play();
			return;
		}
		transform.parent.parent.gameObject.SetActive(true);
		monstruosDesbloqueados = new string[nMonstruosDesbloqueados];
		int indice = 0;
		for(int i = 0; i < monstruos.Length; i++){
			if(PlayerPrefs.GetInt("monstruo" + i + "desbloqueado", 0) == 1){
				monstruosDesbloqueados[indice] = monstruos[i];
				indice++;
			}
		}
		
		luces = new Transform[1 + nEstaciones * 2];
		luces[nEstaciones] = luz;
		lucesAbajo = new Transform[1 + nEstaciones * 2];
		lucesAbajo[nEstaciones] = (Transform)Instantiate(luz, new Vector3(0, -0.35f, luz.position.z), Quaternion.identity);;
		
		for(int i = 0; i < nEstaciones; i++){
			Transform t = (Transform)Instantiate(slotObj, new Vector3(90f * i - (90f * (nEstaciones - 1)) / 2, 0f, 0f), Quaternion.identity);
			t.parent = transform;
			t.position = t.position / 400;
			t.localScale = t.localScale / 400;
			slotScript[i] = t.gameObject.GetComponent<slot>();
			for(int j = 0; j < 6; j++){
				slotScript[i].setInformacion(monstruosDesbloqueados[j % nMonstruosDesbloqueados], j);
			}
			luces[nEstaciones + i + 1] = (Transform)Instantiate(luz, new Vector3(0.1f * (i + 1), luz.position.y, luz.position.z), Quaternion.identity);
			luces[nEstaciones - i - 1] = (Transform)Instantiate(luz, new Vector3(-0.1f * (i + 1), luz.position.y, luz.position.z), Quaternion.identity);
			lucesAbajo[nEstaciones + i + 1] = (Transform)Instantiate(luz, new Vector3(0.1f * (i + 1), -0.35f, luz.position.z), Quaternion.identity);
			lucesAbajo[nEstaciones - i - 1] = (Transform)Instantiate(luz, new Vector3(-0.1f * (i + 1), -0.35f, luz.position.z), Quaternion.identity);
			
			
		}
		for(int i = 0; i < luces.Length; i++){
			luces[i].gameObject.SendMessage("setTiempo", i * 0.3f);
			lucesAbajo[luces.Length - 1 - i].gameObject.SendMessage("setTiempo", i * 0.3f);
		}
		maquinaDer.localScale = new Vector3(130 + (nEstaciones - 2) * 45, maquinaDer.localScale.y, maquinaDer.localScale.z);
		maquinaIzq.localScale = new Vector3(130 + (nEstaciones - 2) * 45, maquinaIzq.localScale.y, maquinaIzq.localScale.z);
		fondo.localScale = new Vector3(190 + (nEstaciones - 2) * 100, fondo.localScale.y, fondo.localScale.z);
	}
	
	void iniciar(){
		botonGo.SetActive(false);
		for(int i = 0; i < nEstaciones; i++){
			slotScript[i].girar(0.5f * i, 3.0f + i);
		}
	}
		
	void resultadoSlot(int indice){
		PlayerPrefs.SetInt("asiento" + resultadoIndice, indice % nMonstruosDesbloqueados);
		resultadoIndice++;
		
		if(resultadoIndice == nEstaciones) botonPlay.SetActive(true);
	}
	
	void Skip(){
		//Playtomic.Log.CustomMetric("skipMRuleta", "Boton");
		
		PlayerPrefs.SetInt("asiento0", Random.Range(0, nMonstruosDesbloqueados - 1));
		PlayerPrefs.SetInt("asiento1", Random.Range(0, nMonstruosDesbloqueados - 1));
		PlayerPrefs.SetInt("asiento2", Random.Range(0, nMonstruosDesbloqueados - 1));
		PlayerPrefs.SetInt("asiento3", Random.Range(0, nMonstruosDesbloqueados - 1));
		PlayerPrefs.SetInt("asiento4", Random.Range(0, nMonstruosDesbloqueados - 1));
		PlayerPrefs.SetInt("asiento5", Random.Range(0, nMonstruosDesbloqueados - 1));
		
		Play ();
	}
	
	void Play(){
		Application.LoadLevel("Loading");	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
