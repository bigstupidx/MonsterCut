using UnityEngine;
using System.Collections;

public class central : MonoBehaviour {
	public int nivelActual;
	public int puntajeNivel;
	int puntajeTotal;
	public int puntajePorCorte = 10;
	public UILabel puntajeLabel;
	public UILabel puntajeLabelAux;
	public UILabel nivelLabel;
	public UILabel vidasLabel;
	public UILabel monedasLabel;
	int multiplicadorMonedas = 0;
	public float tiempoNivel;
	float tiempoActual;
	System.DateTime tiempochallenge;
	public UILabel tiempoLabel;
	public UILabel challengeLabel;
	public UISlider barraChallenge;
	int estaciones;
	int estacionActual; //[0, n]
	public Transform[] monstruos;
	public monstruo[] monstruosNivel;
	
	public Transform[] peluqueros;
	peluquero peluqueroNivel;
	public Transform miniCamara;
	Camera miniCamaraScript;
	public GameObject camaraObj;
	public Transform tijeras;
	public Transform reloj;
	public GameObject botonReintentar;
	tilt posActual;
	
	public TweenPosition[] elementosGUI;
	public TweenPosition[] pausaGUI;
	public TweenPosition pausaPreguntaGUI;
	
	public TweenPosition challengePanel;
	public TweenPosition noChallengePanel;
	public TweenPosition challengeFailPanel;
	public TweenPosition challengeWinPanel;
	//public resumen resumenScript;
	public gameFinished gameFinishedScript;
	
	public GameObject mensajeLabel;
	
	bool mostrandoPerdiste = false;
	
	public Transform panelIzqAbajo;
	public Transform panelDerAbajo;
	
	//0: conteo
	//1: jugando
	//2: pausa
	//3: pierde vida
	//4: terminado
	public int estado = 0;
	
	public bool pausado = false;
	float tiempoPausa = 0f;
	public GameObject consejoTexto;
	
	float tiempoPuntaje = 0f;
	public int bonusPerfect = 200;
	public int bonusPowers = 20;
	public int bonusLives = 50;
	public int bonusHappyness = 30;
	//int poderesUsados = 0;
	float expCollected = 0;
	
	GameObject barraEstrella;
	int idLenguaje = 0;
	
	public AudioSource musica;
	bool autorizadoPasar = false;
	float tiempoCheckAnimo = 3.0f;
	public int nMonstruosDesbloqueados = 0;
	public int peluqueroPoderCorte = 0;
	int peluqueroPoderCorteAux = 0;
	int clientesAtendidos = 0;
	
	//misiones
	int hairCuts = 0;
	int topHairCuts = 0;
	int poder1usos = 0;
	int poder2usos = 0;
	int vidasUsadas = 0;
	
	public GameObject swipeMessage;
	bool swipeMostrado = false;
	public GameObject vidasPanel;
	
	bool modoTraining = false;

	bool salirAMapa = false;

	int[] monstruosDesbloqueadosID;

	public Transform[] warningSign;

	public AudioClip musicaFinal;

	public GameObject tutorialFinal;
	public AudioClip sonidoVictoria;
	public AudioClip sonidoReady;
	public AudioClip sonidoGo;

	public GameObject botonFacebookPublicar;
	public GameObject[] botonesReintentar;

	public AudioClip musicaInicio;
	//Localization loc;
	flurryNemoris fl;

	public GameObject tap;
	public GameObject tiltAnim;
	int nCortes = 0;
	public GameObject thankYouPrefab;

	public GameObject botonShare;

