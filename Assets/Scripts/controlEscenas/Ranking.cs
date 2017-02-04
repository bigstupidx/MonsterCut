using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {
	public UISprite[] imagenes;
	public string[] peluqueros;
	public UILabel textoPodio;
	public UILabel textoResto;
	public UILabel scorePodio;
	public UILabel scoreResto;
	
	public GameObject recibiendoTexto;
	
	// Use this for initialization
	void Start () {
		//StartCoroutine(listScore("alltime", -1));
	}
	
	void Titulo(){
		Application.LoadLevel("Titulo");	
	}
	
	void mostrarvalores(bool b){
		recibiendoTexto.SetActive (!b);
		textoPodio.gameObject.SetActive(b);
		textoResto.gameObject.SetActive(b);
		scorePodio.gameObject.SetActive(b);
		scoreResto.gameObject.SetActive(b);
		for(int i = 0; i < imagenes.Length; i++){
			imagenes[i].gameObject.SetActive(b);
		}
	}
	
	void OnSelectionChange(string val){
	/*	mostrarvalores(false);
		switch(val){
		case "Hall of Fame":
			StartCoroutine(listScore("alltime", -1));
			break;
		case "Monthly":
			StartCoroutine(listScore("monthly", -1));
			break;
		case "Weekly":
			StartCoroutine(listScore("weekly", -1));
			break;
		case "Today":
			StartCoroutine(listScore("today", -1));
			break;
		}*/
	}
	
/*	IEnumerator listScore(string tiempo, int peluquero)
	{
		yield return StartCoroutine(Playtomic.Leaderboards.List("highscores", true, tiempo, 1, 10));
		var response = Playtomic.Leaderboards.GetResponse("List");
		
		if(response.Success)
		{
			Debug.Log("Scores listed successfully: " + response.NumItems + " in total, " + response.Scores.Count + " returned");
			textoPodio.text = "";
			textoResto.text = "";
			scorePodio.text = "";
			scoreResto.text = "";
			for(int i = 0; i < response.Scores.Count; i++){
				string colores = "";
				string coloresFin = "";
				Debug.Log(response.Scores[i].Name + ": " + response.Scores[i].Points + ", name:" + response.Scores[i].CustomData["nombre"]);
				if(response.Scores[i].Name == SystemInfo.deviceUniqueIdentifier){
					if(PlayerPrefs.GetInt("puntajeTotal") == (int)(response.Scores[i].Points)) colores = "[f94300]";
					else colores = "[94bb6b]";
					coloresFin = "[-]";
				}
				if(i < 3){
					textoPodio.text += colores + (i+1) + ". " + response.Scores[i].CustomData["nombre"] + coloresFin + "\n";	
					scorePodio.text += colores + response.Scores[i].Points + coloresFin + "\n";
				}
				else{
					textoResto.text += colores + (i+1) + ". " + response.Scores[i].CustomData["nombre"] + coloresFin + "\n";
					scoreResto.text += colores + response.Scores[i].Points + coloresFin + "\n";
				}
				imagenes[i].gameObject.SetActive(true);
				imagenes[i].spriteName = peluqueros[int.Parse(response.Scores[i].CustomData["peluquero"])];
			}
			mostrarvalores(true);
			for(int i = response.Scores.Count; i < imagenes.Length; i++){
				imagenes[i].gameObject.SetActive(false);	
			}
		}
		else
		{
			Debug.Log("Score list failed to load because of " + response.ErrorCode + ": " + response.ErrorDescription);
		}
		
	}*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
