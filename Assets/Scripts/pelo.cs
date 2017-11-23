using UnityEngine;
using System.Collections;
using SmoothMoves;

public class pelo : MonoBehaviour {
	
	public BoneAnimation animacion;
	public int dificultad; //[0, 2]
	
	public int nSecciones;
	public int seccionActual; //[0, n]
	public GameObject[] segmentosCortados;
	public float tiempoCrecimiento;
	float tiempoCrecimientoAux;
	float tiempoActual;
	public float expPorSegmento;
	
	bool iniciado = false;
	public bool peloCompleto = false;
	bool pausado = false;
	
	GameObject centralObj;
	
	int idAnimacionActual;
	public AudioClip sonidoCorte;
	public float probabilidadOro = 0.5f;
	public Transform moneda;
	public int[] seccionesOro;

	public Transform brillo;
	
	void Awake () {
		int mejoraOro = PlayerPrefs.GetInt("mejoraOro", 0);
		switch(mejoraOro){
		case 0:
			probabilidadOro += 0f;
			break;
		case 1:
			probabilidadOro += 0.01f;
			break;
		case 2:
			probabilidadOro += 0.02f;
			break;
		case 3:
			probabilidadOro += 0.04f;
			break;
		case 4:
			probabilidadOro += 0.06f;
			break;
		case 5:
			probabilidadOro += 0.1f;
			break;
		}
		
		animacion = gameObject.GetComponent<BoneAnimation>();
		
		tiempoCrecimientoAux = tiempoCrecimiento;
		
		centralObj = GameObject.FindWithTag("central");
		
		switch(dificultad){
			case 1:
				animacion.SetMeshColor(new Color(0.24609f, 0.5039f, 0.70703f, 1f));
				animacion.updateColors = true;
			break;
			case 2:
				animacion.SetMeshColor(new Color(0.84375f, 0.26953f, 0.0625f, 1f));
				animacion.updateColors = true;
			break;
		}
		
		seccionesOro = new int[nSecciones];
		for(int i = 0; i < nSecciones; i++) cortar (0, false);
	}
	
	public void reset(){
		for(int i = 0; i < nSecciones; i++) cortar (0, false);
		if(pausado) pausar(false);
		iniciar ();
	}
	
	void iniciar(){
		tiempoActual = Time.time + tiempoCrecimiento * Random.Range(0.5f, 1f);
		iniciado = true;
		int nAnimaciones = GetComponent<Animation>().GetClipCount();//animacion.animationData.AnimationClipCount;
		idAnimacionActual = Random.Range(0, nAnimaciones) + 1;
		//animacion.Play(animacion.animationData.animationClips[idAnimacionActual].animationName);
		GetComponent<Animation>().Play("completo"+idAnimacionActual);
	}
	
	public void pausar(bool p){
		if(!p) tiempoActual = Time.time + tiempoCrecimiento;
		pausado = p;
	}
	
	void crecer () {
		if(pausado) return;
		if(seccionActual < nSecciones){
			seccionActual++;
			if(Random.Range(0f, 1f) < probabilidadOro && seccionActual < nSecciones - 1){
				//animacion.SetBoneColor("Bone " + seccionActual, new Color(1f, 0.9f, 0f, 1f), 1.0f);
				animacion.FlashBoneColor("Bone " + seccionActual, new Color(1f, 1.0f, 0f, 1f), 1.0f, new Color(animacion.mColors[0].r,animacion.mColors[0].g,animacion.mColors[0].b,1.0f), 1.0f, 0.5f, -1, false);
				Transform t = (Transform) Instantiate (brillo);
				print ("oro en " + seccionActual);
				t.parent = animacion.GetBoneTransform("Bone " + seccionActual);
				t.localPosition = Vector3.zero;
				if(seccionActual < seccionesOro.Length - 1)
					seccionesOro[seccionActual] = 1;
			}
			else
				animacion.SetBoneColor("Bone "+seccionActual, new Color(animacion.mColors[0].r,animacion.mColors[0].g,animacion.mColors[0].b,1.0f), 1.0f);
		}
		if(seccionActual >= nSecciones){ 
			peloCompleto = true;
			transform.root.gameObject.SendMessage("checkPelos");
		}
	}
	
