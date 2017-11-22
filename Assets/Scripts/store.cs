using UnityEngine;
using System.Collections;

public class store : MonoBehaviour {
	//public IAPNemoris IAPNemorisStore;
	bool mostrarItemsMonedas = false;
	public UILabel monedasLabel;
	int monedas = 0;
	int addMonedas = 0;
	public GameObject panelPeluqueros;
	public GameObject panelMejoras;
	public GameObject panelVisuales;
	public GameObject panelObjetos;
	public GameObject panelMonedas;
	public GameObject scrollPeluqueros;
	public GameObject scrollMejoras;
	public GameObject scrollVisuales;
	public GameObject scrollObjetos;
	public GameObject scrollMonedas;
	public itemPeluquero[] itemsPeluquero;
	public itemMejora[] itemsMejora;
	public itemVisual[] itemsVisual;
	public itemObjeto[] itemsObjeto;
	public itemMoneda[] itemsMoneda;
	public GameObject[] itemsMonedaGameObjects;

	SugerenciasCompra sugerenciasCompra;
	int idMonedaActual = 0;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindGameObjectWithTag ("sugerencias");
		if(g != null) sugerenciasCompra = g.GetComponent<SugerenciasCompra> ();
		//PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("peluquero0", 1);
		//PlayerPrefs.SetInt("monedas", 1000);
		//productosCargados(true);
		monedas = PlayerPrefs.GetInt("monedas", 0);
		monedasLabel.text = "" + monedas;
		actualizarItemsPeluqueros();
		actualizarItemsMejora();
		//actualizarItemsVisual();
		actualizarItemsObjeto();
		//mostrarPanelMejoras();
		mostrarPanelPeluqueros();
		//panelPeluqueros.GetComponent<UIScrollView> ().ResetPosition ();
		//scrollPeluqueros.GetComponent<UIScrollBar> ().barSize = 0.86f;
		#if UNITY_IPHONE
		//la version de android se hace en la clase directamente
		IAPNemorisStore.cargarProductos();
		#endif		

	}
	
	void compraExitosa(int monedas){
		addMonedas += monedas;	
		PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) + monedas);

		#if UNITY_IPHONE
		////FlurryAnalytics.logEvent( "monedasCompradas" + monedas, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("monedasCompradas" + monedas);
		#endif
		sugerenciasCompra.mostrarPanelComprado(itemsMoneda[idMonedaActual].titulo.text, itemsMoneda[idMonedaActual].imagen);
	}
	
	void compraFallida(int error){
		print ("compra fallida, error: "+error);
		#if UNITY_IPHONE
		////FlurryAnalytics.logEvent( "compraFallida", false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("compraFallida");
		#endif
	}
	
	void productosCargados(bool cargados){
		/*print ("productos cargados: "+cargados);
		mostrarItemsMonedas = cargados;
		for(int i = 0; i < itemsMonedaGameObjects.Length; i++){ 
			#if UNITY_IPHONE || UNITY_ANDROID 
			itemsMoneda[i].currencyLabel.text = "" + IAPNemorisStore.productos[i].currency;
			itemsMoneda[i].precioLabel.text = "" + IAPNemorisStore.productos[i].precio;
			#endif
			itemsMonedaGameObjects[i].SetActive(cargados);
		}*/
	}
	
	void comprarMonedas0(){ comprarMonedas(0); }
	void comprarMonedas1(){ comprarMonedas(1); }
	void comprarMonedas2(){ comprarMonedas(2); }
	void comprarMonedas3(){ comprarMonedas(3); }
	void comprarMonedas4(){ comprarMonedas(4); }
	
	void comprarObjeto0(){ comprarObjeto(0); }
	void comprarObjeto1(){ comprarObjeto(1); }
	void comprarObjeto2(){ comprarObjeto(2); }
	void comprarObjeto3(){ comprarObjeto(3); }
	void comprarObjeto4(){ comprarObjeto(4); }
	void comprarObjeto5(){ comprarObjeto(5); }
	void comprarObjeto6(){ comprarObjeto(6); }
	
	void comprarPeluquero0(){ comprarPeluquero(0); }
	void upgradeLevelPeluquero0(){ upgradeLevelPeluquero(0); }
	void upgradeLevelPoder1Peluquero0(){ upgradeLevelPoder1Peluquero (0); }
	void upgradeLevelPoder2Peluquero0(){ upgradeLevelPoder2Peluquero (0); }
	void comprarPeluquero1(){ comprarPeluquero(1); }
	void upgradeLevelPeluquero1(){ upgradeLevelPeluquero(1); }
	void upgradeLevelPoder1Peluquero1(){ upgradeLevelPoder1Peluquero (1); }
	void upgradeLevelPoder2Peluquero1(){ upgradeLevelPoder2Peluquero (1); }
	void comprarPeluquero2(){ comprarPeluquero(2); }
	void upgradeLevelPeluquero2(){ upgradeLevelPeluquero(2); }
	void upgradeLevelPoder1Peluquero2(){ upgradeLevelPoder1Peluquero (2); }
	void upgradeLevelPoder2Peluquero2(){ upgradeLevelPoder2Peluquero (2); }
	
	void comprarItem0(){ comprarItem(0); }
	void comprarItem1(){ comprarItem(1); }
	void comprarItem2(){ comprarItem(2); }
	void comprarItem3(){ comprarItem(3); }
	void comprarItem4(){ comprarItem(4); }
	void comprarItem5(){ comprarItem(5); }
	
	void comprarVisual0(){ comprarVisual(0); }
	void comprarVisual1(){ comprarVisual(1); }
	void comprarVisual2(){ comprarVisual(2); }
	void comprarVisual3(){ comprarVisual(3); }
	void comprarVisual4(){ comprarVisual(4); }
	void comprarVisual5(){ comprarVisual(5); }
	void comprarVisual6(){ comprarVisual(6); }
	void comprarVisual7(){ comprarVisual(7); }
	void comprarVisual8(){ comprarVisual(8); }
	void comprarVisual9(){ comprarVisual(9); }
	
	void activarVisual0(bool ev){ activarVisual(0, ev); }
	void activarVisual1(bool ev){ activarVisual(1, ev); }
	void activarVisual2(bool ev){ activarVisual(2, ev); }
	void activarVisual3(bool ev){ activarVisual(3, ev); }
	void activarVisual4(bool ev){ activarVisual(4, ev); }
	void activarVisual5(bool ev){ activarVisual(5, ev); }
	void activarVisual6(bool ev){ activarVisual(6, ev); }
	void activarVisual7(bool ev){ activarVisual(7, ev); }
	void activarVisual8(bool ev){ activarVisual(8, ev); }
	void activarVisual9(bool ev){ activarVisual(9, ev); }
	
	void comprarMonedas(int id){
		//if(IAPNemorisStore.esPosibleComprar() && IAPNemorisStore.compraTerminada){
		/*if(IAPNemorisStore.compraTerminada){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "monedasComprar" + id, false );
			#endif 
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("monedasComprar" + id);
			# endif 
			idMonedaActual = id;
			IAPNemorisStore.comprar(id);	
		}*/
	}
	
	void comprarObjeto(int id){
		if(comprar (int.Parse(itemsObjeto[id].precioLabel.text))){
			PlayerPrefs.SetInt(itemsObjeto[id].playerPref, PlayerPrefs.GetInt(itemsObjeto[id].playerPref, 0) + 1);
			actualizarItemsObjeto();
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "comprarObjeto"+id, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("comprarObjeto"+id);
			#endif
			sugerenciasCompra.mostrarPanelComprado(itemsObjeto[id].titulo.text, itemsObjeto[id].imagen);
		}
	}
	
	void activarVisual(int id, bool active){
		print ("activar " + id + " " + active);
		#if UNITY_IPHONE
		////FlurryAnalytics.logEvent( "visualActivado" + id, false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("visualActivado" + id);
		#endif
		PlayerPrefs.SetInt(itemsVisual[id].playerPref, active?2:1);
	}
	
	void comprarVisual(int id){
		if(comprar (int.Parse(itemsVisual[id].precioLabel.text))){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "visualComprado" + id, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("visualComprado" + id);
			#endif
			PlayerPrefs.SetInt(itemsVisual[id].playerPref, 1);
			actualizarItemsVisual();
		}
	}
	
	void comprarItem(int id){
		if(comprar (int.Parse(itemsMejora[id].precioLabel.text))){
			PlayerPrefs.SetInt(itemsMejora[id].playerPref, PlayerPrefs.GetInt(itemsMejora[id].playerPref) + 1);
			actualizarItemsMejora();
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "comprarMejora"+id, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("comprarMejora"+id);
			#endif
			sugerenciasCompra.mostrarPanelComprado(itemsMejora[id].titulo.text, itemsMejora[id].imagen);
		}
	}
	
	void actualizarItemsObjeto(){
		for(int i = 0; i < itemsObjeto.Length; i++){
			itemsObjeto[i].actualizar();	
		}	
	}
	
	void actualizarItemsVisual(){
		for(int i = 0; i < itemsVisual.Length; i++){
			itemsVisual[i].actualizar();	
		}	
	}
	
	void actualizarItemsMejora(){
		for(int i = 0; i < itemsMejora.Length; i++){
			itemsMejora[i].actualizar();	
		}	
	}
	
	void actualizarItemsPeluqueros(){
		for(int i = 0; i < itemsPeluquero.Length; i++){
			itemsPeluquero[i].actualizar();	
		}	
	}
	
	void upgradeLevelPoder1Peluquero(int id){
		if(comprar (int.Parse(itemsPeluquero[id].precioUpgradeLevelPoder1.text))){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "upgradePeluquero"+id+"poder1n"+PlayerPrefs.GetInt("peluquero" + id + "Poder1", 0) + 1, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("upgradePeluquero"+id+"poder1n"+PlayerPrefs.GetInt("peluquero" + id + "Poder1", 0) + 1);
			#endif
			PlayerPrefs.SetInt("peluquero" + id + "Poder1", PlayerPrefs.GetInt("peluquero" + id + "Poder1", 0) + 1);
			actualizarItemsPeluqueros();
		}	
	}
	
	void upgradeLevelPoder2Peluquero(int id){
		if(comprar (int.Parse(itemsPeluquero[id].precioUpgradeLevelPoder2.text))){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "upgradePeluquero"+id+"poder2n"+PlayerPrefs.GetInt("peluquero" + id + "Poder2", 0) + 1, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("upgradePeluquero"+id+"poder2n"+PlayerPrefs.GetInt("peluquero" + id + "Poder2", 0) + 1);
			#endif
			PlayerPrefs.SetInt("peluquero" + id + "Poder2", PlayerPrefs.GetInt("peluquero" + id + "Poder2", 0) + 1);
			actualizarItemsPeluqueros();
		}	
	}
	
	void upgradeLevelPeluquero(int id){
		if(comprar (int.Parse(itemsPeluquero[id].precioUpgradeLevel.text))){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "upgradePeluquero"+id+"n"+PlayerPrefs.GetInt("peluquero" + id + "nivel", 1) + 1, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("upgradePeluquero"+id+"n"+PlayerPrefs.GetInt("peluquero" + id + "nivel", 1) + 1);
			#endif
			PlayerPrefs.SetInt("peluquero" + id + "nivel", PlayerPrefs.GetInt("peluquero" + id + "nivel", 1) + 1);
			PlayerPrefs.SetInt("peluquero" + id + "exp", globalVariables.niveles[PlayerPrefs.GetInt("peluquero" + id + "nivel") - 1]);
			actualizarItemsPeluqueros();
		}
	}
	
	void comprarPeluquero(int id){
		if(comprar (int.Parse(itemsPeluquero[id].precioUnlock.text))){
			#if UNITY_IPHONE
			////FlurryAnalytics.logEvent( "comprarPeluquero"+id, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("comprarPeluquero"+id);
			#endif
			PlayerPrefs.SetInt("peluquero" + id, 1);
			actualizarItemsPeluqueros();
			sugerenciasCompra.mostrarPanelComprado(itemsPeluquero[id].titulo.text, itemsPeluquero[id].imagen);
		}
	}
	
	bool comprar(int costo){
		if(PlayerPrefs.GetInt("monedas", 0) >= costo){
			addMonedas -= costo;	
			PlayerPrefs.SetInt("monedas", PlayerPrefs.GetInt("monedas", 0) - costo);
			return true;
		}
		return false;
	}
	
	void mostrarPanelPeluqueros(){
		/*reposicionarTweens();
		panelPeluqueros.Play(true);
		panelMejoras.Play(false);
		panelVisuales.Play(false);
		panelObjetos.Play(false);
		panelMonedas.Play(false);*/
		panelPeluqueros.SetActive(true);
		panelMejoras.SetActive(false);
		panelVisuales.SetActive(false);
		panelObjetos.SetActive(false);
		panelMonedas.SetActive(false);
		scrollPeluqueros.SetActive(true);
		scrollMejoras.SetActive(false);
		scrollVisuales.SetActive(false);
		scrollObjetos.SetActive(false);
		scrollMonedas.SetActive(false);
	}
	void mostrarPanelMejoras(){
		/*reposicionarTweens();
		panelPeluqueros.Play(false);
		panelMejoras.Play(true);
		panelVisuales.Play(false);
		panelObjetos.Play(false);
		panelMonedas.Play(false);*/
		panelPeluqueros.SetActive(false);
		panelMejoras.SetActive(true);
		panelVisuales.SetActive(false);
		panelObjetos.SetActive(false);
		panelMonedas.SetActive(false);
		scrollPeluqueros.SetActive(false);
		scrollMejoras.SetActive(true);
		scrollVisuales.SetActive(false);
		scrollObjetos.SetActive(false);
		scrollMonedas.SetActive(false);

		GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 2, SendMessageOptions.RequireReceiver);}
	}
	void mostrarPanelVisuales(){
		/*reposicionarTweens();
		panelPeluqueros.Play(false);
		panelMejoras.Play(false);
		panelVisuales.Play(true);
		panelObjetos.Play(false);
		panelMonedas.Play(false);*/
		panelPeluqueros.SetActive(false);
		panelMejoras.SetActive(false);
		panelVisuales.SetActive(true);
		panelObjetos.SetActive(false);
		panelMonedas.SetActive(false);
		scrollPeluqueros.SetActive(false);
		scrollMejoras.SetActive(false);
		scrollVisuales.SetActive(true);
		scrollObjetos.SetActive(false);
		scrollMonedas.SetActive(false);
	}
	
	void mostrarPanelObjetos(){
		/*reposicionarTweens();
		panelPeluqueros.Play(false);
		panelMejoras.Play(false);
		panelVisuales.Play(false);
		panelObjetos.Play(true);
		panelMonedas.Play(false);*/
		panelPeluqueros.SetActive(false);
		panelMejoras.SetActive(false);
		panelVisuales.SetActive(false);
		panelObjetos.SetActive(true);
		panelMonedas.SetActive(false);
		scrollPeluqueros.SetActive(false);
		scrollMejoras.SetActive(false);
		scrollVisuales.SetActive(false);
		scrollObjetos.SetActive(true);
		scrollMonedas.SetActive(false);
		GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 4, SendMessageOptions.RequireReceiver);}
	}
	void mostrarPanelMonedas(){
		/*reposicionarTweens();
		panelPeluqueros.Play(false);
		panelMejoras.Play(false);
		panelVisuales.Play(false);
		panelObjetos.Play(false);
		panelMonedas.Play(true);*/
		panelPeluqueros.SetActive(false);
		panelMejoras.SetActive(false);
		panelVisuales.SetActive(false);
		panelObjetos.SetActive(false);
		panelMonedas.SetActive(true);
		scrollPeluqueros.SetActive(false);
		scrollMejoras.SetActive(false);
		scrollVisuales.SetActive(false);
		scrollObjetos.SetActive(false);
		scrollMonedas.SetActive(true);
		GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 6, SendMessageOptions.RequireReceiver);}
	}
	
	/*void reposicionarTweens(){
		panelPeluqueros.from = new Vector3(panelPeluqueros.from.x, panelPeluqueros.transform.position.y, panelPeluqueros.from.z);
		panelPeluqueros.to = new Vector3(panelPeluqueros.to.x, panelPeluqueros.transform.position.y, panelPeluqueros.to.z);
		panelMejoras.from = new Vector3(panelMejoras.from.x, panelMejoras.transform.position.y, panelMejoras.from.z);
		panelMejoras.to = new Vector3(panelMejoras.to.x, panelMejoras.transform.position.y, panelMejoras.to.z);
		panelVisuales.from = new Vector3(panelVisuales.from.x, panelVisuales.transform.position.y, panelVisuales.from.z);
		panelVisuales.to = new Vector3(panelVisuales.to.x, panelVisuales.transform.position.y, panelVisuales.to.z);
		panelObjetos.from = new Vector3(panelObjetos.from.x, panelObjetos.transform.position.y, panelObjetos.from.z);
		panelObjetos.to = new Vector3(panelObjetos.to.x, panelObjetos.transform.position.y, panelObjetos.to.z);
		panelMonedas.from = new Vector3(panelMonedas.from.x, panelMonedas.transform.position.y, panelMonedas.from.z);
		panelMonedas.to = new Vector3(panelMonedas.to.x, panelMonedas.transform.position.y, panelMonedas.to.z);
	}*/
	
	void salir(){
		//IAPNemorisStore.eliminarEventListeners();
		PlayerPrefs.SetString("escenaCargar", "Titulo");
		Application.LoadLevel("Loading");
	}
	
	void irPeluqueria(){
		Application.LoadLevel("Peluqueria");	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Titulo");
		}
		if(addMonedas != 0){
			int incremento = globalVariables.calcularIncremento(addMonedas);
			monedas += (int)(Mathf.Sign(addMonedas)) * incremento;
			monedasLabel.text = "" + monedas;
			addMonedas -= (int)(Mathf.Sign(addMonedas)) * incremento;
		}
	}
}

