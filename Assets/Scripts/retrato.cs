using UnityEngine;
using System.Collections;

public class retrato : MonoBehaviour {
	public string nombre;
	public UIButton boton;
	public string[] imagenBloqueado;
	public string imagenDesbloqueado;
	public string[] descripcion;
	public int puntosDesbloqueo;
	public string poder1imagen;
	public string poder2imagen;
	public string[] poder1descripcion;
	public string[] poder2descripcion;
	// Use this for initialization
	void Start () {
		
	}
	
	public void seleccionado(bool b){
//		if(b) boton.transform.localScale = Vector3.one;
//		else boton.transform.localScale = Vector3.one * 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