	void consultaMoneda(){
		//for(int i = 0; i < seccionesOro.Length; i++){
			if(seccionActual + 1 < seccionesOro.Length && seccionesOro[seccionActual + 1] == 1){ 
				Instantiate(moneda, new Vector3(animacion.mBoneTransforms[seccionActual + 1].position.x, animacion.mBoneTransforms[seccionActual + 1].position.y, -30f), moneda.rotation);
				seccionesOro[seccionActual + 1] = 0;
				Transform t = animacion.GetBoneTransform("Bone " + (seccionActual + 1));
				if(t != null) Destroy(t.FindChild("brilloOro(Clone)").gameObject);
				#if UNITY_IPHONE
				//FlurryAnalytics.logEvent("monedaGanada", false );
				#endif		
				#if UNITY_ANDROID
				//FlurryAndroid.logEvent("monedaGanada");
				#endif
			}
		//}
	}
	
	public void cortar (float poderCorte, bool callback){
		int cantidadCortes = Mathf.FloorToInt((poderCorte / (100.0f / nSecciones)));
		bool cortado = false;
		if(cantidadCortes < 1) cantidadCortes = 1;
		print ("cantidad cortes " + cantidadCortes);
		for(int i = 0; i < cantidadCortes; i++){
			if(seccionActual > 0){
				animacion.StopFlashingBoneColor("Bone " + seccionActual);
				animacion.SetBoneColor("Bone " + seccionActual, new Color(animacion.mColors[0].r, animacion.mColors[0].g, animacion.mColors[0].b, 0f), 1.0f);
				seccionActual--;
				
				if(callback){ 
					GameObject s = (GameObject) Instantiate (segmentosCortados[seccionActual], animacion.GetBoneTransform("Bone " + (seccionActual + 1)).position + animacion.GetBoneTransform("Bone " + (seccionActual + 1)).up * 10, Quaternion.Euler(animacion.GetBoneTransform("Bone " + (seccionActual + 1)).eulerAngles));
					s.transform.localScale = animacion.GetBoneTransform("Bone " + (seccionActual + 1)).lossyScale;
					SmoothMoves.Sprite ss = s.GetComponent<SmoothMoves.Sprite>();
					if(dificultad == 1){
						ss.color = new Color(0.24609f, 0.5039f, 0.70703f, 1f);
						ss.UpdateArrays();
					}
					if(dificultad == 2){
						ss.color = new Color(0.84375f, 0.26953f, 0.0625f, 1f);
						ss.UpdateArrays();
					}
					if(GetComponent<AudioSource>()!=null && sonidoCorte != null && PlayerPrefs.GetInt("sonido", 1) == 1) GetComponent<AudioSource>().PlayOneShot(sonidoCorte);

				}
				consultaMoneda();
				tiempoActual = Time.time + tiempoCrecimiento;
				cortado = true;
			}
		}
		if(callback && cortado) centralObj.SendMessage("peloCortado", new Vector3(cantidadCortes, expPorSegmento, peloCompleto?1:0));
		peloCompleto = false;
		transform.root.gameObject.SendMessage("checkPelos");
	}
	
	public void velocidadCrecimiento(int porcentage){
        if (GetComponent<Animation>() != null && this != null && transform != null && gameObject != null)
        {
            //animacion[animacion.animationData.animationClips[idAnimacionActual].animationName].speed = (porcentage / 100f) * 0.75f;
            GetComponent<Animation>()["completo" + idAnimacionActual].speed = (porcentage / 10f);
            //print (animation["completo" + idAnimacionActual].speed);
            //tiempoActual += (tiempoCrecimiento * (1f - porcentage / 100f));
            if (porcentage != 100)
            {
                tiempoCrecimiento *= (1f + porcentage / 100f) * 1.25f;
                tiempoActual = Time.time + tiempoCrecimiento;
            }
            else tiempoCrecimiento = tiempoCrecimientoAux;
            //print (tiempoActual);
        }
	}
	
	public void detenerTotalmente(float tiempo){
		tiempoActual += tiempo;
	}
	
	void Update () {
		if(!iniciado) return;
		if(pausado) return;
		if(Time.time > tiempoActual){
			crecer ();
			tiempoActual = Time.time + tiempoCrecimiento;
		}
	}
}