[System.Serializable]
public class itemMoneda{
	public UILabel precioLabel;
	public UILabel currencyLabel;
	public UILabel titulo;
	public UISprite imagen;
	
	public void actualizar(){
		
	}
}

[System.Serializable]
public class itemObjeto{
	public string playerPref;
	public UILabel precioLabel;
	public UILabel cantidadLabel;
	public UILabel titulo;
	public UISprite imagen;
	
	public void actualizar(){
		cantidadLabel.text = "x " + PlayerPrefs.GetInt(playerPref, 0);
	}
}

[System.Serializable]
public class itemVisual{
	public int id = 0;
	public UILabel precioLabel;
	public GameObject botonCompra;
	GameObject activarCheckboxG;
	public UIToggle activarCheckbox;
	//-1: no cumple requisito
	//0: no comprado
	//1: comprado, no activado
	//2: comprado y activado
	public string playerPref;
	public string playerPrefRequisito;
	
	public void actualizar(){
		activarCheckboxG = activarCheckbox.gameObject;
		int requisito = playerPrefRequisito == ""?1:PlayerPrefs.GetInt(playerPrefRequisito, 0);
		int estado = requisito == 1?PlayerPrefs.GetInt(playerPref, 0):-1;
		switch(estado){
		case -1:
			activarCheckboxG.SetActive(false);
			botonCompra.SetActive(false);
			break;
		case 0:
			activarCheckboxG.SetActive(false);
			botonCompra.SetActive(true);
			break;
		case 1:
			activarCheckboxG.SetActive(true);
			activarCheckbox.isChecked = false;
			botonCompra.SetActive(false);
			break;
		case 2:
			activarCheckboxG.SetActive(true);
			activarCheckbox.isChecked = true;
			botonCompra.SetActive(false);
			break;
		}
		//activarCheckbox.functionName = "activarVisual" + (id);
	}
}