	void Start(){
		Time.timeScale = 1;

		tiltAnim.SetActive (false);
		tap.SetActive (false);
		GameObject go = GameObject.Find ("FlurryObjeto");
		if (go != null)
			fl = go.GetComponent<flurryNemoris> ();
		//PlayerPrefs.SetInt ("monedas", 10000); 
		//loc = Localization.instance;

		modoTraining = PlayerPrefs.GetInt("training", 0) == 1;
		if(!modoTraining){
			noChallengePanel.Play(true);
			challengePanel.Play(false);
			PlayerPrefs.SetInt("challengeLives", PlayerPrefs.GetInt("challengeLives", 0) - 1);
			barraChallenge.sliderValue = (float)PlayerPrefs.GetInt("challengeScore") / (float)PlayerPrefs.GetInt("challengeStartScore");
			challengeLabel.text = "" + PlayerPrefs.GetInt("challengeStartScore");
			tiempochallenge = System.DateTime.Parse(PlayerPrefs.GetString("challengeStopTime"));
		}
		else{
			noChallengePanel.Play(false);
			challengePanel.Play(true);
		}
		
		
		
		PlayerPrefs.SetInt("nivelActual", 1);
		globalVariables.establecerMisiones();
		
		tiempoNivel *= 1f + 0.12f * PlayerPrefs.GetInt("mejoraApariciones", 0);
		
		if(PlayerPrefs.GetInt("objeto2oro", 0) > 0){
			multiplicadorMonedas += 2;
			PlayerPrefs.SetInt("objeto2oro", PlayerPrefs.GetInt("objeto2oro", 0) - 1);
		}
		if(PlayerPrefs.GetInt("objeto3oro", 0) > 0){
			multiplicadorMonedas += 3;
			PlayerPrefs.SetInt("objeto3oro", PlayerPrefs.GetInt("objeto3oro", 0) - 1);
		}
		PlayerPrefs.SetInt("monstruo0desbloqueado", 1);
		//Quitar esta linea
		//PlayerPrefs.SetInt("monstruo1desbloqueado", 1);
		for(int i = 0; i < monstruos.Length; i++){
			if(PlayerPrefs.GetInt("monstruo" + i + "desbloqueado", 0) == 1) 
				nMonstruosDesbloqueados++;
		}
		monstruosDesbloqueadosID = new int[nMonstruosDesbloqueados];
		int idActual = 0;
		for(int i = 0; i < monstruos.Length; i++){
			if(PlayerPrefs.GetInt("monstruo" + i + "desbloqueado", 0) == 1){
				monstruosDesbloqueadosID[idActual] = i;
				idActual++;
			}
		}
		if (PlayerPrefs.GetInt ("forzarMonstruo", -1) >= 0) {
			monstruosDesbloqueadosID = null;
			nMonstruosDesbloqueados = 1;
			monstruosDesbloqueadosID = new int[1];
			monstruosDesbloqueadosID[0] = PlayerPrefs.GetInt ("forzarMonstruo", -1);
		}
		print (PlayerPrefs.GetInt ("monstruo0desbloqueado", 0) +" "+ PlayerPrefs.GetInt ("monstruo1desbloqueado", 0));
		if(PlayerPrefs.GetString("Language").Contains("Esp")) idLenguaje = 1;
		nivelActual = PlayerPrefs.GetInt ("nivelActual", 1);
		nivelLabel.text = Localization.Get("Level") + " " + nivelActual;
		if(nivelActual == 1){
			//PlayerPrefs.SetInt("vidas", 0);
			PlayerPrefs.SetInt("puntajeTotal", 0);
		}
		else puntajeTotal = PlayerPrefs.GetInt("puntajeTotal", 0);

		toggleGUI(false, true);
		pausaPreguntaGUI.gameObject.SetActive(false);
		pausaPreguntaGUI.Play(false);
		
		barraEstrella = GameObject.FindWithTag("barraEstrella");
		addPuntaje(0);
		
		definirEstaciones();
		spawnMonstruos();
		
		//Spawning minicamara
		//if(estaciones > 1){
			Transform m = (Transform) Instantiate (miniCamara, new Vector3(500 * (estaciones - 1), miniCamara.position.y, miniCamara.position.z), Quaternion.identity);
			miniCamaraScript = m.gameObject.GetComponent<Camera>();
			miniCamaraScript.gameObject.SetActive(false);
		//}
		
		//Spawning peluquero
		Transform p = (Transform) Instantiate(peluqueros[PlayerPrefs.GetInt("peluqueroSeleccionado", 0)]);
		peluqueroNivel = p.gameObject.GetComponent<peluquero>();
		p.parent = camaraObj.transform;
		peluqueroNivel.setAnchor(panelIzqAbajo, panelDerAbajo);
		
		int objetoCooldown = PlayerPrefs.GetInt("objetoCooldown", 0);
		if(objetoCooldown > 0){
			PlayerPrefs.SetInt("objetoCooldown", PlayerPrefs.GetInt("objetoCooldown", 0) - 1);
			peluqueroNivel.reducirCooldownPorcentaje(0.25f);
		}
		
		peluqueroNivel.iniciar();
		
		camaraObj.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
		tijeras.gameObject.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
		tijeras.gameObject.SendMessage("setCorteCooldown", peluqueroNivel.getCorteCooldown());
		peluqueroPoderCorte = peluqueroNivel.getPoderCorte();
		
		gameFinishedScript.setPeluquero(peluqueroNivel.nombre, peluqueroNivel.retrato, (int)peluqueroNivel.experiencia, peluqueroNivel.nivel, peluqueroNivel.spritePoderes[0], peluqueroNivel.spritePoderes[1]);
		
		posActual = camaraObj.gameObject.GetComponent<tilt>();
		
		vidasLabel.text = "x  " + PlayerPrefs.GetInt("vidas", 0);
		monedasLabel.text = "" + PlayerPrefs.GetInt("monedas", 0);

		if(PlayerPrefs.GetInt("vidas", 0) <= 0) vidasPanel.SetActive(false);
		
		mostrarMensaje(Localization.Get("Ready?"));
		musica.PlayOneShot(musicaInicio, 1f);
		musica.PlayOneShot(sonidoReady, 0.5f);

		Time.timeScale = 1f;
		
		//Playtomic.Log.CustomMetric("nivel"+nivelActual, "Nivel");
		if(PlayerPrefs.GetInt("challengeLives", 0) <= 0){
			foreach(GameObject g in botonesReintentar){
				g.SetActive(false);
			}
		}
	}

	void shareExitoso(){
		monedasLabel.text = "" + PlayerPrefs.GetInt("monedas", 0);
		botonShare.SetActive (false);
	}
	
