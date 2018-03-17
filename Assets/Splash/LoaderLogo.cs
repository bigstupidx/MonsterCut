using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoaderLogo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        StartCoroutine(LogoWait());
    }
	
	private IEnumerator LogoWait() {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
        //Application.LoadLevel (1);
    } 
}
