using UnityEngine;
using System.Collections;

public class mensaje : MonoBehaviour {
	public UILabel label;
	public TweenColor panelPosterior;
	public TweenScale labelScale;
	//int bigMult = 3;
	Vector3 escalaOriginal;
	// Use this for initialization
	void Start () {
		panelPosterior.Play(false);
		labelScale.Play(false);
		escalaOriginal = transform.localScale;
	}
	
	void setMensaje(string m){
		panelPosterior.Play(true);
		labelScale.Play(true);
		label.text = m;
		
		transform.localScale = escalaOriginal;
		label.alpha = 1.0f;
	}
	
	void puntoMedioAnimacion(){
		if(labelScale.direction != AnimationOrTween.Direction.Reverse){
			panelPosterior.Play(false);
			labelScale.Play(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.localScale = Vector3.Lerp(transform.localScale, escalaOriginal * bigMult, 1.5f * Time.deltaTime);
		//label.alpha = Mathf.Lerp (label.alpha, 0, 2f * Time.deltaTime);
		
	}
}
