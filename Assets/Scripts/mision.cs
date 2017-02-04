using UnityEngine;
using System.Collections;

public class mision : MonoBehaviour {
	public UILabel[] objetivosLabel;
	public UILabel[] objetivosPremio;
	public TweenColor[] objetivosFondo;
	public TweenColor[] objetivosLabelTween;
	public UISprite[] objetivosImagen;
	public Transform monedaMov;
	// Use this for initialization
	void Start () {
		/*for(int i = 0; i < 3; i++){
			PlayerPrefs.DeleteKey("misionSlot"+i);
		}
		for(int i = 0; i < 4; i++){
			PlayerPrefs.SetInt("mision"+i, 0);
			print("mision"+i+" "+PlayerPrefs.GetInt("mision"+i));
		}*/
		definirMisiones();
		
	}
	
	void definirMisiones(){
		globalVariables.establecerMisiones();
		for(int i = 0; i < objetivosLabel.Length; i++){
			objetivosImagen[i].spriteName = "" + globalVariables.obtenerMisionImagen(PlayerPrefs.GetInt("misionSlot"+i));
			objetivosLabel[i].text = "" + globalVariables.obtenerMision(PlayerPrefs.GetInt("misionSlot"+i));
			objetivosPremio[i].text = "" + globalVariables.misionesRecompensa[PlayerPrefs.GetInt("misionSlot"+i)];
		}
	}
	
	public int revisarMisiones(int[] valores){
		bool[] estadoMisiones = {false, false, false};
		int monedasRecompensa = 0;
		estadoMisiones = globalVariables.revisarMisiones(valores);
		for(int i = 0; i < estadoMisiones.Length; i++){
			if(estadoMisiones[i]){
				StartCoroutine("misionTerminada", i);
				monedasRecompensa += globalVariables.misionesRecompensa[PlayerPrefs.GetInt("misionSlot"+i)];
			}
		}
		
		return monedasRecompensa;
	}
	
	IEnumerator misionTerminada(int slot){
		#if UNITY_IPHONE
		//FlurryAnalytics.logEvent("misionTerminada" + PlayerPrefs.GetInt("misionSlot"+slot), false );
		#endif		
		#if UNITY_ANDROID
		FlurryAndroid.logEvent("misionTerminada" + PlayerPrefs.GetInt("misionSlot"+slot));
		#endif
		yield return new WaitForSeconds(2f);
		//int idMision = PlayerPrefs.GetInt("misionSlot"+slot);
		//objetivosLabel[slot].text = "Terminada!";
		objetivosFondo[slot].Play(true);
		objetivosLabelTween[slot].Play(true);
		print ("terminando mision "+slot);
		print(objetivosFondo[0].transform.localPosition+" "+objetivosFondo[1].transform.localPosition+" "+objetivosFondo[2].transform.localPosition);
		for(int i = 0; i < 10; i++){
			Transform g = (Transform)Instantiate(monedaMov, new Vector3(-2000,0,0), Quaternion.identity);
			g.parent = transform;
			g.localScale = new Vector3(1, 1, 1);
			TweenPosition t = g.gameObject.GetComponent<TweenPosition>();
			t.from = objetivosFondo[slot].transform.localPosition;
			t.to =  new Vector3(Screen.width / 2 - 20,Screen.height / 2 - 20,0);
			t.delay= i * 0.1f;
			t.Play (true);
		}
		yield return new WaitForSeconds(2f);
		
		PlayerPrefs.DeleteKey("misionSlot"+slot);
		objetivosFondo[slot].enabled = false;
		objetivosLabelTween[slot].Play(false);
		definirMisiones();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
