// Original script by wbokunic - http://forum.unity3d.com/threads/52001-Movie-Player-for-Indie-Users

// Modified C# version by Ronan Tumelty

 

using UnityEngine;

using System.Collections;

using System.IO;

 

[RequireComponent(typeof(AudioSource))]

public class VideoPlayer : MonoBehaviour {

	public Object[] movie_stills;
	public int number_of_stills = 0;
    public bool loop = false;
    public bool playOnStart = false;
    public int fps = 30;
    public AudioClip sound;
    public string resourceSubfolder = "";
	public int stills = 0;
	public bool play = false;
	public bool loaded = false;
	
	public string nextScene="";
	
	public WWW wwwData;
	
    void Update() {
        if (!resourceSubfolder.Equals("") && !loaded) {
            StartCoroutine(ImportVideo());
        }
        if(fps > 0){
            if(play == true){
                StartCoroutine(Player());
				
            }
        } else {
              Debug.LogError("'fps' must be set to a value greater than 0.");
        }  
    }

    IEnumerator Player(){
        play = false;
        if(loop){
            Debug.Log("looped. stills: " + stills + ", length: " + movie_stills.Length);
            if(stills >= movie_stills.Length) {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = sound;
                if(PlayerPrefs.GetInt("sonido", 1) == 1)
					GetComponent<AudioSource>().Play();
                stills = 0;
            }
        } else {
            if(stills > movie_stills.Length) {
                GetComponent<AudioSource>().Stop();
                stills -= 1;
            }
        }
        if (stills >= 0 && stills < movie_stills.Length) {
            Texture2D MainTex = movie_stills[stills] as Texture2D;
            GetComponent<Renderer>().material.SetTexture("_MainTex", MainTex);
            stills += 1;
            int fps_fixer = fps*3;
            float wait_time = 1.0f/fps_fixer;
            yield return new WaitForSeconds(wait_time);
            if(!GetComponent<AudioSource>().clip){
                if(sound && PlayerPrefs.GetInt("sonido", 1) == 1){
                    GetComponent<AudioSource>().clip = sound;
                    GetComponent<AudioSource>().Play();
                }
            }
            play = true;
			print (stills);
        }
		else{
			Application.LoadLevel(nextScene);
		}
    }

    public void Play() { play = true; }

    public void Pause() { play = false; }

    void Start ()
    {
		GetComponent<AudioSource>().loop = false;
        number_of_stills -= 1;
		Time.timeScale = 1f;
    }

    IEnumerator ImportVideo() {
        movie_stills = Resources.LoadAll(resourceSubfolder, typeof(Texture2D));
        loaded = true;
        if (playOnStart)
            play = true;    
        yield return null;
    }

    public void UnloadFromMemory() {
        play = false;
        GetComponent<AudioSource>().Stop();
        foreach (Object o in movie_stills) Destroy(o);
    }
}
