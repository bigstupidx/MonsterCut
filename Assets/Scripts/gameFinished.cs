using UnityEngine;
using System.Collections;

public class gameFinished : MonoBehaviour {
	public UILabel scoreLabel;
	public UILabel clientsLabel;
	public UILabel timeLabel;
	public UILabel bestLabel;
	public UILabel tipsLabel;
	
	public UISlider expBarra;
	public UILabel expLabel;
	public UILabel levelLabel;
	
	public UILabel costoLevelUpLabel;
	int costoLevelUp = 0;
	
	//barraTrofeos barraTrofeosActual;
	public UILabel nombreLabel;
	public UISprite peluqueroImagen;
	public bool activarBarra =  false;
	public int expCollected = 0;
	int level = 1;
	int exp = 0;
	public UILabel monedasLabel;
	public GameObject botonComprarLevel;
	int addMonedas = 0;
	int monedas = 0;
	TweenPosition tween;
	
	public TweenPosition misionesPanel;
	public mision misionScript;
	
	public UILabel poder1NivelLabel;
	int poder1Nivel;
	public GameObject poder1UpgradeBoton;
	public UILabel poder1PrecioLabel;
	int poder1Precio;
	public UISprite poder1Imagen;
	public UILabel poder2NivelLabel;
	int poder2Nivel;
	public GameObject poder2UpgradeBoton;
	public UILabel poder2PrecioLabel;
	int poder2Precio;
	public UISprite poder2Imagen;
	
	public Transform[] objetosChallenge;
	// Use this for initialization
	void Start () {
		/*GameObject barraEstrella = GameObject.FindWithTag("barraEstrella");
		if(barraEstrella != null){
			barraTrofeosActual = barraEstrella.GetComponent<barraTrofeos>();
			limites = barraTrofeosActual.obtenerLimitesBarra();
			barraProgresion.sliderValue = ((PlayerPrefs.GetInt("puntajeTotalJuego") - limites.x) / (limites.y - limites.x));
			logroActualImagen.spriteName = barraTrofeosActual.logroImagen.spriteName;
			logroActualImagen.color = barraTrofeosActual.logroImagen.color;
			logroActualTexto.text = barraTrofeosActual.logroMensaje.text;
		}*/
		tween = gameObject.GetComponent<TweenPosition>();
		tween.Play(false);
	}
	
