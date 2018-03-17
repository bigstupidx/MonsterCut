using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
