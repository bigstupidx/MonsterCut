#pragma strict
var tiempo:float = 0;

function setTiempo(t:float){
//	print(t);
	yield WaitForSeconds(t);
	Destroy(gameObject);
}

function Start () {
	if(tiempo > 0) setTiempo(tiempo); 
}

function Update () {

}