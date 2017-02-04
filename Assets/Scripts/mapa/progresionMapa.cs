using UnityEngine;
using System.Collections;

public class progresionMapa : MonoBehaviour {
	public itemPeluqueria[] peluquerias;
	public Transform[] objetoEscalar;
	public Transform[] objetoEscalarRestriccion;
	Camera camaraRedesSociales;
	Transform panelRedesSociales;
	public Camera camaraObj;
	float camaraSizeFinal = 1f;
	public itemPeluqueria[] primerMundo;
	public itemPeluqueria[] segundoMundo;
	public itemPeluqueria[] tercerMundo;

	public Transform[] afectadosAds;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.GetInt ("activateAds", 1) == 1) {
			for(int i = 0; i < afectadosAds.Length; i++){
				afectadosAds[i].localPosition = new Vector3(afectadosAds[i].localPosition.x, afectadosAds[i].localPosition.y + 50f, afectadosAds[i].localPosition.z);
			}
		}
		Time.timeScale = 1f;
		if (PlayerPrefs.GetInt ("estadoPeluqueria1", -1) == -1) PlayerPrefs.SetInt ("estadoPeluqueria1", 0);
		if(PlayerPrefs.GetInt ("tutorial3", 0) == 0 && PlayerPrefs.GetInt ("estadoPeluqueria1", 0) != 0){
			PlayerPrefs.SetInt ("estadoPeluqueria1", 0);
			for(int i = 1; i <= 100; i++){
				if(PlayerPrefs.HasKey("estadoPeluqueria" + i)){
					if(PlayerPrefs.GetInt("estadoPeluqueria" + i, 0) == 1){
						PlayerPrefs.SetInt("estadoPeluqueria" + i, 0);
					}
				}
			}
			PlayerPrefs.SetInt("enChallenge", 0);
			PlayerPrefs.SetInt("challengeId", -1);
			Application.LoadLevel (Application.loadedLevelName);
		}

		GameObject c = GameObject.FindWithTag("camaraRedesSociales");
		GameObject p = GameObject.FindWithTag("panelRedesSociales");
		if (c != null) { 
			camaraRedesSociales = c.GetComponent<Camera>();
			camaraRedesSociales.orthographicSize = 3f;
		}
		if(p != null) panelRedesSociales = p.transform;

		PlayerPrefs.SetInt("training", 0);

		//print (PlayerPrefs.GetInt ("tutorial3", 0)+"&&"+PlayerPrefs.GetInt ("estadoPeluqueria1", 0));

		//else if(primerMundo[0].estado < 0) primerMundo[0].setEstado(0);
		adaptar();
	}
	
	public void adaptar(){
		//habilita solo la primera peluqueria y si está pasada, habilita las del primer mundo
		if(PlayerPrefs.GetInt ("tutorial3", 0) == 1 && PlayerPrefs.GetInt ("estadoPeluqueria1", 0) == 2){
			foreach(itemPeluqueria i in primerMundo){
				if(PlayerPrefs.GetInt ("estadoPeluqueria"+i.id, 0) < 0) i.setEstado(0);
			}
		}

		int peluqueriasListas = 0;
		for(int i = 0; i < peluquerias.Length; i++){
//			print(peluquerias[i].estado);
			if(peluquerias[i].estado == 2) peluqueriasListas++;
		}
		print("peluquerias listas: "+peluqueriasListas);
		if (peluqueriasListas >= 3) {
				zoom (1.5f, false);
				int actual = 0;
				foreach (itemPeluqueria i in segundoMundo) {
					//if (PlayerPrefs.GetInt ("estadoPeluqueria" + actual, 0) < 0) //PlayerPrefs.SetInt ("estadoPeluqueria" + actual, 0);
					//	i.setEstado (0);
					if(i.cargado) i.testDesbloquear();
					else i.desbloquear = true;
					//actual++;
				}
				if (peluqueriasListas >= 9) {
						zoom (3f, false);
						//actual = 0;
						foreach (itemPeluqueria i in tercerMundo) {
								//if (PlayerPrefs.GetInt ("estadoPeluqueria" + actual, 0) < 0)
								//		i.setEstado (0);
								//actual++;
								if(i.cargado) i.testDesbloquear();
								else i.desbloquear = true;
						}
				}
		} else {
			foreach (itemPeluqueria i in primerMundo) {
				if(i.cargado) i.testDesbloquear();
				else i.desbloquear = true;
			}
			camaraSizeFinal = 1f;
		}
		/*
		int actual2 = 0;
		foreach(itemPeluqueria i in peluquerias){
			i.setEstado(PlayerPrefs.GetInt ("estadoPeluqueria" + actual2, 0));
		    actual2++;
	    }*/
	}
	
	void zoom(float val, bool zoomout){
		print ("zoom " + val);
		if(zoomout) adaptar();
		else 
			camaraSizeFinal = val;
	}
	
	void zoomOut(){
		zoom(3f, camaraSizeFinal == 3f);
	}

	void store(){
		Application.LoadLevel("Store");
	}
	/*
	void training(){
		PlayerPrefs.SetInt("training", 1);
		Application.LoadLevel("Loading");
	}
	*/
	void salir(){
		//if(PlayerPrefs.GetInt("peluquero1", 0) == 1)
			Application.LoadLevel("Seleccion");
		//else Application.LoadLevel("Titulo");	
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		camaraObj.orthographicSize = Mathf.Lerp(camaraObj.orthographicSize, camaraSizeFinal, Time.deltaTime * 1f);
		for(int i = 0; i < objetoEscalar.Length; i++){
			objetoEscalar[i].localScale = Vector3.one * camaraObj.orthographicSize;	
		}
		if (camaraRedesSociales != null) { 
			camaraRedesSociales.orthographicSize = Mathf.Lerp (camaraRedesSociales.orthographicSize, camaraSizeFinal, Time.deltaTime * 1f);
			if(panelRedesSociales == null){
				GameObject p = GameObject.FindWithTag("panelRedesSociales");
				if(p != null) panelRedesSociales = p.transform;
			}
		}
		if(panelRedesSociales != null) panelRedesSociales.localScale = Vector3.one * camaraObj.orthographicSize;	
		for(int i = 0; i < objetoEscalarRestriccion.Length; i++){
			objetoEscalarRestriccion[i].localScale = Vector3.one * Mathf.Clamp(camaraObj.orthographicSize * 0.7f, 1f, 3f);	
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("SeleccionMundo");
		}
	}

}
