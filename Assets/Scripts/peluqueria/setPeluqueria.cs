using UnityEngine;
using System.Collections;

public class setPeluqueria : MonoBehaviour {
	public objetoPeluqueria[] objetos;
	public objetoBasePeluqueria[] objetosBase;
	//public baseDatos miBaseDatos;
	string[] valoresPrefsAmigo;
	
	public GameObject tutorial;
	public GameObject[] objetosEsconder;
	public GameObject[] objetosMostrar;
	public UILabel nombreDueno;

	public GameObject loading;

	// Use this for initialization
	void Start () {
        //miBaseDatos = gameObject.GetComponent<baseDatos>();
        crearPeluqueriaPropia(true);
	}

	void crearPeluqueriaPropia(bool actualizarBD){
		/*if(PlayerPrefs.GetString("idFacebookAmigo", "-1") != "-1"){ 
			if(tutorial != null) Destroy(tutorial);
			foreach(GameObject g in objetosEsconder) g.SetActive(false);

			if(nombreDueno != null) nombreDueno.text = PlayerPrefs.GetString("nombreAmigo", "Friend") + "'s\nBarberShop";
			StartCoroutine(crearPeluqueriaAmigo());
			return;
		}
		else */foreach(GameObject g in objetosMostrar) g.SetActive(false);
		for(int i = 0; i < objetosBase.Length; i++){
			bool crear = true;
			foreach(string pp in objetosBase[i].playerPrefs){
				//print("probando "+pp);
				if(PlayerPrefs.GetInt(pp, 0) == 2) crear = false;
			}
			if(crear){
				objetosBase[i].crear(transform);
				//if(objetosBase[i].playerPrefs.Length > 0 && objetosBase[i].nombreBaseDatos != "" && actualizarBD) 
				//	StartCoroutine(registrarPlayerPrefBase(), objetosBase[i]);
			}
			else objetosBase[i].limpiar();
		}
		for(int i = 0; i < objetos.Length; i++){
			if(PlayerPrefs.GetInt(objetos[i].playerPref, 0) == 2){
				objetos[i].crear(transform);	
				//if(objetos[i].playerPref != "" && objetos[i].nombreBaseDatos != "" && actualizarBD) 
				//	StartCoroutine(registrarPlayerPref(), objetos[i]);
			}
			else objetos[i].limpiar();
		}
        
		if (loading != null) {
			loading.SetActive (false);
		}
		
				//miBaseDatos.registrarPlayerPref(obtenerCampoPlayerPref(id), items[id].playerPref);
	}
	
