using UnityEngine;
using System.Collections;

public class itemPeluqueria : MonoBehaviour {
	//-1: desactivado, 0: normal, 1: en challenge, 2: terminado
	public int estado;
	public int id = 0;
	public float hours;
	int seconds;
	public int score;
	public int lives;
	
	public int prizeGold;
	public int prizeExp;
	public string prizePlayerPref;

	public string nombrePeluqueria;
	public UILabel[] puntosLabel;
	public UILabel[] tiempoLabel;
	public UILabel[] nombreLabel;
	public UILabel[] goldLabel;
	public UILabel[] expLabel;
	public UISprite[] premioSprite;
	public UISprite vidasSprite;
	
	public GameObject objetoDesactivado;
	public GameObject objetoSimple;
	public GameObject objetoChallenge;
	public GameObject objetoInChallenge;
	public GameObject objetoPasado;
	
	public bool detalles = false;
	
	public UILabel timeLeft;
	public UISprite livesLeft;
	public UILabel scoreLeft;
	public UISprite reloj;
	public UILabel newChance;
	
	public UISlider barraPuntaje;
	int scoreActual;
	
	System.DateTime tiempochallenge;
	string tiempoNextChance;
	
	public TweenPosition panelFail;
	public TweenPosition panelWin;
	
	progresionMapa mapaScript;
	public bool desbloquear = false;
	//Localization loc;
	public int forzarMonstruoId = -1;

	public bool cargado = false;
	
	// Use this for initialization
	void Start () {
		
		//loc = Localization.instance;

		GameObject cam = GameObject.FindWithTag("MainCamera");
		mapaScript = cam.GetComponent<progresionMapa>();
		setEstado(PlayerPrefs.GetInt("estadoPeluqueria" + id, -1));
		actualizarIndicadores();
		
		seconds = (int)(hours * 60f * 60f);
		//print(PlayerPrefs.GetInt("challengeId") + ", " + PlayerPrefs.GetInt("enChallenge") + ", " + PlayerPrefs.GetString("challengeStopTime") + ", " + PlayerPrefs.GetInt("challengeLives") + ", " + PlayerPrefs.GetInt("challengeStartScore") + ", " + PlayerPrefs.GetInt("challengeScore") + ", " + PlayerPrefs.GetString("challengeNextChance"));
		
		if(PlayerPrefs.HasKey("challengeStopTime")) 
			tiempochallenge = System.DateTime.Parse(PlayerPrefs.GetString("challengeStopTime"));
		
		if(estado == 1 && PlayerPrefs.GetInt("challengeLives", 0) <= 0){
			tiempoNextChance = PlayerPrefs.GetString("challengeNextChance", "");	
			if(tiempoNextChance == ""){
				tiempoNextChance = System.DateTime.Now.AddMinutes(60).ToString();
				PlayerPrefs.SetString("challengeNextChance", tiempoNextChance);
			}
			else{
				if((System.DateTime.Parse(tiempoNextChance) - System.DateTime.Now) <= System.TimeSpan.Zero){
					tiempoNextChance = "";
					PlayerPrefs.SetString("challengeNextChance", tiempoNextChance);
					PlayerPrefs.SetInt("challengeLives", 1);
					actualizarIndicadores();
				}
			}
		}
		if (PlayerPrefs.GetInt ("enChallenge") == 2 && PlayerPrefs.GetInt ("challengeId") == -1) {
			PlayerPrefs.SetInt("enChallenge", 0);
			PlayerPrefs.SetInt("challengeId", -1);
			setEstado(0);
		}
		print("pel id: "+PlayerPrefs.GetInt("challengeId") + ", enChallenge: "+PlayerPrefs.GetInt("enChallenge", 0));
		if(PlayerPrefs.GetInt("enChallenge") == 2 && PlayerPrefs.GetInt("challengeId") == id){
			challengeSuccess();
		}
		/*if(PlayerPrefs.GetInt("enChallenge") == 1 && PlayerPrefs.GetInt("challengeId") == id){
			setEstado(1);
		}*/

		if (desbloquear) {
			if(estado < 0) setEstado(0);
			desbloquear = false;
		}
		cargado = true;
		inicializarContenido ();

	}

	public void testDesbloquear(){
		if(estado < 0) setEstado(0);
		desbloquear = false;
	}

	void inicializarContenido(){
		foreach (UILabel l in nombreLabel) {
			l.text = nombrePeluqueria;
		}
		foreach (UILabel p in puntosLabel) {
			p.text = score + "pts";
		}
		foreach (UILabel t in tiempoLabel) {
			string horas = "";
			if(hours < 10) horas = "0";
			horas += "" + Mathf.FloorToInt(hours);
			string minutos = "";
			float min = (hours - Mathf.FloorToInt(hours)) * 60f;
			int min2 = Mathf.FloorToInt(min);
			if(min2 < 10) minutos = "0";
			minutos += "" + min2;
			t.text = horas + ":" + minutos + ":00";
		}
		foreach (UILabel g in goldLabel) {
			g.text = "" + prizeGold;
		}
		foreach (UILabel e in expLabel) {
			e.text = prizeExp + "XP";
		}
		if (prizePlayerPref == "") {
			foreach (UISprite p in premioSprite) {
				p.gameObject.SetActive(false);
			}
		}
		vidasSprite.width = lives * vidasSprite.height;
		vidasSprite.gameObject.SetActive (lives > 0);
	}
	
