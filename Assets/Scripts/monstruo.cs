using UnityEngine;
using System.Collections;
using SmoothMoves;

public class monstruo : MonoBehaviour {
	//dice que tipo de monstruo es
	public int tipo;
	//dice la dificultad del monstruo (lectura)
	public int dificultad;
	public Transform[] pelos;
	Transform[] peloMonstruo;
	Transform cabezaBone;
	pelo[] peloMonstruoScript;
	public int[] peloTipo;
	//public int finalPorcentaje;
	public int estacion;
	public int totalPelos;
	public Vector3 pelosPivote;
	
	public expresion expresionScript;
	// < 0 malo
	// > 0 bueno
	public int animo = 0;
	//public bool iniciado = false;
	public bool completo = false;
	GameObject centralObj;
	public bool pausado = false;
	
	public Transform reloj;
	
	TweenPosition tween;
	
	float tiempoCheckAnimo = 0.5f;
	public bool enPrimerPlano = false;
	public Vector3 posicionInicial;
	public float radioPelos = 70f;
	
	float tiempoFinEfecto;
	public string ultimoPoderEjecutado;
	
	bool composicionCompleta = false;
	public BoneAnimation cuerpo;
	
	void Start(){
		Color colorCuerpo = Color.white;
		switch (tipo) {
		case 0:
			switch(dificultad){
			case 0: colorCuerpo = new Color(227f/255f, 127f/255f, 179f/255f); break;
			case 1: colorCuerpo = new Color(54f/255f, 166f/255f, 203f/255f); break;
			case 2: colorCuerpo = new Color(255f/255f, 254f/255f, 129f/255f); break;
			case 3: colorCuerpo = new Color(245f/255f, 156f/255f, 16f/255f); break;
			case 4: colorCuerpo = new Color(102f/255f, 24f/255f, 24f/255f); break;
			}
			break;
		case 1:
			switch(dificultad){
			case 0: colorCuerpo = new Color(227f/255f, 127f/255f, 179f/255f); break;
			case 1: colorCuerpo = new Color(54f/255f, 166f/255f, 203f/255f); break;
			case 2: colorCuerpo = new Color(255f/255f, 254f/255f, 129f/255f); break;
			case 3: colorCuerpo = new Color(245f/255f, 156f/255f, 16f/255f); break;
			case 4: colorCuerpo = new Color(102f/255f, 24f/255f, 24f/255f); break;
			}
			break;
		case 2:
			switch(dificultad){
			case 0: colorCuerpo = new Color(227f/255f, 127f/255f, 179f/255f); break;
			case 1: colorCuerpo = new Color(54f/255f, 166f/255f, 203f/255f); break;
			case 2: colorCuerpo = new Color(255f/255f, 254f/255f, 129f/255f); break;
			case 3: colorCuerpo = new Color(245f/255f, 156f/255f, 16f/255f); break;
			case 4: colorCuerpo = new Color(102f/255f, 24f/255f, 24f/255f); break;
			}
			break;
		case 3:
			switch(dificultad){
			case 0: colorCuerpo = new Color(227f/255f, 127f/255f, 179f/255f); break;
			case 1: colorCuerpo = new Color(54f/255f, 166f/255f, 203f/255f); break;
			case 2: colorCuerpo = new Color(255f/255f, 254f/255f, 129f/255f); break;
			case 3: colorCuerpo = new Color(245f/255f, 156f/255f, 16f/255f); break;
			case 4: colorCuerpo = new Color(102f/255f, 24f/255f, 24f/255f); break;
			}
			break;
		}
		print (colorCuerpo);
		cuerpo.SetBoneColor ((tipo==0)?"cuerpo":"cuerpoEncima", colorCuerpo, 1f);
		centralObj = GameObject.FindWithTag("central");
		if (tween == null) {
				tween = gameObject.GetComponent<TweenPosition> ();
				tween.from = new Vector3 (transform.position.x, tween.from.y, tween.from.z);
				tween.to = new Vector3 (transform.position.x, tween.to.y, tween.to.z);
				tween.Play (true);
		}
	}
	
