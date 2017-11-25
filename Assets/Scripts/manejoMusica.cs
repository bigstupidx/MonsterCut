using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class manejoMusica : MonoBehaviour {
	public AudioClip musica;
	public string[] nivelEliminar;
	// Use this for initialization
	void Start () {
        Object[] m = GameObject.FindObjectsOfType(typeof(manejoMusica));
		if(m.Length > 1) Destroy(gameObject);
		GetComponent<AudioSource>().clip = musica;
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().Play();
		Time.timeScale = 1f;
		print (Time.timeScale);
		DontDestroyOnLoad(gameObject);
	}


    void Awake(){
	}

	void OnLevelWasLoaded(int level){
		foreach(string l in nivelEliminar){
			if(Application.loadedLevelName == l) Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		//if(!GetComponent<AudioSource>().isPlaying)GetComponent<AudioSource>().Play();
	}
}