[System.Serializable]
public class itemMejora{
	public UISprite[] nivelesImagen;
	public GameObject[] descripciones;
	public string playerPref;
	public UILabel precioLabel;
	public GameObject botonComprar;
	public int precioBase = 20;
	public int precioIncremento = 30;
	int nivel;
	int precio;
	public UILabel titulo;
	public UISprite imagen;
	
	public void actualizar(){
		nivel = PlayerPrefs.GetInt(playerPref, 0);
		precio = nivel * precioIncremento + precioBase;
		precioLabel.text = "" + precio;
		for(int i = 0; i < nivelesImagen.Length; i++){
			descripciones[i].SetActive(i == nivel);
			nivelesImagen[i].color = new Color(nivelesImagen[i].color.r, nivelesImagen[i].color.g, nivelesImagen[i].color.b, nivel<=i?0.5f:15f);
		}
		
		if(nivel == 5){ 
			botonComprar.SetActive(false);
			descripciones[4].SetActive(true);
		}
	}
}

[System.Serializable]
public class itemPeluquero{
	public int idPeluquero = 0;
	public GameObject[] itemLocked;
	public GameObject[] itemUnlocked;
	public UILabel precioUnlock;
	public UILabel precioUpgradeLevel;
	public UILabel precioUpgradeLevelPoder1;
	public UILabel precioUpgradeLevelPoder2;
	public UILabel levelPoder1;
	public UILabel levelPoder2;
	public UILabel expLabel;
	public UILabel nivel;
	public UISlider expBarra;
	public GameObject botonComprarLevel;
	public GameObject botonComprarLevelPoder1;
	public GameObject botonComprarLevelPoder2;
	int poder1Nivel;
	int poder2Nivel;
	int poder1Precio;
	int poder2Precio;
	