	void actualizarIndicadores(){
		scoreActual = PlayerPrefs.GetInt("challengeScore", 0);
		livesLeft.width = (PlayerPrefs.GetInt("challengeLives", 0)) * livesLeft.height;
		livesLeft.gameObject.SetActive (PlayerPrefs.GetInt("challengeLives", 0) > 0);
		scoreLeft.text = "" + PlayerPrefs.GetInt("challengeScore")+ " / " + score;
		barraPuntaje.sliderValue = (float)PlayerPrefs.GetInt("challengeScore") / (float)score;
	}
	
	void verDetalles(){
		if(id == 1 ){
		   GameObject t = GameObject.FindWithTag("tutorial");
		   if(t != null){ print("enviado"); t.SendMessage("evento", 1, SendMessageOptions.RequireReceiver);}
		}
		if (PlayerPrefs.GetInt ("enChallenge", 0) == 1)
			return;
		detalles = true;	
		setEstado(estado);
	}
	
	void esconderDetalles(){
		detalles = false;	
		setEstado(estado);
	}
	//-1 bloqueado
	//0 disponible
	//1 en challenge
	//2 pasado
	public void setEstado(int e){
		estado = e;
		switch(estado){
		case -1:
			objetoDesactivado.SetActive(true);
			objetoSimple.SetActive(false);
			objetoChallenge.SetActive(false);
			objetoInChallenge.SetActive(false);
			objetoPasado.SetActive(false);
			GetComponent<AudioSource>().Stop();
			break;
		case 0:
			objetoDesactivado.SetActive(false);
			objetoSimple.SetActive(!detalles);
			objetoChallenge.SetActive(detalles);
			objetoInChallenge.SetActive(false);
			objetoPasado.SetActive(false);
			GetComponent<AudioSource>().Stop();
			break;
		case 1:
			objetoDesactivado.SetActive(false);
			objetoSimple.SetActive(false);
			objetoChallenge.SetActive(false);
			objetoInChallenge.SetActive(true);
			objetoPasado.SetActive(false);
			GetComponent<AudioSource>().Play();
			break;
		case 2:
			objetoDesactivado.SetActive(false);
			objetoSimple.SetActive(false);
			objetoChallenge.SetActive(false);
			objetoInChallenge.SetActive(false);
			objetoPasado.SetActive(true);
			GetComponent<AudioSource>().Stop();
			break;
		}
		print ("estado "+estado+" set "+ id);
		PlayerPrefs.SetInt("estadoPeluqueria" + id, estado);
		if(mapaScript != null) mapaScript.adaptar();
	}
	
	void challenge(){
		if(id == 1 ){
			GameObject t = GameObject.FindWithTag("tutorial");
			if(t != null){ print("enviado"); t.SendMessage("evento", 3, SendMessageOptions.RequireReceiver);}
		}
		print ("en challenge "+PlayerPrefs.GetInt ("enChallenge", 0)+" id: "+PlayerPrefs.GetInt("challengeId"));
		if(PlayerPrefs.GetInt("enChallenge", 0) == 0){
			PlayerPrefs.SetInt("challengeId", id);
			PlayerPrefs.SetInt("enChallenge", 1);
			PlayerPrefs.SetString("challengeStopTime", System.DateTime.Now.AddSeconds(seconds).ToString());//(int)System.Convert.ToInt64(System.DateTime.Now.Ticks / 10000000) - 63509500000 + seconds);
			PlayerPrefs.SetInt("challengeLives", lives);
			PlayerPrefs.SetInt("challengeStartScore", score);
			PlayerPrefs.SetInt("challengeScore", 0);
			tiempoNextChance = "";
			newChance.text = "Fight!";
			PlayerPrefs.SetString("challengeNextChance", tiempoNextChance);
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent("challenge" + id, true );
			#endif		
			#if UNITY_ANDROID
			//FlurryAndroid.logEvent("challenge" + id, true);
			#endif
			tiempochallenge = System.DateTime.Parse(PlayerPrefs.GetString("challengeStopTime"));
			actualizarIndicadores();
			
			setEstado(1);
		}
	}
	
	void Go(){
		if(PlayerPrefs.GetInt("challengeLives", 0) > 0){
			if(forzarMonstruoId >= 0) PlayerPrefs.SetInt("forzarMonstruo", forzarMonstruoId);
			else PlayerPrefs.SetInt("forzarMonstruo", -1);
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent("challengeGo" + id, false );
			#endif		
			#if UNITY_ANDROID
			//FlurryAndroid.logEvent("challengeGo" + id);
			#endif
			PlayerPrefs.SetString("escenaCargar", "Nivel");
			Application.LoadLevel("Loading");	
		}
	}
	
