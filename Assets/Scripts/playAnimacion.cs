using UnityEngine;
using System.Collections;
using SmoothMoves;

public class playAnimacion : MonoBehaviour {
	public BoneAnimation animacion;
	// Use this for initialization
	void Start () {
		animacion.Play("titulo");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
