using UnityEngine;
using System.Collections;

public class resumen : MonoBehaviour {
	public UILabel tituloLabel;
	public UILabel informacionLabel;
	public UILabel expCollectedLabel;
	public UISlider barraProgresion;
	barraTrofeos barraTrofeosActual;
	public UISprite logroActualImagen;
	public UILabel logroActualTexto;
	Vector2 limites;
	TweenPosition tween;
	bool activarBarra =  false;
	string mensaje = "";
	int pTotalNivel;
	// Use this for initialization
	void Start () {
		GameObject barraEstrella = GameObject.FindWithTag("barraEstrella");
		if(barraEstrella != null){
			barraTrofeosActual = barraEstrella.GetComponent<barraTrofeos>();
			limites = barraTrofeosActual.obtenerLimitesBarra();
			barraProgresion.sliderValue = ((PlayerPrefs.GetInt("puntajeTotalJuego") - limites.x) / (limites.y - limites.x));
			logroActualImagen.spriteName = barraTrofeosActual.logroImagen.spriteName;
			logroActualImagen.color = barraTrofeosActual.logroImagen.color;
			logroActualTexto.text = barraTrofeosActual.logroMensaje.text;
		}
		tween = gameObject.GetComponent<TweenPosition>();
		tween.Play(false);
	}
	
	public void setInformacion(string titulo, int levelPoints, int bonusPerfect, int bonusNoPowers, int bonusAllLives, int bonusHappyness, int expCollected){
		tituloLabel.text = titulo;
		mensaje += levelPoints + "\n";
		mensaje += bonusPerfect + "\n";
		mensaje += bonusNoPowers + "\n";
		mensaje += bonusAllLives + "\n";
		mensaje += bonusHappyness + "\n";
		mensaje += "[f94300]" + (levelPoints + bonusPerfect + bonusNoPowers + bonusAllLives + bonusHappyness) + "[-]";
		//pTotalNivel = levelPoints + bonusPerfect + bonusNoPowers + bonusAllLives + bonusHappyness;
		informacionLabel.text = mensaje;
		
		expCollectedLabel.text = "" + expCollected;
		
	}
	
	public void activar(bool activo){
		tween.Play (activo);	
	}
	
	void OnGUI(){
		//if(GUI.Button (new Rect(0,0,100,100), "")) activarBarra=true;	
	}
	
	void panelPosicionado(){
		if(mensaje != "")
			activarBarra = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(activarBarra){
			//print (PlayerPrefs.GetInt("puntajeTotalJuego") +"-"+ limites.x +"/"+ limites.y +"-"+ limites.x);
			float pTotal = (PlayerPrefs.GetInt("puntajeTotalJuego") - limites.x) / (limites.y - limites.x); 
			barraProgresion.sliderValue = Mathf.Lerp(barraProgresion.sliderValue, Mathf.Clamp( pTotal, 0f, 1f), 2 * Time.deltaTime);
			if(barraProgresion.sliderValue >= 1f && logroActualImagen.spriteName != barraTrofeosActual.logroImagen.spriteName){
				logroActualImagen.spriteName = barraTrofeosActual.logroImagen.spriteName;
				logroActualImagen.color = barraTrofeosActual.logroImagen.color;
				logroActualTexto.text = barraTrofeosActual.logroMensaje.text;
			}
		}
	}
}