	void Cancelar(){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("challengeCancel" + id, false );
		#endif		
		#if UNITY_ANDROID
		//FlurryAndroid.logEvent("challengeCancel" + id);
		#endif
		challengeFail();	
	}
	
	void challengeFail(){
		//Camera.main.gameObject.audio.PlayOneShot(sonidoFail);
		#if UNITY_IPHONE
		//FlurryAnalytics.endTimedEvent("challenge" + id);
		#endif		
		#if UNITY_ANDROID
		//FlurryAndroid.endTimedEvent("challenge" + id);
		#endif
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("challengeFail" + id, false );
		#endif		
		#if UNITY_ANDROID
		//FlurryAndroid.logEvent("challengeFail" + id);
		#endif
		PlayerPrefs.SetInt("enChallenge", 0);
		PlayerPrefs.SetInt("challengeId", -1);
		//gameObject.SendMessage ("playSonido", sonidoManejo.tipoSonido.panel);
		panelFail.Play(true);
		esconderDetalles();
		setEstado(0);
	}
	
	void challengeSuccess(){
		//Camera.main.gameObject.audio.PlayOneShot(sonidoWin);
		PlayerPrefs.SetInt("challengeId", -1);
		PlayerPrefs.SetInt("enChallenge", 0);
		#if UNITY_IPHONE
		//FlurryAnalytics.endTimedEvent("challenge" + id);
		#endif		
		#if UNITY_ANDROID
		//FlurryAndroid.endTimedEvent("challenge" + id);
		#endif
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("challengeSuccess" + id, false );
		#endif		
		#if UNITY_ANDROID
		//FlurryAndroid.logEvent("challengeSuccess" + id);
		#endif
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas") + prizeGold);
		int expf = PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"exp") + prizeExp;
		PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"exp", Mathf.Clamp(expf, 0, globalVariables.niveles[globalVariables.niveles.Length -1]));
		if(PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel") < globalVariables.niveles.Length && expf >= globalVariables.niveles[PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel")]){
			PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel", Mathf.Clamp(PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel") + 1, 0, globalVariables.niveles.Length));
		}
		esconderDetalles();
		PlayerPrefs.SetInt(prizePlayerPref, 1);
		//gameObject.SendMessage ("playSonido", sonidoManejo.tipoSonido.panel);
		panelWin.Play(true);
		GameObject g = panelWin.gameObject;
		panelWinMapa p = g.GetComponent<panelWinMapa>();
		
		string unlockTexto = "";
		switch(prizePlayerPref){
		case "monstruo1desbloqueado":
			unlockTexto = "Werewolf "+ Localization.Get("Unlocked!");
			break;
		case "monstruo2desbloqueado":
			unlockTexto = "Mummy " + Localization.Get("Unlocked!");
			break;
		case "monstruo3desbloqueado":
			unlockTexto = "Vampire " + Localization.Get("Unlocked!");
			break;
		}
		
		p.setInformacion(prizeGold, prizeExp, unlockTexto);
		setEstado(2);
		
		mapaScript.adaptar();
		print("challenge success");
		
		GameObject.FindGameObjectWithTag("sugerencias").SendMessage("mostrarPanel");
	}
	
	// Update is called once per frame
	void Update () {
		if(estado == 1){
			if((tiempochallenge - System.DateTime.Now) <= System.TimeSpan.Zero) challengeFail();
			else{
				System.TimeSpan tiempo_ = (tiempochallenge - System.DateTime.Now);
				string h = (((tiempo_.Hours + 24 * tiempo_.Days) >= 10) ? ("" + (tiempo_.Hours + 24 * tiempo_.Days)) : ("0" + (tiempo_.Hours + 24 * tiempo_.Days)));
				string m = ((tiempo_.Minutes >= 10) ? ("" + (tiempo_.Minutes)) : ("0" + (tiempo_.Minutes)));
				string s = ((tiempo_.Seconds >= 10) ? ("" + (tiempo_.Seconds)) : ("0" + (tiempo_.Seconds)));
				string tiempoActual = h + ":" + m + ":" + s;
				timeLeft.text = tiempoActual;
				reloj.fillAmount = (float)(tiempochallenge - System.DateTime.Now).TotalSeconds / seconds;
				if(PlayerPrefs.GetInt("challengeLives", 0) <= 0){
					print (tiempoNextChance);
					string t = (System.DateTime.Parse(tiempoNextChance) - System.DateTime.Now).ToString();
					newChance.text = Localization.Get("New life at")+ "\n" + t.Substring(0, 8);
					if((System.DateTime.Parse(tiempoNextChance) - System.DateTime.Now) <= System.TimeSpan.Zero){
						tiempoNextChance = "";
						PlayerPrefs.SetString("challengeNextChance", tiempoNextChance);
						PlayerPrefs.SetInt("challengeLives", 1);
						actualizarIndicadores();
					}
				}
				
			}
		}
	}
}
