using UnityEngine;
using System.Collections;

public class tilt : MonoBehaviour {
	Vector3 acc = Vector3.zero;
	public float velocidad = 10f;
	public float aceleracion = 70f;
	int totalPelosActual = 7;
	int[] posicionesPelos;
	float anguloActual;
	public int pos;
	public bool useFullAngles = false;
	public bool snapToPositions = false;
	GameObject centralObj;
    central centralScript;
	bool activado = true;
	float angMenorAlcanzado = 0f;
	float angMayorAlcanzado = 0f;

    void Awake()
    {
        if (useFullAngles)
        {
            centralObj = GameObject.FindGameObjectWithTag("central");
            centralScript = centralObj.GetComponent<central>();
        }
    
	}
	
	void activar(bool a){
		activado = a;	
	}
	
	void setTotalPelos(int t){
		totalPelosActual = t;
		posicionesPelos = new int[totalPelosActual];
		for(int i = 0; i < totalPelosActual; i++){
			posicionesPelos[i] = 180 * i / (totalPelosActual - 1) - 90;	
			
		}
		pos = Mathf.FloorToInt(totalPelosActual / 2);
	}

	public float rangoAngulos(){
		return angMayorAlcanzado - angMenorAlcanzado;
	}
	
	void setPos(int ang){
		pos = 0;
		ang = (ang * 90) / (int)(70 * 0.4);
		if (ang < angMenorAlcanzado)
			angMenorAlcanzado = ang;
		if (ang > angMayorAlcanzado)
			angMayorAlcanzado = ang;
		float dif = Mathf.Abs(posicionesPelos[0] - posicionesPelos[1]);
		for(int i = 0; i < totalPelosActual; i++){
			if(ang >= posicionesPelos[i] - dif / 2 && ang < posicionesPelos[i] + dif / 2){
				pos = i;
				return;
			}
		}
	}
	
	void cortarPeloTilt(){
        if (centralObj == null || centralScript == null)
        {
            centralObj = GameObject.FindGameObjectWithTag("central");
            centralScript = centralObj.GetComponent<central>();
        }
        centralScript.cortarPelo(pos);
        //centralObj.SendMessage("cortarPelo", pos);	
	}
	
	// Update is called once per frame
	void Update () {
		if(posicionesPelos == null && Time.timeScale == 0f) return;
		if(!activado) acc = Vector3.zero;
		else{
#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
		acc.x += 0.04f*Input.GetAxis("Horizontal");
#else
		acc = Vector3.Lerp(acc, Input.acceleration, velocidad*Time.deltaTime);
#endif
		}
		acc.y = Mathf.Clamp(acc.y, -0.4f, 0.4f);
		acc.x = Mathf.Clamp(acc.x, -0.4f, 0.4f);
        Vector3 tmp = transform.eulerAngles;
#if UNITY_ANDROID
		tmp.z = - acc.x * aceleracion;
#else
		tmp.z = - acc.x * aceleracion;
#endif  
		setPos(Mathf.CeilToInt(tmp.z));
		if (snapToPositions){
			anguloActual = Mathf.Lerp(anguloActual, posicionesPelos[pos], 10 * Time.deltaTime);
			if(useFullAngles)
				tmp.z = Mathf.RoundToInt(anguloActual);
			else
				tmp.z = (anguloActual * 35) / 90;
		}
		transform.eulerAngles = tmp;
		
	}
	
	void OnGUI(){
		/*if(GUI.Button(new Rect(0,0,100,100), "veloc. -")) velocidad-=50f;
		if(GUI.Button(new Rect(100,0,100,100), "veloc. +")) velocidad+=50;
		if(GUI.Button(new Rect(0,100,100,100), "acel. -")) aceleracion-=10;
		if(GUI.Button(new Rect(100,100,100,100), "acel. +")) aceleracion+=10;
		GUI.Label(new Rect(200,100,300,100), "velocidad: "+velocidad+", aceleracion: "+aceleracion ); */
		//GUI.Box(new Rect(200,200,300,100), "acc: "+acc+" pos: "+pos );
	}
}
