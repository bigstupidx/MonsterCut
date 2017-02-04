#pragma strict
var intervalo:float = 0.5f;
private var delay:float = 0f;
private var tiempoActual:float;
private var activado:boolean = false; 
var materialEncendido:Material;
var materialApagado:Material;

function Start () {
	
}

function setTiempo(t:float){
	gameObject.GetComponent.<Renderer>().sharedMaterial = materialEncendido;
	delay = t;
	yield WaitForSeconds(delay);
	activado = true;
	tiempoActual = Time.time;
	//gameObject.renderer.material = renderer.materials[0];
}

function Update () {
	if(activado){
		if(tiempoActual + intervalo < Time.time){
			if(gameObject.GetComponent.<Renderer>().sharedMaterial == materialApagado){
				gameObject.GetComponent.<Renderer>().sharedMaterial = materialEncendido;
			}
			else gameObject.GetComponent.<Renderer>().sharedMaterial = materialApagado;
			//if(gameObject.activeSelf) gameObject.SetActive(false);
			//else gameObject.SetActive(true);
			tiempoActual = Time.time;
		} 
	}
}