	public void addMonedas(int m){
		if(multiplicadorMonedas > 0){
			m *= multiplicadorMonedas;
		}
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + m);	
		monedasLabel.text = "" + PlayerPrefs.GetInt("monedas", 0);
	}

	void definirEstaciones(){
		estaciones = globalVariables.estacionesNivel[Mathf.Clamp(nivelActual - 1, 0, globalVariables.estacionesNivel.Length - 1)];
		//estacionActual = Mathf.FloorToInt(estaciones / 2);
	}
	
	//Spawning monstruos
	void spawnMonstruos(){
		//deja a ind en el indice correspondiente de globalVariables
		int ind = 0;
		for(int i = 0; i < Mathf.Clamp(nivelActual - 1, 0, globalVariables.estacionesNivel.Length); i++){
			ind += globalVariables.estacionesNivel[i];
		}
		
		monstruo[] monstruoAux = new monstruo[estaciones];
		if(monstruosNivel!=null && monstruosNivel.Length > 0){
			for(int i = 0; i < monstruosNivel.Length; i++) monstruoAux[i] = monstruosNivel[i];
			monstruosNivel = monstruoAux;
			for(int i = ind; i < ind + estaciones; i++){
				if(monstruosNivel[i - ind] == null){
					int idSel = monstruosDesbloqueadosID[Random.Range(0, nMonstruosDesbloqueados)];
					print ("seleccionado monstruo " + idSel);
					PlayerPrefs.SetInt("asiento"+(i - ind), idSel);
					StartCoroutine (crearMonstruoNuevo(i, ind));
				}
				else{
					print(monstruosNivel[i - ind].dificultad +"!="+ globalVariables.monstruosDificultadNivel[i]);
					if(monstruosNivel[i - ind].dificultad != globalVariables.monstruosDificultadNivel[i]){
						StartCoroutine(reemplazarMonstruo(i - ind, i));
					}
				}
			}
			miniCamaraScript.transform.position = new Vector3(500 * (estaciones - 1), miniCamara.position.y, miniCamara.position.z);
			miniCamaraScript.gameObject.SetActive(true);
			if(!swipeMostrado){ 
				swipeMessage.SetActive(true);
				swipeMostrado = true;
			}
			miniCamaraScript.rect = new Rect(0.4f - (estaciones - 1) * 0.1f, 0.79f, 0.2f * estaciones, 0.1f);
		}
		else{
			monstruosNivel = new monstruo[estaciones];
			for(int i = ind; i < ind + estaciones; i++){
				//REEMPLAZAR
				//PlayerPrefs.SetInt("asiento"+(i - ind), 1);
				int idSel = monstruosDesbloqueadosID[Random.Range(0, nMonstruosDesbloqueados)];
				print ("seleccionado monstruo " + idSel);
				PlayerPrefs.SetInt("asiento"+(i - ind), idSel);
				StartCoroutine (crearMonstruoNuevo(i, ind));
			}
		}
		
		/*for(int i = 0; i < monstruosNivel.Length; i++)
			if(!monstruosNivel[i].iniciado){ 
				print ("iniciado "+i);
				monstruosNivel[i].iniciar();
			}*/
	}

	IEnumerator crearMonstruoNuevo(int i, int ind){
		if(i > 0) yield return new WaitForSeconds((Random.Range(0f, 2f)));
		Transform t = (Transform) Instantiate(monstruos[PlayerPrefs.GetInt("asiento" + (i - ind) , 0) * 5 + globalVariables.monstruosDificultadNivel[i]], new Vector3(1000 * (i - ind), -1000, 150), Quaternion.identity);
		GameObject g = t.gameObject;
		monstruosNivel[i - ind] = g.GetComponent<monstruo>();
		monstruosNivel[i - ind].componer();
		yield return false;
	}
	
	IEnumerator reemplazarMonstruo(int indice, int indiceGlobal){
		monstruosNivel[indice].pausar(true);
		monstruosNivel[indice].esconder(true);
		GameObject g1 = (GameObject)Instantiate (thankYouPrefab, new Vector3 (1000 * (indice), 40, 1), Quaternion.identity);
		g1.GetComponent<TweenPosition> ().from = new Vector3 (1000 * (indice), 40, 1);
		g1.GetComponent<TweenPosition> ().to = new Vector3 (1000 * (indice), 130, 1);
		yield return new WaitForSeconds(1);
		float tiempoRestanteEfecto = monstruosNivel[indice].obtenerTiempoRestanteEfecto();
		string ultimoPoderEjecutado = monstruosNivel[indice].ultimoPoderEjecutado;
		//print("tiempo restante: "+tiempoRestanteEfecto);
		Destroy(monstruosNivel[indice].gameObject);
		//se genera aleatoriamente para evitar sobrecarga
		yield return new WaitForSeconds(Random.Range(0f, 2f));
		//REEMPLAZAR
		//PlayerPrefs.SetInt("asiento"+indice, 1);
		PlayerPrefs.SetInt("asiento"+indice, monstruosDesbloqueadosID[Random.Range(0, nMonstruosDesbloqueados)]);
		Transform t = (Transform) Instantiate(monstruos[PlayerPrefs.GetInt("asiento" + (indice) , 0) * 5 + globalVariables.monstruosDificultadNivel[indiceGlobal]], new Vector3(1000 * (indice), -1000, 150), Quaternion.identity);
		GameObject g = t.gameObject;
		monstruosNivel[indice] = g.GetComponent<monstruo>();
		monstruosNivel[indice].componer();
		monstruosNivel[indice].esconder(false);
		print ("monstruo reemplazado en "+(indice));
		clientesAtendidos++;
		//monstruosNivel[indice].iniciar();
		if(tiempoRestanteEfecto > 0){
			yield return new WaitForSeconds(1.5f);
			switch(ultimoPoderEjecutado){
			case "poderCongelar":
				StartCoroutine(monstruosNivel[indice].poderCongelar(tiempoRestanteEfecto));
				break;
			case "poderDetenerTiempo":
				StartCoroutine(monstruosNivel[indice].poderDetenerTiempo(tiempoRestanteEfecto));
				break;
			}
		}
		//resetea el numero de pelos si el monstruo cambia en la misma estacion del peluquero
		if(estacionActual == indice){
			camaraObj.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
			tijeras.gameObject.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
		}
	}
	
	void iniciar(){}
	void estacionSiguiente(){}
	void estacionAnterior(){}
	
	void cortarPelo(int pos){
		monstruosNivel[estacionActual].cortarPelo(pos, peluqueroPoderCorte);
		//print ("cortes " + nCortes);
		nCortes++;
		if (nCortes > 3 && tap.activeSelf) { 
			tap.gameObject.SetActive (false);
			tiltAnim.SetActive (true);

		}
	}
	//cantidad, exp por corte, topSegmento (0 o 1)
	void peloCortado(Vector3 datos){
		peluqueroNivel.peloCortado((datos[0] * datos[1]));
		addPuntaje ((int)(datos[0]) * puntajePorCorte);
		hairCuts += (int)(datos[0]);
		if(datos[2] == 1) topHairCuts++;
	}
	
	void OnGUI(){
		//GUI.Box (new Rect (0, Screen.height - 100, Screen.width, 100), "");
		/*if(GUI.Button (new Rect(0,Screen.height-30, 100, 30), "reset")){
			PlayerPrefs.SetInt ("puntajeTotal", 0);
			PlayerPrefs.SetInt ("poder1exp", 0);
			PlayerPrefs.SetInt ("poder2exp", 0);
			PlayerPrefs.SetInt ("nivelActual", 1);	
			PlayerPrefs.SetInt ("puntajeTotalJuego", 0);
			Application.LoadLevel ("Nivel");
		}*/
		//GUI.Box (new Rect( estacionActual * Screen.width / (estaciones - 1) - 10, 100, 20, 20), "^");
#if UNITY_EDITOR || UNITY_WEBPLAYER
		/*if(GUI.Button(new Rect(0,Screen.height/2, 100, 100), "")){
			swipeBack();
		}
		if(GUI.Button(new Rect(Screen.width-100,Screen.height/2, 100, 100), "")){
			swipe();
		}*/
#endif
	}
	
	void addPuntaje(int puntos){
		puntajeNivel += puntos;
		if(!modoTraining){
			PlayerPrefs.SetInt("challengeScore", PlayerPrefs.GetInt("challengeScore") + puntos);
			barraChallenge.sliderValue = (float)PlayerPrefs.GetInt("challengeScore") / (float)PlayerPrefs.GetInt("challengeStartScore");
		}
		string ceros = "";
		if(puntajeTotal + puntajeNivel < 10) ceros = "00000";
		else
			if(puntajeTotal + puntajeNivel < 100) ceros = "0000";
			else
				if(puntajeTotal + puntajeNivel < 1000) ceros = "000";
				else
					if(puntajeTotal + puntajeNivel < 10000) ceros = "00";
					else
						if(puntajeTotal + puntajeNivel < 100000) ceros = "0";
		//if(idLenguaje == 0) 
			if(PlayerPrefs.GetInt("training", 0) == 0)
				puntajeLabel.text = ""+ ceros + (puntajeTotal + puntajeNivel);
			else
				puntajeLabelAux.text = ""+ ceros + (puntajeTotal + puntajeNivel);
		//if(idLenguaje == 1) puntajeLabel.text = ""+ ceros + (puntajeTotal + puntajeNivel);
		if(barraEstrella != null) barraEstrella.SendMessage("actualizarPuntajeTotalAux", puntajeNivel);
	}
	
	void swipe(){
		if(estado == 0 || estado == 1){
			estacionActual++;
			if(estacionActual > estaciones - 1) estacionActual = estaciones - 1;
			if(estaciones > 1){
				swipeMessage.SetActive(false);
				camaraObj.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
				tijeras.gameObject.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
			}
		}
	}
	
	void swipeBack(){
		if(estado == 0 || estado == 1){
			estacionActual--;
			if(estacionActual < 0) estacionActual = 0;
			if(estaciones > 1){
				swipeMessage.SetActive(false);
				camaraObj.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
				tijeras.gameObject.SendMessage("setTotalPelos", monstruosNivel[estacionActual].totalPelos);
			}
		}
	}
	
	void reintentar(){
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevelName);	
	}
	
	public void pausarDesdePausa(){
		//barraEstrella.SendMessage("toggleBarras");
		pausaPreguntaGUI.gameObject.SetActive(false);
		pausar ();
	}
	
	public void pausar(){
		if(estado != 1 && estado != 2) return;
		pausado = !pausado;
		if(pausado){ 
			consejoTexto.SendMessage("reset");
			Time.timeScale = 0f;
			tiempoPausa = Time.time;
			estado = 2;
		}
		else{ 
			print ("trying");
			//pausaPreguntaGUI.Play (false);
			Time.timeScale = 1f;
			tiempoActual += Time.time - tiempoPausa;
			estado = 1;
		}
		toggleGUI(pausado, true);
		pausaPreguntaGUI.Play (pausado);
		gameObject.SendMessage("playSonido", sonidoManejo.tipoSonido.panel);
	}
	
	void toggleGUI(bool activar, bool afectarMenuPausa){
		for(int i = 0; i < elementosGUI.Length; i++){
			elementosGUI[i].Play(activar);
		}
		if(afectarMenuPausa){ 
			for(int i = 0; i < pausaGUI.Length; i++)
				pausaGUI[i].Play(activar);
		}
	}
	
	void checkAnimo(){
		int animoMonstruos = 0;
		for(int i = 0; i< monstruosNivel.Length; i++){
			if(monstruosNivel[i] != null){
				warningSign[i].position = new Vector3(-7000f, warningSign[i].position.y, warningSign[i].position.z);
				if(animoMonstruos < monstruosNivel[i].animo){
					animoMonstruos = monstruosNivel[i].animo;
				}
				if(monstruosNivel[i].animo > 2) warningSign[i].position = new Vector3(monstruosNivel[i].transform.position.x, warningSign[i].position.y, warningSign[i].position.z);
			}
			//animoMonstruos += monstruosNivel[i].animo;
		}
		
		//animoMonstruos = animoMonstruos / monstruosNivel.Length;
		switch(animoMonstruos){
		case 0:
			peluqueroNivel.setExpresion(1);
			break;
		case 1:
			peluqueroNivel.setExpresion(3);
			break;
		case 2:
			peluqueroNivel.setExpresion(4);
			break;
		case 3:
			peluqueroNivel.setExpresion(5);
			break;
		}
		//print ("animoMonstruos "+animoMonstruos);
	}
	
	void Update () {
		//if(estado == 0 || estado == 1){
		setTiempo();
		camaraObj.transform.position=Vector3.Lerp(camaraObj.transform.position, new Vector3(estacionActual*1000, camaraObj.transform.position.y, camaraObj.transform.position.z), 7*Time.deltaTime);
		//}
		//0: conteo
		//1: jugando
		//2: pausa
		//3: pierde vida
		//4: terminado
		switch(estado){
		case 0:
			if(Time.timeSinceLevelLoad > 2f){ 
				musica.PlayDelayed(1.5f);
				musica.PlayOneShot(sonidoGo, 0.5f);
				mostrarMensaje(Localization.Get("GO!"));
				tap.SetActive (true);
				//for(int i = 0; i < monstruosNivel.Length; i++)
				//	monstruosNivel[i].iniciar();
				peluqueroNivel.activar(true);
				tijeras.gameObject.SendMessage("activar", true);
				estado = 1;
				tiempoActual = Time.time;
			}
			break;
		case 1:
			if(tiltAnim.activeSelf){
				tiltAnim.SetActive(posActual.rangoAngulos() < 130f);
			}
			if(tiempoActual + tiempoNivel <= Time.time){
				//print (tiempoNivel + "/" + nivelActual);
				tiempoNivel += tiempoNivel / nivelActual;
				
				nivelActual++;
				definirEstaciones ();
				spawnMonstruos();
				/*pausarMonstruos(true);
				tijeras.gameObject.SendMessage("activar", false);
				peluqueroNivel.activar(false);
				estado = 4;
				if(idLenguaje == 0) mostrarMensaje("Level Passed!");
				if(idLenguaje == 1) mostrarMensaje("Nivel Pasado!");
				toggleGUI(true, false);
				int animoGeneral = 0;
				for(int i = 0; i < monstruosNivel.Length; i++) animoGeneral += monstruosNivel[i].animo;
				if(idLenguaje == 0) resumenScript.setInformacion("FINISHED!", puntajeNivel, bonusPerfect, poderesUsados * bonusPowers, bonusLives * peluqueroNivel.vidas, animoGeneral * bonusHappyness, expCollected);
				if(idLenguaje == 1) resumenScript.setInformacion("LO LOGRASTE!", puntajeNivel, bonusPerfect, poderesUsados * bonusPowers, bonusLives * peluqueroNivel.vidas, animoGeneral * bonusHappyness, expCollected);
				
				PlayerPrefs.SetInt("puntajeTotalJuego", PlayerPrefs.GetInt("puntajeTotalJuego", 0) + puntajeNivel + bonusPerfect + poderesUsados * bonusPowers + bonusLives * peluqueroNivel.vidas + animoGeneral * bonusHappyness);
				PlayerPrefs.SetInt ("puntajeTotal", puntajeTotal + puntajeNivel + bonusPerfect + poderesUsados * bonusPowers + bonusLives * peluqueroNivel.vidas + animoGeneral * bonusHappyness);
				
				//Playtomic.Log.LevelAverageMetric("puntaje", "nivel"+nivelActual, puntajeNivel + bonusPerfect + poderesUsados * bonusPowers + bonusLives * peluqueroNivel.vidas + animoGeneral * bonusHappyness);
				
				Vector2 expPoderes = peluqueroNivel.getExpPoderes();
				PlayerPrefs.SetInt ("poder1exp", (int) expPoderes.x);
				PlayerPrefs.SetInt ("poder2exp", (int) expPoderes.y);
				PlayerPrefs.SetInt ("nivelActual", nivelActual + 1);
				if(barraEstrella != null) barraEstrella.SendMessage("actualizarPuntajeTotal");
				resumenScript.activar(true);*/
			}
			else{
				if(tiempoCheckAnimo < Time.time){
					tiempoCheckAnimo = Time.time + 0.5f;
					checkAnimo();
				}
				/*if(musica.pitch != 1.5f && tiempoActual + tiempoNivel <= Time.time + 10){
					musica.pitch = 1.5f;
					print (musica.pitch);
				}*/
				
				//tiempoLabel.text = ""+Mathf.FloorToInt(tiempoActual + tiempoNivel - Time.time);
				if(Time.time > tiempoPuntaje){
					tiempoPuntaje = Time.time + 0.1f;
					addPuntaje(1);
				}
			}
			
			if(!modoTraining && (tiempochallenge - System.DateTime.Now) <= System.TimeSpan.Zero){
				challengePanel.Play(true);
				challengeFailPanel.Play(true);
				mostrarMensaje(Localization.Get("TIME IS UP!"));
				estado = 3;
			}
			
			if(!modoTraining && PlayerPrefs.GetInt("enChallenge") < 2 && PlayerPrefs.GetInt("challengeScore") >= PlayerPrefs.GetInt("challengeStartScore")){
				challengePanel.Play(true);
				challengeWinPanel.Play(true);
				PlayerPrefs.SetInt("enChallenge", 2);
				estado = 3;
			}

			if(PlayerPrefs.GetInt("tutorial7", 0) == 0 && puntajeNivel > 850f){
				mostrarMensaje(Localization.Get("FINISHED!"));
				estado = 3;
			}

			if(Input.GetKeyUp(KeyCode.RightArrow)) swipe();
			if(Input.GetKeyUp(KeyCode.LeftArrow)) swipeBack();
			
			break;
		case 2:
			
			break;
		case 3:
			if(mostrandoPerdiste) 
				
				return;
			
//			if(fl != null) fl.eliminarAds();

			if(PlayerPrefs.GetInt("enChallenge", 0) == 2 || PlayerPrefs.GetInt("tutorial7", 0) == 0){
				botonReintentar.SetActive(false);
				musica.PlayOneShot(sonidoVictoria);
				mostrarMensaje(Localization.Get("You did it!"));
				if(PlayerPrefs.GetInt("giftizMision1Completa", 0) == 0){
					PlayerPrefs.SetInt("giftizMision1Completa", 1);
					//GiftizBinding.missionComplete();
				}
			}
			/*else{
				if(idLenguaje == 0) mostrarMensaje("DANGER!");
				if(idLenguaje == 1) mostrarMensaje("PELIGRO!");
			}*/
			StartCoroutine(perdiste());
			break;
		case 4:
			
			break;
		}
		if((tiempochallenge - System.DateTime.Now) <= System.TimeSpan.Zero) tiempoLabel.text = "00:00:00";
	}
	
	void setTiempo(){
		if(modoTraining) return;
		/*float t = Time.time - tiempoActual;
		int milesimas = (int)(t * 10);
		milesimas = milesimas % 10;
		int minutos = (int)(t / 60);
		int segundos = (int)(t % 60);
		string minutosString = "0" + minutos;
		if(minutos > 9) minutosString = "" + minutos;
		string segundosString = "0" + segundos;
		if(segundos > 9) segundosString = "" + segundos;
		tiempoLabel.text = minutosString + ":" + segundosString + "." + milesimas;*/
		string t = PlayerPrefs.GetString("challengeStopTime");
		System.TimeSpan tiempo_ = System.DateTime.Parse (t) - System.DateTime.Now;
		string h = (((tiempo_.Hours + 24 * tiempo_.Days) >= 10) ? ("" + (tiempo_.Hours + 24 * tiempo_.Days)) : ("0" + (tiempo_.Hours + 24 * tiempo_.Days)));
		string m = ((tiempo_.Minutes >= 10) ? ("" + (tiempo_.Minutes)) : ("0" + (tiempo_.Minutes)));
		string s = ((tiempo_.Seconds >= 10) ? ("" + (tiempo_.Seconds)) : ("0" + (tiempo_.Seconds)));
		string tiempoActual = h + ":" + m + ":" + s;

		tiempoLabel.text = "" + tiempoActual;
		
	}
	
	void nextLevel(){
		
		int animoGeneral = 0;
		for(int i = 0; i < monstruosNivel.Length; i++) animoGeneral += monstruosNivel[i].animo;
		
		if(nivelActual % 3 == 0 && PlayerPrefs.GetInt("minijuego0", 0) > 0){
			int minijuegosDesbloqueados = 0;
			for(int i = 0; i < 10; i++){
				if(PlayerPrefs.GetInt("minijuego" + i, 0) == 1) minijuegosDesbloqueados++;
			}
			Application.LoadLevel ("Minijuego" + Random.Range (0, minijuegosDesbloqueados));
		}
		else{
			if((nivelActual) % 5 == 0)
				Application.LoadLevel ("Historia");
			else{
				print("aux: " + nivelActual + " " + globalVariables.estacionesNivel[nivelActual - 1]);
				if(PlayerPrefs.GetInt ("monstruo1desbloqueado", 0) == 1 && globalVariables.estacionesNivel[PlayerPrefs.GetInt ("nivelActual", 1) - 1] > 1){
					Application.LoadLevel ("Ruleta");
				}
				else Application.LoadLevel("Loading");
			}
		}
	}
	
	/*void desaparecerPausaPregunta(){
		if(pausaPreguntaGUI.direction == AnimationOrTween.Direction.Reverse){
			pausaPreguntaGUI.gameObject.SetActive(false);
			for(int i = 0; i < pausaGUI.Length; i++)
				pausaGUI[i].Play(true);
		}
	}*/

	void pausaMapa(){
		salirAMapa = true;
		//Playtomic.Log.LevelCounterMetric("salir", "nivel"+nivelActual);
		pausaPreguntaGUI.gameObject.SetActive(true);
		pausaPreguntaGUI.Play (true);
		for(int i = 0; i < pausaGUI.Length; i++)
			pausaGUI[i].Play(false);
	}

	void pausaStore(){
		salirAMapa = false;
		//Playtomic.Log.LevelCounterMetric("salir", "nivel"+nivelActual);
		pausaPreguntaGUI.gameObject.SetActive(true);
		pausaPreguntaGUI.Play (true);
		for(int i = 0; i < pausaGUI.Length; i++)
			pausaGUI[i].Play(false);
	}
	
	void pausaExitNo(){
		pausaPreguntaGUI.Play (false);
		for(int i = 0; i < pausaGUI.Length; i++)
			pausaGUI[i].Play(true);
	}
	
	void pausaExitYes(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("salirDesdePausa", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("salirDesdePausa");
		#endif
		Application.LoadLevel(salirAMapa?"Mapa":"Store");
	}
	
	void finishedGameTrofeos(){
		Application.LoadLevel("Trofeos");
	}
	
	void finishedGameExit(){
		//if(!autorizadoPasar) return;
		//Playtomic.Log.CustomMetric("rankingJuego", "Nivel");
		Application.LoadLevel("Mapa");	
	}
	
	/*IEnumerator saveScore()
	{
		var score = new Playtomic_PlayerScore();
		score.Name = SystemInfo.deviceUniqueIdentifier;
		score.Points = PlayerPrefs.GetInt("puntajeTotal");
		score.CustomData.Add("nombre", PlayerPrefs.GetString("nombre", "Player"));
		score.CustomData.Add("peluquero", "" + PlayerPrefs.GetInt("peluqueroSeleccionado", 0));
		print("enviando");
		yield return StartCoroutine(Playtomic.Leaderboards.Save("highscores", score, true, true));
		var response = Playtomic.Leaderboards.GetResponse("Save");
		if(response.Success)
		{
			Playtomic.Log.CustomMetric("scoreSuccess", "Bugs");
			autorizadoPasar = true;
			Handheld.StopActivityIndicator();
			print("Score saved!");
		}
		else
		{
			Playtomic.Log.CustomMetric("scoreFail", "Bugs");
			autorizadoPasar = true;
			Handheld.StopActivityIndicator();
			print("Score failed to save because of " + response.ErrorCode + ": " + response.ErrorDescription);
		}
	}*/
	
	IEnumerator perdiste(){
		if(!mostrandoPerdiste){

			if(PlayerPrefs.GetInt("enChallenge", 0) == 2 || PlayerPrefs.GetInt("tutorial7", 0) == 0){
				peluqueroNivel.setExpresion(2, true);
			}
			else
				peluqueroNivel.setExpresion(6);
			//Playtomic.Log.LevelCounterMetric("death", "nivel"+nivelActual); // level names must be alphanumeric


			camaraObj.SendMessage("activar", false);
			tijeras.gameObject.SendMessage("activar", false);
			peluqueroNivel.activar(false);
			mostrandoPerdiste = true;
			pausarMonstruos(true);
			//multiplico por 10 para tener los milisegundos
			int tiempoTermino = (int)((Time.time - tiempoActual) * 10);
			toggleGUI(true, false);
			
			yield return new WaitForSeconds(1);
			//if(peluqueroNivel.vidas - 1 < 0){
			if(PlayerPrefs.GetInt("enChallenge", 0) != 2 && PlayerPrefs.GetInt("tutorial7", 0) == 1){
				musica.Stop();
				musica.PlayOneShot(musicaFinal);
			}
			//}
			yield return new WaitForSeconds(2);

			if(peluqueroNivel.vidas - 1 < 0 || PlayerPrefs.GetInt("enChallenge", 0) == 2 || PlayerPrefs.GetInt("tutorial7", 0) == 0){
				#if UNITY_IPHONE
//				//FlurryAnalytics.logEvent("juegoTerminado" + nivelActual, false );
				#endif		
				#if UNITY_ANDROID
//				FlurryAndroid.logEvent("juegoTerminado" + nivelActual);
				#endif
				//Playtomic.Log.LevelCounterMetric("gameOver", "nivel"+nivelActual);
				//Playtomic.Log.LevelAverageMetric("puntajeFinal", "nivel"+nivelActual, puntajeTotal + puntajeNivel);
				estado = 4;	
				//si se pasó el challenge
				string mensajeFinal;
				if(PlayerPrefs.GetInt("enChallenge", 0) == 2 || PlayerPrefs.GetInt("tutorial7", 0) == 0){
					mensajeFinal = Localization.Get((PlayerPrefs.GetInt("tutorial7", 0) == 0)?"Tutorial Passed!":"Challenge Passed!");
					botonFacebookPublicar.SendMessage("setMensaje", Localization.Get("I had defeated a barbershop with") + " " + PlayerPrefs.GetInt("challengeStartScore") + " " + Localization.Get("points. Can you beat me?"));
				}
				else{
					mensajeFinal = "HAIR GOD!";
					if(puntajeNivel < 200) mensajeFinal = Localization.Get("Not bad");
					else 
						if(puntajeNivel < 500) mensajeFinal = Localization.Get("Good!");
						else 
						    if(puntajeNivel < 1500) mensajeFinal = Localization.Get("Well Done!");
							else 
						         if(puntajeNivel < 3000) mensajeFinal = Localization.Get("Great performance!");
					botonFacebookPublicar.SendMessage("setMensaje", Localization.Get("I'm getting stronger scissors every second. I've got") + " " + puntajeNivel + " " + Localization.Get("points. Can you beat me?") );
				}
				mostrarMensaje(mensajeFinal);
				yield return new WaitForSeconds(2);
				//PlayerPrefs.SetInt ("puntajeTotal", puntajeTotal + puntajeNivel);
				PlayerPrefs.SetInt("tiempoMejor", Mathf.Max(tiempoTermino, PlayerPrefs.GetInt("tiempoMejor", 0)));
				peluqueroNivel.terminar();
				float mult = 0.2f;
				if(multiplicadorMonedas>0) mult = multiplicadorMonedas;
				gameFinishedScript.setInformacion(tiempoTermino ,puntajeTotal + puntajeNivel, clientesAtendidos, (int)expCollected, (int)(clientesAtendidos * mult));

				gameObject.SendMessage("playSonido", sonidoManejo.tipoSonido.panel);

				bool makeUpgrade = PlayerPrefs.GetInt("mejoraOro", 0) > 0 ||
									PlayerPrefs.GetInt("mejoraVelocidad", 0) > 0 ||
									PlayerPrefs.GetInt("mejoraFuerza", 0) > 0 ||
									PlayerPrefs.GetInt("mejoraApariciones", 0) > 0 ||
									PlayerPrefs.GetInt("mejoraCD1", 0) > 0 ||
									PlayerPrefs.GetInt("mejoraCD2", 0) > 0;
				bool makeUpgradeFull = PlayerPrefs.GetInt("mejoraOro", 0) > 4 ||
									PlayerPrefs.GetInt("mejoraVelocidad", 0) > 4 ||
									PlayerPrefs.GetInt("mejoraFuerza", 0) > 4 ||
									PlayerPrefs.GetInt("mejoraApariciones", 0) > 4 ||
									PlayerPrefs.GetInt("mejoraCD1", 0) > 4 ||
									PlayerPrefs.GetInt("mejoraCD2", 0) > 4;
				bool hability5 = false;
				if(PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0) hability5 = PlayerPrefs.GetInt("peluquero0Poder1", 0) >= 5 || PlayerPrefs.GetInt("peluquero0Poder2", 0) >= 5;
				if(PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1) hability5 = PlayerPrefs.GetInt("peluquero1Poder1", 0) >= 5 || PlayerPrefs.GetInt("peluquero1Poder2", 0) >= 5;
				if(PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2) hability5 = PlayerPrefs.GetInt("peluquero2Poder1", 0) >= 5 || PlayerPrefs.GetInt("peluquero2Poder2", 0) >= 5;
				bool useSpecialScissor = PlayerPrefs.GetInt("objetoTijeraFuerza", 0) > 0 ||
									PlayerPrefs.GetInt("objetoTijeraVelocidad", 0) > 0 ||
									PlayerPrefs.GetInt("objetoTijeraDorada", 0) > 0;
				/*bool useVisualPel1 = PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0 &&(
									PlayerPrefs.GetInt("peluquero0Traje0", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero0Traje1", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero0Traje2", 0) == 2);
				bool useVisualPel2 = PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1 &&(
									PlayerPrefs.GetInt("peluquero1Traje0", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero1Traje1", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero1Traje2", 0) == 20);
				bool useVisualPel3 = PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2 &&(
									PlayerPrefs.GetInt("peluquero2Traje0", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero2Traje1", 0) == 2 ||
									PlayerPrefs.GetInt("peluquero2Traje2", 0) == 2);*/
				int[] valores = {hairCuts, topHairCuts, clientesAtendidos, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder2usos:0, 
								(int)tiempoTermino, makeUpgrade?1:0, hairCuts, topHairCuts, clientesAtendidos,
					PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder2usos:0, (int)tiempoTermino, useSpecialScissor?1:0, (PlayerPrefs.GetInt("objetoPeluqueriaActivado", 0) > 1)?1:0,
								PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?1:0, vidasUsadas > 0?1:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder2usos:0, hairCuts,
					PlayerPrefs.GetInt("objeto2oro", 0) > 0?1:0, (PlayerPrefs.GetInt("objetoPeluqueriaActivado", 0) > 3)?1:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?1:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?poder2usos:0,
								PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 0?poder2usos:0, clientesAtendidos, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder2usos:0,
					(int)tiempoTermino, vidasUsadas > 1?1:0, (PlayerPrefs.GetInt("objetoPeluqueriaActivado", 0) > 5)?1:0, hairCuts, vidasUsadas > 2?1:0,
					PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?poder1usos:0, (PlayerPrefs.GetInt("objetoPeluqueriaActivado", 0) > 7)?1:0, PlayerPrefs.GetInt("objeto3oro", 0) > 0?1:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder1usos:0,
								PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 1?poder2usos:0, hairCuts, topHairCuts, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?poder1usos:0, PlayerPrefs.GetInt("peluqueroSeleccionado", 0) == 2?poder2usos:0,
								hability5?1:0, makeUpgradeFull?1:0};
				//print (valores[5]+" "+valores[6]+" "+valores[7]+" "+valores[8]+" "+valores[9]);
				gameFinishedScript.revisarObjetivos(valores);
				gameFinishedScript.activar(true);

				GameObject.FindGameObjectWithTag("sugerencias").SendMessage("mostrarPanel");
				//PlayerPrefs.SetInt("puntajeTotalJuego", PlayerPrefs.GetInt("puntajeTotalJuego", 0) + puntajeNivel);
				//PlayerPrefs.SetInt("puntajeMejor", Mathf.Max(puntajeTotal + puntajeNivel, PlayerPrefs.GetInt("puntajeMejor", 0)));
				if(barraEstrella != null) barraEstrella.SendMessage("actualizarPuntajeTotal");

				if(PlayerPrefs.GetInt("tutorial7", 0) == 0) tutorialFinal.SetActive(true);
				
//				if(fl != null) fl.eliminarAds();

				print("enviar info");
				//StartCoroutine(saveScore());
			}
			else{
				mostrarMensaje(Localization.Get("EXTRA LIFE!"));
				TweenPosition tp = vidasPanel.GetComponent<TweenPosition>();
				TweenScale ts = vidasPanel.GetComponent<TweenScale>();
				tp.Play(true);
				ts.Play(true);
				yield return new WaitForSeconds(2);
				musica.Play();
			}
		}
	}

	void setPoderUpgrade(){
		gameFinishedScript.SendMessage ("setPoderUpgrade");
	}
	
	void disminuirVidas(){
		TweenPosition tp = vidasPanel.GetComponent<TweenPosition>();
		TweenScale ts = vidasPanel.GetComponent<TweenScale>();
		if(tp.direction == AnimationOrTween.Direction.Forward){
			peluqueroNivel.vidas--;
			PlayerPrefs.SetInt("vidas", peluqueroNivel.vidas);
			vidasLabel.text = "x  " + peluqueroNivel.vidas;
			vidasUsadas++;
			tp.Play(false);
			ts.Play(false);
			toggleGUI(false, false);
			resetNivel();	
		}
		else{
			if(peluqueroNivel.vidas <= 0) vidasPanel.SetActive(false);
		}
	}
	
	void goToStore(){
		Application.LoadLevel("Store");	
	}
	
	void pausarMonstruos(bool p){
		for(int i = 0; i < monstruosNivel.Length; i++)
			if(monstruosNivel[i] != null) monstruosNivel[i].pausar(p);
	}
	void resetNivel(){
		print ("reseteando");
		for(int i = 0; i < monstruosNivel.Length; i++)
			monstruosNivel[i].reset();
		peluqueroNivel.activar(true);
		tijeras.gameObject.SendMessage("activar", true);
		camaraObj.SendMessage("activar", true);
		estado = 1;
		mostrandoPerdiste = false;
	}
	
	void monstruoCompleto(){
		if(estado == 1){ 
			bonusPerfect = 0;
			estado = 3;
		}
		
	}
	
	void addExp(float e){
		expCollected += e;	
	}
	
	/*void checkMonstruos(){
		for(int i = 0; i < monstruosNivel.Length; i++)
			monstruosNivel[i].
	}*/
	
	void mostrarMensaje(string m){
		mensajeLabel.SendMessage("setMensaje", m);
	}
	
	void iniciarReloj(float tiempo){
		Transform t = (Transform) Instantiate (reloj, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10), Quaternion.identity);
		t.gameObject.SendMessage("setTiempoVida", tiempo);
		//StartCoroutine();	
	}
	
	void peluqueroCorteAutomatico(bool activado){
		tijeras.gameObject.SendMessage("activarAutomatico", activado);
	}
	
	void peluqueroPoderFuerza(bool activado){
		tijeras.gameObject.SendMessage("activarFuerza", activado);
		if(activado){ 
			peluqueroPoderCorteAux = peluqueroPoderCorte;
			peluqueroPoderCorte = Mathf.Clamp(peluqueroPoderCorte * 2, 30, 100);
		}
		else{
			peluqueroPoderCorte = peluqueroPoderCorteAux;	
		}
	}
	
	void poderCortarPeloCompleto(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderCortarPelo", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderCortarPelo");
		#endif
		poder1usos++;
		monstruosNivel[estacionActual].poderCortarPeloCompleto(nivel, posActual.pos);
	}
	
	void poderCongelar(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderCongelar", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderCongelar");
		#endif
		poder2usos++;
		StartCoroutine(monstruosNivel[estacionActual].poderCongelar(5 + 3 * nivel));
	}
	
	void poderCortarPuntas(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderCortarPuntas", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderCortarPuntas");
		#endif
		poder1usos++;
		monstruosNivel[estacionActual].poderCortarPuntas(nivel, posActual.pos);
	}
	
	void poderAutoCorte(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderAutoCorte", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderAutoCorte");
		#endif
		poder2usos++;
		StartCoroutine(monstruosNivel[estacionActual].poderAutoCorte(5 + 3 * nivel));
	}
	
	void poderDetenerTiempo(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderDetenerTiempo", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderDetenerTiempo");
		#endif
		poder1usos++;
		StartCoroutine(monstruosNivel[estacionActual].poderDetenerTiempo(3 + nivel));
	}
	
	void poderFuerza(int nivel){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("poderFuerza", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("poderFuerza");
		#endif
		poder2usos++;
		StartCoroutine(monstruosNivel[estacionActual].poderFuerza(3 + nivel));
	}
	
}