	public void revisarObjetivos(int[] valores){
		int monedasRetorno = misionScript.revisarMisiones(valores);
		addMonedas += monedasRetorno;
		
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + monedasRetorno);
	}
	
	public void setInformacion(int tiempoJugado, int totalScore, int clientes, int expCollected, int tips){
		
		scoreLabel.text = "" + totalScore;
		this.expCollected = expCollected;
		clientsLabel.text = "" + clientes;
		timeLabel.text = calcularReloj(tiempoJugado);
		bestLabel.text = calcularReloj(PlayerPrefs.GetInt("tiempoMejor", 0));
		tipsLabel.text = "" + tips;
		addMonedas = tips;
		monedas = PlayerPrefs.GetInt("monedas", 0);
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + tips);
		
		setBotonComprarNivel();
		
		for(int i = 0; i < objetosChallenge.Length; i++){
			objetosChallenge[i].parent = transform;
			objetosChallenge[i].localPosition += new Vector3(-750, -100, 0);
		}
	}
	
	void setBotonComprarNivel(){
		int nivelFinal = PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel");
		int expFinal = PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"exp");
		print ("finales " + nivelFinal + " " + expFinal);
		if (nivelFinal >= globalVariables.niveles.Length) {
			botonComprarLevel.SetActive(false);
		}
		else{
			costoLevelUp = (int)((globalVariables.niveles[nivelFinal] - expFinal) * 0.1f);
			costoLevelUpLabel.text = "" + costoLevelUp;
		}
	}
	
	void comprarNivel(){
		if(PlayerPrefs.GetInt("monedas", 0) >= costoLevelUp){
			addMonedas -= costoLevelUp;	
			PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) - costoLevelUp);
			PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"nivel", level + 1);
			PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"exp", globalVariables.niveles[level]);
			expCollected = globalVariables.niveles[level] - exp;
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent("upgradePeluquero" + PlayerPrefs.GetInt("peluqueroSeleccionado", 0) + "n" + level, false );
			#endif		
			#if UNITY_ANDROID
			//FlurryAndroid.logEvent("upgradePeluquero" + PlayerPrefs.GetInt("peluqueroSeleccionado", 0) + "n" + level);
			#endif
			setBotonComprarNivel();
		}
	}
	
	public void setPeluquero(string nombre, string retrato, int exp, int level, string poder1, string poder2){
		Localization loc = Localization.instance;
		poder1Imagen.spriteName = poder1;
		poder2Imagen.spriteName = poder2;
		nombreLabel.text = nombre;
		peluqueroImagen.spriteName = retrato;
		if (level >= globalVariables.niveles.Length) expLabel.text = "";
		else expLabel.text = "" + exp + " / " + globalVariables.niveles[level];
		levelLabel.text = Localization.Get("Level") + " " + level;
		this.level = level;
		this.exp = exp;
		setPoderUpgrade();
		if (level >= globalVariables.niveles.Length) expBarra.sliderValue = 1f;
		else expBarra.sliderValue = ((float)exp - (float)globalVariables.niveles[level - 1]) / ((float)globalVariables.niveles[level] - (float)globalVariables.niveles[level - 1]);
	}
	
	void setPoderUpgrade(){
		poder1Nivel = PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder1", 0);
		poder2Nivel = PlayerPrefs.GetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder2", 0);
		poder1NivelLabel.text = "" + poder1Nivel;
		poder2NivelLabel.text = "" + poder2Nivel;
		poder1Precio = 10 + poder1Nivel * 20;
		poder2Precio = 10 + poder2Nivel * 20;
		poder1PrecioLabel.text = "" + poder1Precio;
		poder2PrecioLabel.text = "" + poder2Precio;
		if(poder1Nivel >= 9) poder1UpgradeBoton.SetActive(false);
		if(poder2Nivel >= 9) poder2UpgradeBoton.SetActive(false);
	}
	
	void comprarPoder1(){
		if(PlayerPrefs.GetInt("monedas", 0) >= poder1Precio){
			addMonedas -= poder1Precio;
			PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas") - poder1Precio);
			poder1Nivel++;
			PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder1", poder1Nivel);
			setPoderUpgrade();
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent( "upgradePeluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"poder1n" + poder1Nivel, false );
			#endif		
			#if UNITY_ANDROID
			//FlurryAndroid.logEvent("upgradePeluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"poder1n" + poder1Nivel);
			#endif
		}
	}
	
	void comprarPoder2(){
		if(PlayerPrefs.GetInt("monedas", 0) >= poder2Precio){
			addMonedas -= poder2Precio;
			PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas") - poder2Precio);
			poder2Nivel++;
			PlayerPrefs.SetInt("peluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0)+"Poder2", poder2Nivel);
			setPoderUpgrade();
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent( "upgradePeluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0) + poder2Nivel, false );
			#endif		
			#if UNITY_ANDROID
			//FlurryAndroid.logEvent("upgradePeluquero"+PlayerPrefs.GetInt("peluqueroSeleccionado", 0) + poder2Nivel);
			#endif
		}
	}
	
	string calcularReloj(int valor){
		//ya vienen en milisegundos
		int milesimas = (int)(valor);
		milesimas = milesimas % 10;
		valor = valor / 10;
		int minutos = (int)(valor / 60);
		int segundos = (int)(valor % 60);
		string minutosString = "0" + minutos;
		if(minutos > 9) minutosString = "" + minutos;
		string segundosString = "0" + segundos;
		if(segundos > 9) segundosString = "" + segundos;
		
		return minutosString + ":" + segundosString + "." + milesimas;
	}
	
	public void activar(bool activo){
		tween.Play (activo);	
		misionesPanel.Play(activo);
	}
	
	void panelPosicionado(){
		if(Vector3.Distance(gameObject.transform.position, tween.to) < 30)
			activarBarra = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(activarBarra){
			if(expCollected > 0){
				int incremento = globalVariables.calcularIncremento(expCollected);
				exp += incremento;
				if(level < globalVariables.niveles.Length){
					expBarra.sliderValue = ((float)exp - (float)globalVariables.niveles[level - 1]) / ((float)globalVariables.niveles[level] - (float)globalVariables.niveles[level - 1]);
					expLabel.text = "" + exp + " / " + globalVariables.niveles[level];
					if(exp >= globalVariables.niveles[level]){
						level++;
						levelLabel.text = "Level " + level;
						expLabel.text = "" + exp + " / " + globalVariables.niveles[level];
						expBarra.sliderValue = ((float)exp - (float)globalVariables.niveles[level - 1]) / ((float)globalVariables.niveles[level] - (float)globalVariables.niveles[level - 1]);
					}
				}
				else{
					expBarra.sliderValue = 1f;
					expLabel.text = "";
				}

				
				expCollected -= incremento;
			}
			if(addMonedas != 0){
				int incremento = globalVariables.calcularIncremento(addMonedas);
				monedas += (int)(Mathf.Sign(addMonedas)) * incremento;
				monedasLabel.text = "" + monedas;
				addMonedas -= (int)(Mathf.Sign(addMonedas)) * incremento;
			}
			//print (PlayerPrefs.GetInt("puntajeTotalJuego") +"-"+ limites.x +"/"+ limites.y +"-"+ limites.x);
			/*float pTotal = (PlayerPrefs.GetInt("puntajeTotalJuego") - limites.x) / (limites.y - limites.x); 
			barraProgresion.sliderValue = Mathf.Lerp(barraProgresion.sliderValue, Mathf.Clamp( pTotal, 0f, 1f), 3 * Time.deltaTime);
			if(barraProgresion.sliderValue >= 1f && logroActualImagen.spriteName != barraTrofeosActual.logroImagen.spriteName){
				logroActualImagen.spriteName = barraTrofeosActual.logroImagen.spriteName;
				logroActualImagen.color = barraTrofeosActual.logroImagen.color;
				logroActualTexto.text = barraTrofeosActual.logroMensaje.text;
			}*/
		}
	}
}