	public UILabel titulo;
	public UISprite imagen;
	
	public void actualizar(){
		Localization loc = Localization.instance;
		precioUnlock.text = "" + globalVariables.costoPeluquero[idPeluquero];
		for(int i = 0; i < itemLocked.Length; i++){
			itemLocked[i].SetActive(PlayerPrefs.GetInt("peluquero"+idPeluquero, 0) == 0);	
		}
		for(int i = 0; i < itemUnlocked.Length; i++){
			itemUnlocked[i].SetActive(PlayerPrefs.GetInt("peluquero"+idPeluquero, 0) == 1);	
		}
		//si el peluquero esta desbloqueado
		if(PlayerPrefs.GetInt("peluquero"+idPeluquero, 0) == 1){
			int nivelFinal = PlayerPrefs.GetInt("peluquero"+idPeluquero+"nivel", 1);
			int expFinal = PlayerPrefs.GetInt("peluquero"+idPeluquero+"exp", 0);
			if(nivelFinal >= globalVariables.niveles.Length){
				botonComprarLevel.SetActive(false);
				nivel.text = Localization.Get("Level") + " " +"10";
				expBarra.sliderValue = 1f;
				expLabel.text = "" + expFinal + " / " + expFinal;
			}
			else{
				int costoLevelUp = (int)((globalVariables.niveles[nivelFinal] - expFinal) * 0.1f);
				precioUpgradeLevel.text = "" + costoLevelUp;
				
				nivel.text = Localization.Get("Level") + " " + nivelFinal;
				//nivel.text=((float)expFinal +"+"+ 1 +"-"+ (float)globalVariables.niveles[nivelFinal - 1] +"/"+ (float)globalVariables.niveles[nivelFinal] +"-"+ (float)globalVariables.niveles[nivelFinal - 1]);
				expBarra.sliderValue = expFinal == 0?0f:((float)expFinal + 1 - (float)globalVariables.niveles[nivelFinal - 1]) / ((float)globalVariables.niveles[nivelFinal] - (float)globalVariables.niveles[nivelFinal - 1]);
				expLabel.text = "" + expFinal + " / " + globalVariables.niveles[nivelFinal];
			}
			setPoderUpgrade();
		}
	}
	
	void setPoderUpgrade(){
		poder1Nivel = PlayerPrefs.GetInt("peluquero"+idPeluquero+"Poder1", 0);
		poder2Nivel = PlayerPrefs.GetInt("peluquero"+idPeluquero+"Poder2", 0);
		levelPoder1.text = "" + poder1Nivel;
		levelPoder2.text = "" + poder2Nivel;
		poder1Precio = 10 + poder1Nivel * 20;
		poder2Precio = 10 + poder2Nivel * 20;
		precioUpgradeLevelPoder1.text = "" + poder1Precio;
		precioUpgradeLevelPoder2.text = "" + poder2Precio;
		if(poder1Nivel >= 9) botonComprarLevelPoder1.SetActive(false);
		if(poder2Nivel >= 9) botonComprarLevelPoder2.SetActive(false);
	}
	
}
