using UnityEngine;
using System.Collections;

public class storePeluqueria : MonoBehaviour {
	
	public UILabel monedasLabel;
	public TweenPosition[] paneles;
	int monedas = 0;
	int addMonedas = 0;
	public itemesPeluqueria[] items;
	public UIScrollBar scroll;
	public GameObject objetoPeluqueria;
	public UIPanel panelPrincipal;
	
	float valorPanelInicial = 0f;
	
	bool panelesActivos = false;
	SugerenciasCompra sugerenciasCompra;
	
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindGameObjectWithTag ("sugerencias");
		if(g != null) sugerenciasCompra = g.GetComponent<SugerenciasCompra> ();
		//PlayerPrefs.SetInt("monedas", 3000);
		monedas = PlayerPrefs.GetInt("monedas", 0);
		monedasLabel.text = "" + monedas;
		actualizarItems();
		valorPanelInicial = panelPrincipal.clipRange.y;

		//desbloquea la ventana top
		if(PlayerPrefs.GetInt(items[3].playerPref, 0) <= 0) PlayerPrefs.SetInt(items[3].playerPref, 1);
		actualizarItems();
	}
	
	void togglePanel(){
		panelesActivos = !panelesActivos;
		foreach(TweenPosition tp in paneles){
			tp.Play(panelesActivos);	
		}
		//panelPrincipal.clipRange = new Vector4(panelPrincipal.clipRange.x,valorPanelInicial, panelPrincipal.clipRange.z, panelPrincipal.clipRange.w);
		panelPrincipal.clipRange = new Vector4(panelPrincipal.clipRange.x, valorPanelInicial, panelPrincipal.clipRange.z, panelPrincipal.clipRange.w);
		panelPrincipal.clipOffset = Vector2.zero;
		scroll.value = 0f;
		GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 2, SendMessageOptions.RequireReceiver);}
	}
	
	void irStore(){
		Application.LoadLevel("Store");	
	}
	
	void salir(){
		PlayerPrefs.DeleteKey("idFacebookAmigo");
		Application.LoadLevel("mapa");	
	}

	public void comprar0(){ comprarItem(0); }
	public void comprar1(){ comprarItem(1); }
	public void comprar2(){ comprarItem(2); }
	public void comprar3(){ comprarItem(3); }
	public void comprar4(){ comprarItem(4); }
	public void comprar5(){ comprarItem(5); }
	public void comprar6(){ comprarItem(6); }
	public void comprar7(){ comprarItem(7); }
	public void comprar8(){ comprarItem(8); }
	public void comprar9(){ comprarItem(9); }
	public void comprar10(){ comprarItem(10); }
	public void comprar11(){ comprarItem(11); }
	public void comprar12(){ comprarItem(12); }
	public void comprar13(){ comprarItem(13); }
	public void comprar14(){ comprarItem(14); }
	public void comprar15(){ comprarItem(15); }
	public void comprar16(){ comprarItem(16); }
	public void comprar17(){ comprarItem(17); }
	public void comprar18(){ comprarItem(18); }
	public void comprar19(){ comprarItem(19); }
	public void comprar20(){ comprarItem(20); }
	public void comprar21(){ comprarItem(21); }
	public void comprar22(){ comprarItem(22); }
	public void comprar23(){ comprarItem(23); }
	public void comprar24(){ comprarItem(24); }
	public void comprar25(){ comprarItem(25); }
	public void comprar26(){ comprarItem(26); }
	
	public void activar0(bool ev){ activar(0, ev); }
	public void activar1(bool ev){ activar(1, ev); }
	public void activar2(bool ev){ activar(2, ev); }
	public void activar3(bool ev){ activar(3, ev); GameObject t = GameObject.FindWithTag("tutorial");
		if(t != null){ print("enviado"); t.SendMessage("evento", 4, SendMessageOptions.RequireReceiver);}}
	public void activar4(bool ev){ activar(4, ev); }
	public void activar5(bool ev){ activar(5, ev); }
	public void activar6(bool ev){ activar(6, ev); }
	public void activar7(bool ev){ activar(7, ev); }
	public void activar8(bool ev){ activar(8, ev); }
	public void activar9(bool ev){ activar(9, ev); }
	public void activar10(bool ev){ activar(10, ev); }
	public void activar11(bool ev){ activar(11, ev); }
	public void activar12(bool ev){ activar(12, ev); }
	public void activar13(bool ev){ activar(13, ev); }
	public void activar14(bool ev){ activar(14, ev); }
	public void activar15(bool ev){ activar(15, ev); }
	public void activar16(bool ev){ activar(16, ev); }
	public void activar17(bool ev){ activar(17, ev); }
	public void activar18(bool ev){ activar(18, ev); }
	public void activar19(bool ev){ activar(19, ev); }
	public void activar20(bool ev){ activar(20, ev); }
	public void activar21(bool ev){ activar(21, ev); }
	public void activar22(bool ev){ activar(22, ev); }
	public void activar23(bool ev){ activar(23, ev); }
	public void activar24(bool ev){ activar(24, ev); }
	public void activar25(bool ev){ activar(25, ev); }
	public void activar26(bool ev){ activar(26, ev); }
	
	void activar(int id, bool active){
		if(PlayerPrefs.GetInt(items[id].playerPref, 0) == 0) return;
		//print ("activar " + id + " " + active);
		#if UNITY_IPHONE
//		if(active) //FlurryAnalytics.logEvent("activar_"+items[id].playerPref, false );
		#endif		
		#if UNITY_ANDROID
//		if(active) FlurryAndroid.logEvent("activar_"+items[id].playerPref);
		#endif
		PlayerPrefs.SetInt(items[id].playerPref, active?2:1);
		//if(active){
		//	print("actualiza peluqueria");
			objetoPeluqueria.SendMessage("crearPeluqueriaPropia", true);
		//}

		actualizarItems();
		//actualizarBD
	}
	
	void actualizarItems(){
		int activados = 0;
		for(int i = 0; i < items.Length; i++){
			if(PlayerPrefs.GetInt(items[i].playerPref, 0) == 2) activados++;
			items[i].actualizar();	
			//print("c"+i+" "+PlayerPrefs.GetInt(items[i].playerPref, 0));
		}	
		print (activados + " obj activados");
		PlayerPrefs.SetInt("objetoPeluqueriaActivado", activados);
	}
	
	void comprarItem(int id){
		if(comprar (int.Parse(items[id].precioLabel.text))){
			PlayerPrefs.SetInt(items[id].playerPref, 1);
			#if UNITY_IPHONE
			//FlurryAnalytics.logEvent("comprado_"+items[id].playerPref, false );
			#endif		
			#if UNITY_ANDROID
			FlurryAndroid.logEvent("comprado_"+items[id].playerPref);
			#endif
			activar(id, true);
			actualizarItems();
			sugerenciasCompra.mostrarPanelComprado(items[id].titulo.text, items[id].imagen);
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
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Store");
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
public class itemesPeluqueria{
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
	public string playerPrefValor;
	public UILabel titulo;
	public UISprite imagen;
	
	public void actualizar(){
		activarCheckboxG = activarCheckbox.gameObject;
		//int requisito = playerPrefRequisito == ""?1:PlayerPrefs.GetInt(playerPrefRequisito, 0);
		int estado = PlayerPrefs.GetInt(playerPref, 0);
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
		//activarCheckbox.eventReceiver = Camera.main.transform.gameObject;
		//activarCheckbox.functionName = "activar" + (id);
	}
}