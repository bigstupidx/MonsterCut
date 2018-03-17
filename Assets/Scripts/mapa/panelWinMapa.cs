using UnityEngine;
using System.Collections;

public class panelWinMapa : MonoBehaviour {
	public UILabel monedasTexto;
	public GameObject[] monedasObjetos;
	public UILabel expTexto;
	public GameObject[] expObjetos;
	public UILabel unlockTexto;
	public GameObject[] unlockObjetos;
	public GameObject explosionPrefab;
	public UISprite unlockSprite;
	
	bool activo = false;
	int ciclo = 0;
	int expAux = 0;
	int monedasAux = 0;
	int expFinal = 0;
	int monedasFinal = 0;
	// Use this for initialization
	void Start () {
	}
	
	public void setInformacion(int monedas, int exp, string unlock){
		if(monedas > 0){
			monedasFinal = monedas;
		}
		else{
			for(int i = 0; i < monedasObjetos.Length; i++) monedasObjetos[i].SetActive(false);
		}
		if(exp > 0){
			expFinal = exp;
		}
		else{
			for(int i = 0; i < expObjetos.Length; i++) expObjetos[i].SetActive(false);
		}
		if(unlock != ""){
			unlockTexto.text = "" + unlock;
		}
		else{
			for(int i = 0; i < unlockObjetos.Length; i++) unlockObjetos[i].SetActive(false);
		}
		activo = true;
		StartCoroutine (fuegosArtificiales ());
		unlockSprite.spriteName = "";
		if(unlock.Contains("Werewolf")) unlockSprite.spriteName = "monstruoLobo";
		if(unlock.Contains("Mummy")) unlockSprite.spriteName = "monstruoMomia";
		if(unlock.Contains("Vampire")) unlockSprite.spriteName = "monstruoVampiro";
		gameObject.GetComponent<UIPlaySound> ().Play ();
	}

	IEnumerator fuegosArtificiales(){
		yield return new WaitForSeconds (0.4f);
		GameObject g = (GameObject)Instantiate (explosionPrefab);
		g.transform.Find ("Particle System").localPosition = new Vector3 (300f, -300f, 1495f);

		g.transform.parent = transform.parent;
		g.transform.localScale = Vector3.one;
		yield return new WaitForSeconds (0.4f);
		g = (GameObject)Instantiate (explosionPrefab);
		g.transform.Find ("Particle System").localPosition = new Vector3 (-300f, -300f, 1495f);

		g.transform.parent = transform.parent;
		g.transform.localScale = Vector3.one;
		yield return new WaitForSeconds (0.4f);
		g = (GameObject)Instantiate (explosionPrefab);
		g.transform.Find ("Particle System").localPosition = new Vector3 (0f, 300f, 1495f);

		g.transform.parent = transform.parent;
		g.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
		if(activo){
			ciclo++;
			if(ciclo%2 == 0){
				monedasTexto.text = "" + monedasAux;
				monedasAux += Mathf.CeilToInt(monedasFinal / 100f);
				if(monedasAux > monedasFinal) monedasAux = monedasFinal;
				
				expTexto.text = "" + expAux;
				expAux += Mathf.CeilToInt(expFinal / 100f);
				if(expAux > expFinal) expAux = expFinal;
			}
		}
	}
}