	public void iniciar(){
		//iniciado = true;
		//for(int i = 0; i < totalPelos; i++) peloMonstruo[i].gameObject.SendMessage("iniciar");
	}
	
	public void componer(){
		if(tween == null) tween = gameObject.GetComponent<TweenPosition>();
		tween.from = new Vector3(transform.position.x, tween.from.y, tween.from.z);
		tween.to = new Vector3(transform.position.x, tween.to.y, tween.to.z);
		tween.Play(true);
		cabezaBone = transform.Find("Animaciones/Root/cuerpo/cabeza");
		totalPelos = peloTipo.Length;
		peloMonstruo = new Transform[totalPelos];
		peloMonstruoScript = new pelo[totalPelos];
		StartCoroutine(crearPelos());

	}
	
	public IEnumerator crearPelos(){
		for(int i = 0; i < totalPelos; i++){
			float ang = Mathf.Deg2Rad * (180 * i / (totalPelos - 1));
			Transform pelosObj = pelos[peloTipo[i]];
			peloMonstruo[i] = (Transform) Instantiate (pelosObj, Vector3.zero, Quaternion.Euler(0, 0, 180 * i / (totalPelos - 1) - 90));
			peloMonstruo[i].parent = cabezaBone;//transform;
			peloMonstruo[i].localPosition = new Vector3(radioPelos * Mathf.Cos(ang), radioPelos * Mathf.Sin(ang) + 150, 40) + pelosPivote;
			peloMonstruoScript[i] = peloMonstruo[i].gameObject.GetComponent<pelo>();
			
			yield return new WaitForSeconds(0.2f);
		}
		composicionCompleta = true;
		for(int i = 0; i < totalPelos; i++) peloMonstruo[i].gameObject.SendMessage("iniciar");
	}
	
	public void pausar(bool p){
		for(int i = 0; i < peloMonstruoScript.Length; i++){
			if(peloMonstruoScript[i] != null)
				peloMonstruoScript[i].pausar(p);
		}
		pausado = p;
	}
	
	public void reset(){
		for(int i = 0; i < totalPelos; i++){
			peloMonstruoScript[i].reset();
		}
		completo = false;
		pausado = false;
		if(enPrimerPlano){
			transform.position = posicionInicial;
			posicionInicial = Vector3.zero;
			enPrimerPlano = false;
			tween.from = new Vector3(transform.position.x, -1000, 150);
			tween.to = new Vector3(transform.position.x, -250, 150);
			tween.Play(true);
		}
	}
	 
	public void cortarPelo(int pos, int poderCorte){
		//peloMonstruo[pos].gameObject.SendMessage("cortar", poderCorte);
		if(peloMonstruoScript[pos] != null) peloMonstruoScript[pos].cortar(poderCorte, true);
	}
	
	void checkPelos(){
		if(pausado || !composicionCompleta) return;
		int completos = 0;
		for(int i = 0; i < totalPelos; i++){
			if(peloMonstruoScript[i].peloCompleto) completos++;
		}
		//if((float)completos/(float)totalPelos >= (float)finalPorcentaje/100.0f) completo = true;
		if(completos >= totalPelos) completo = true; 
		else completo = false;
		
		if(completo){ 
			centralObj.SendMessage("monstruoCompleto");
			final ();
		}
	}
	//0: idle, 4:enojadoFinal
	void checkAnimo(){
		if(!composicionCompleta) return;
		int segmentos = 0;
		for(int i = 0; i < totalPelos; i++){
			segmentos += peloMonstruoScript[i].seccionActual;
		}
		//saca el porcentaje de segmentos completos
		animo = (int)(((float) segmentos / (float)(totalPelos * peloMonstruoScript[0].nSecciones)) * 100) / 25;
		animo = Mathf.Clamp(animo, 0, 5);
		expresion.expresiones exp = expresion.expresiones.idle;
		switch(animo){
			case 1: exp = expresion.expresiones.enojado1; break;
			case 2: exp = expresion.expresiones.enojado2; break;
			case 3: exp = expresion.expresiones.enojado3; break;
			case 4: exp = expresion.expresiones.enojadoFinal; break;
		}	
		expresionScript.setExpresionActual(exp);
	}
	