	/*IEnumerator registrarPlayerPrefBase(objetoBasePeluqueria obj){
		if(miBaseDatos != null)
			yield return miBaseDatos.StartCoroutine("activarBase", obj);
	}
	
	IEnumerator registrarPlayerPref(objetoPeluqueria obj){
		if(miBaseDatos != null)
			yield return miBaseDatos.StartCoroutine("activar", obj);
	}
	
	IEnumerator crearPeluqueriaAmigo(){
		valoresPrefsAmigo = new string[objetosBase.Length];
		//miBaseDatos.playerPrefVacio(objetosBase[i].nombreBaseDatos);
		for(int i = 0; i < valoresPrefsAmigo.Length; i++){
			if(objetosBase[i].nombreBaseDatos != "" && miBaseDatos != null){
				yield return miBaseDatos.StartCoroutine("obtenerParametroAmigo", objetosBase[i].nombreBaseDatos);
				valoresPrefsAmigo[i] = miBaseDatos.prefAux;
				print (objetosBase[i].nombreBaseDatos + " " + i + " " + valoresPrefsAmigo[i]);
			}
		}
		//creacion de objetos base
		for(int i = 0; i < objetosBase.Length; i++){
			bool crear = true;
			//comprueba que no haya ningun playerpref de objetos comprados
			foreach(string pp in objetosBase[i].playerPrefs){
				foreach(string ppp in valoresPrefsAmigo){
					if(pp == ppp) crear = false;
				}
			}
			//si no hay, crea objeto base
			if(crear){
				objetosBase[i].crear(transform);
			}
			else objetosBase[i].limpiar();
		}
		//creacion de objetos comprados
		for(int i = 0; i < objetos.Length; i++){
			bool encontrado = false;
			foreach(string ppp in valoresPrefsAmigo){
				if(objetos[i].playerPref == ppp) encontrado = true;
			}
			if(encontrado){
				objetos[i].crear(transform);
				print ("objeto creado " + objetos[i].playerPref);
			}
			else objetos[i].limpiar();
		}

		if (loading != null) {
			loading.SetActive (false);
		}
	}
	//void obtenerParametroAmigo(string nombre){
	//	if(miBaseDatos != null) StartCoroutine(miBaseDatos.obtenerParametroAmigo", nombre);
	//}
	*/
	/*string obtenerCampoPlayerPref(int id){
		string ret = "";
		switch(id){
		case 0: ret = "puertaPref"; break;
		case 1: ret = "puertaPref"; break;
		case 2: ret = "puertaPref"; break;
		case 3: ret = "ventanaPref"; break;
		case 4: ret = "ventanaPref"; break;
		case 5: ret = "ventanaPref"; break;
		case 6: ret = "pisoPref"; break;	
		case 7: ret = "pisoPref"; break;	
		case 8: ret = "pisoPref"; break;	
		case 9: ret = "pisoPref"; break;	
		case 10: ret = "pisoPref"; break;	
		case 11: ret = "pisoPref"; break;	
		case 12: ret = "bancaPref"; break;	
		case 13: ret = "bancaPref"; break;	
		case 14: ret = "bancaPref"; break;	
		case 15: ret = "mesaPref"; break;	
		case 16: ret = "mesaPref"; break;	
		case 17: ret = "mesaPref"; break;	
		case 18: ret = "lucesPref"; break;	
		case 19: ret = "lucesPref"; break;	
		case 20: ret = "lucesPref"; break;	
		case 21: ret = "paredesPref"; break;	
		case 22: ret = "paredesPref"; break;
		case 23: ret = "paredesPref"; break;
		case 24: ret = "paredesPref"; break;
		case 25: ret = "paredesPref"; break;
		case 26: ret = "paredesPref"; break;
		}
		return ret;
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class objetoPeluqueria{
	public string nombreBaseDatos;
	public string playerPref;
	//public int valorActivo;
	public GameObject[] objetos;
	GameObject[] objetosCreados;
	
	public Material objetoMaterial;
	public string tagObjetoMaterial;
	
	public void limpiar(){
		if(objetosCreados != null && objetosCreados.Length > 0) 
				for(int i = 0; i < objetosCreados.Length; i++) GameObject.Destroy(objetosCreados[i]);
	}
	
	public void crear(Transform padre){
		limpiar();
		objetosCreados = new GameObject[objetos.Length];
		for(int i = 0; i < objetos.Length; i++){
			objetosCreados[i] = (GameObject) GameObject.Instantiate(objetos[i]);
			objetosCreados[i].transform.parent = padre;
		}
		if(objetoMaterial != null && tagObjetoMaterial != ""){
			GameObject[] obj = GameObject.FindGameObjectsWithTag(tagObjetoMaterial);
			foreach(GameObject g in obj){
				g.GetComponent<Renderer>().material = objetoMaterial;
			}
		}
	}
}

[System.Serializable]
public class objetoBasePeluqueria{
	public string nombreBaseDatos;
	public string[] playerPrefs;
	//public int valorActivo;
	public GameObject[] objetos;
	GameObject[] objetosCreados;
	
	public Material objetoMaterial;
	public string tagObjetoMaterial;
	
	public void limpiar(){
		if(objetosCreados != null && objetosCreados.Length > 0) 
				for(int i = 0; i < objetosCreados.Length; i++) GameObject.Destroy(objetosCreados[i]);
	}
	
	public void crear(Transform padre){
		limpiar();
		objetosCreados = new GameObject[objetos.Length];
		for(int i = 0; i < objetos.Length; i++){
			objetosCreados[i] = (GameObject) GameObject.Instantiate(objetos[i]);
			objetosCreados[i].transform.parent = padre;
		}
		if(objetoMaterial != null && tagObjetoMaterial != ""){
			GameObject[] obj = GameObject.FindGameObjectsWithTag(tagObjetoMaterial);
			foreach(GameObject g in obj){
				g.GetComponent<Renderer>().material = objetoMaterial;
			}
		}
	}
}