	void Update(){
		if(tiempoCheckAnimo < Time.time){
			tiempoCheckAnimo = Time.time + 0.5f;
			checkAnimo();
		}
	}
	
	public void esconder(bool forward){
		//tween.from = transform.position;
		//tween.to = transform.position + new Vector3(0f, -1000f, 0f);
		if(tween != null) tween.Play(!forward);
	}
	
	void final(){
		posicionInicial = transform.position;
		tween.from = transform.position + new Vector3(0f, -1000f, 0f);
		tween.to = transform.position;
		tween.Play(false);	
	}
	
	void finalEscondido(){
		if(enPrimerPlano || !completo){ 
			return;
		}
		enPrimerPlano = true;
		tween.from = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z + 450);
		tween.to = new Vector3(Camera.main.transform.position.x, -135, Camera.main.transform.position.z + 450);
		tween.Play(true);
	}
	
	public void poderCortarPeloCompleto(int nivel, int pos){
		
		//Playtomic.Log.LevelCounterMetric("poderPeloCompleto", "nivel"+PlayerPrefs.GetInt ("nivelActual", 1));
		
		//en nivel 5 corta un pelo completo
		peloMonstruoScript[pos].cortar( 30f + (70f * nivel) / 5f, false);
		if(nivel >= 5){
			//daño colateral 
			if(pos - 1 >= 0) 
				peloMonstruoScript[pos - 1].cortar((100f * nivel) / 20f, false);
			if(pos + 1 < peloMonstruoScript.Length) 
				peloMonstruoScript[pos + 1].cortar((100f * nivel) / 20f, false);
			
			if(nivel >= 8){
				if(pos - 2 >= 0) 
					peloMonstruoScript[pos - 2].cortar((100f * nivel) / 40f, false);
				if(pos + 2 < peloMonstruoScript.Length) 
					peloMonstruoScript[pos + 2].cortar((100f * nivel) / 40f, false);	
			}
		}
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
	}
	
	public IEnumerator poderCongelar(float tiempoEfecto){
		//tiempo para aplicar el congelar cuando aparece un nuevo monstruo
		yield return new WaitForSeconds(0.5f);
		//Playtomic.Log.LevelCounterMetric("poderCongelar", "nivel"+PlayerPrefs.GetInt ("nivelActual", 1));
		ultimoPoderEjecutado = "poderCongelar";
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
		//en nivel 5 congela un monstruo completo
		print(ultimoPoderEjecutado + tiempoEfecto);
		for(int i = 0; i < totalPelos; i++){
			if(peloMonstruoScript.Length > i && peloMonstruoScript[i] != null) peloMonstruoScript[i].velocidadCrecimiento(30);
		}
		tiempoFinEfecto = Time.time + tiempoEfecto;
		iniciarReloj (tiempoEfecto);
		yield return new WaitForSeconds(tiempoEfecto);
		tiempoFinEfecto = 0f;
		print ("terminado");
		for(int i = 0; i < totalPelos; i++){
			if(peloMonstruoScript.Length > i && peloMonstruoScript[i] != null) peloMonstruoScript[i].velocidadCrecimiento(100);
		}
	}
	
	public void poderCortarPuntas(int nivel, int pos){
		//Playtomic.Log.LevelCounterMetric("poderCortarPuntas", "nivel"+PlayerPrefs.GetInt ("nivelActual", 1));
	
		peloMonstruoScript[pos].cortar( (30f * nivel) / 5f, false);
		for(int i = 1; i <= (int)(nivel / 2f); i++){
			if(pos - i >= 0) peloMonstruoScript[pos - i].cortar( (30f * nivel) / 5f, false);
			if(pos + i < peloMonstruoScript.Length) peloMonstruoScript[pos + i].cortar( (30f * nivel) / 5f, false);
		}
		/*peloMonstruoScript[pos].cortar( 30f + (70f * nivel) / 5f, false);
		if(nivel >= 5){
			//daño colateral 
			if(pos - 1 >= 0) 
				peloMonstruoScript[pos - 1].cortar((100f * nivel) / 20f, false);
			if(pos + 1 < peloMonstruoScript.Length) 
				peloMonstruoScript[pos + 1].cortar((100f * nivel) / 20f, false);
			
			if(nivel >= 8){
				if(pos - 2 >= 0) 
					peloMonstruoScript[pos - 2].cortar((100f * nivel) / 40f, false);
				if(pos + 2 < peloMonstruoScript.Length) 
					peloMonstruoScript[pos + 2].cortar((100f * nivel) / 40f, false);	
			}
		}*/
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
	}
	
	public IEnumerator poderDetenerTiempo(float tiempoEfecto){
		//tiempo para aplicar el congelar cuando aparece un nuevo monstruo
		yield return new WaitForSeconds(0.5f);
		//Playtomic.Log.LevelCounterMetric("poderDetenerTiempo", "nivel"+PlayerPrefs.GetInt ("nivelActual", 1));
		ultimoPoderEjecutado = "poderDetenerTiempo";
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
		
		//en nivel 5 congela un monstruo completo
		print(ultimoPoderEjecutado + tiempoEfecto);
		for(int i = 0; i < totalPelos; i++){
			if(peloMonstruoScript.Length > i && peloMonstruoScript[i] != null) peloMonstruoScript[i].velocidadCrecimiento(0);
			if(peloMonstruoScript.Length > i && peloMonstruoScript[i] != null) peloMonstruoScript[i].detenerTotalmente(tiempoEfecto);
		}
		tiempoFinEfecto = Time.time + tiempoEfecto;
		iniciarReloj (tiempoEfecto);
		yield return new WaitForSeconds(tiempoEfecto);
		tiempoFinEfecto = 0f;
		for(int i = 0; i < totalPelos; i++){
			if(peloMonstruoScript.Length > i && peloMonstruoScript[i] != null) peloMonstruoScript[i].velocidadCrecimiento(100);
		}
		
	}
	
	public IEnumerator poderAutoCorte(float tiempoEfecto){
		ultimoPoderEjecutado = "poderAutoCorte";
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
		
		print(ultimoPoderEjecutado + tiempoEfecto);
		centralObj.SendMessage("peluqueroCorteAutomatico", true);
		tiempoFinEfecto = Time.time + tiempoEfecto;
		iniciarReloj (tiempoEfecto);
		yield return new WaitForSeconds(tiempoEfecto);
		tiempoFinEfecto = 0f;
		centralObj.SendMessage("peluqueroCorteAutomatico", false);
	}
	
	public IEnumerator poderFuerza(float tiempoEfecto){
		ultimoPoderEjecutado = "poderFuerza";
		expresionScript.setExpresionActual(expresion.expresiones.sorpresa);
		
		print(ultimoPoderEjecutado + tiempoEfecto);
		centralObj.SendMessage("peluqueroPoderFuerza", true);
		tiempoFinEfecto = Time.time + tiempoEfecto;
		iniciarReloj (tiempoEfecto);
		yield return new WaitForSeconds(tiempoEfecto);
		tiempoFinEfecto = 0f;
		centralObj.SendMessage("peluqueroPoderFuerza", false);
	}
	
	/*public IEnumerator ejecutarUltimoPoder(float tiempo){
		switch(ultimoPoderEjecutado){
			case "poderCongelar":
			print ("ejecutando");
			StartCoroutine(poderCongelar (tiempo));
			break;
		}
		return null;
	}*/
	
	void iniciarReloj(float tiempo){
		Transform t = (Transform) Instantiate (reloj, transform.position + new Vector3(0, 0, -50), Quaternion.identity);
		t.gameObject.SendMessage("setTiempoVida", tiempo);
		//StartCoroutine();	
	}
	
	public float obtenerTiempoRestanteEfecto(){
		return tiempoFinEfecto - Time.time;	
	}
}
