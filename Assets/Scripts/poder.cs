using UnityEngine;
using System.Collections;

public class poder : MonoBehaviour {
	public int exp;
	public int nivel; //0 - 10
	//public int expUso;
	public UILabel nivelLabel;
	//public UISlider expBar;
	
	public int posicion = 0; //[0 ó 1]
	public float cooldown;
	float cooldownActual;
	public UISprite cooldownImage;
	//public Transform cooldownFijoImage;
	//public UISprite buttonImage;
	//float cooldownImageScaleY;
	public string definicion;
	
	public string funcion;
	bool activo = false;
	
	GameObject peluqueroObj;
	
	public int expNivelSiguiente = 0;
	public int expNivelAnterior = 0;
	
	public GameObject efectoEspecial;
	
	public AudioClip sonidoPoder;
	
	/*void setNivel(int exp){
		this.exp = exp;
		nivel = (int) (2f * Mathf.Pow(exp, (1f / 3f)) / 3F);
		setNivelesExperiencia();
		addExp(0);
	}
	
	void setNivelesExperiencia(){
		expNivelSiguiente = (int) Mathf.Pow((nivel + 1) * 1.5f, 3);
		expNivelAnterior = (int) Mathf.Pow((nivel) * 1.5f, 3);
		nivelLabel.text = "" + nivel;
	}*/
	
	void setNivel(int n){
		nivel = n;
		nivelLabel.text = "" + nivel;
	}
	
	void Start(){
		cooldownImage.gameObject.SetActive(false);
		//cooldownFijoImage.gameObject.SetActive(false);
		peluqueroObj = GameObject.Find("Jesse(Clone)");
        if (peluqueroObj == null)
        {
            peluqueroObj = GameObject.Find("Ron(Clone)");

            if (peluqueroObj == null)
                peluqueroObj = GameObject.Find("Rosa(Clone)");
        }
        if (posicion == 0)
			cooldown *= 1f - 0.07f * PlayerPrefs.GetInt("mejoraCD1",0); 
		else
			cooldown *= 1f - 0.07f * PlayerPrefs.GetInt("mejoraCD2",0);
		
		
		cooldownActual = Time.time;
		//cooldownImageScaleY = cooldownImage.localScale.y;
		setCooldownScale(0);
		//recuperar de player prefs
		//setNivelesExperiencia();
	}
	
	public void activar(bool b){
		activo = b;
		if(activo) cooldownActual = Time.time - cooldown;
	}
	
	void reducirCooldownPorcentaje(float p){
		cooldown -= cooldown * p;
	}
	
	/*void addExp(int e){
		if(nivel < 10){
			exp += e;
			if(exp >= expNivelSiguiente){
				levelUp ();	
			}
			expBar.sliderValue = (float) (exp - expNivelAnterior) / (float) (expNivelSiguiente - expNivelAnterior);
		}
		if(nivel >= 10)
			exp = expNivelAnterior;
	}
	void levelUp(){
		if(nivel < 10){
			nivel++;
			setNivel(nivel);
		}
	}*/
	
	void Update(){
		if(activo){
			if(cooldownActual + cooldown >= Time.time){
				//buttonImage.color =  new Color(0.5f, 0.5f, 0.5f, 0.8f);
				//setCooldownScale((1 - (Time.time - cooldownActual) / cooldown) * cooldownImageScaleY);
				setCooldownScale((1 - (Time.time - cooldownActual) / cooldown));
			}
			else{
				//buttonImage.color =  new Color(1f, 1f, 1f, 0.8f);
				cooldownImage.gameObject.SetActive(false);
				//cooldownFijoImage.gameObject.SetActive(false);
				setCooldownScale(0);
			}
		}
	}
	
	void setCooldownScale(float percentage){
		//cooldownImage.localScale = new Vector2(percentage, percentage);
//		print (percentage);
		//cooldownImage.localEulerAngles = new Vector3(0f,0f, percentage * 360f);
		cooldownImage.fillAmount = percentage;
	}
	
	void OnClick(){
		if(activo){
			
			if(cooldownActual + cooldown < Time.time){
				switch(funcion){
					case "poderCongelar":
						GameObject ef = (GameObject)Instantiate (efectoEspecial, new Vector3(Camera.main.transform.position.x, 0, 0), Quaternion.identity);
						//modificar esto segun lo que diga en script monstruo
						ef.SendMessage ("setTiempo", 5 + 3 * (nivel));
					break;
					case "poderCortarCompleto":
						ef = (GameObject)Instantiate (efectoEspecial);
						GameObject tij = GameObject.Find("TijerasChicas");
						ef.SendMessage("setAngulo", tij.transform.eulerAngles.z);
						//modificar esto segun lo que diga en script monstruo
						//ef.SendMessage ("setTiempo", 5 + 3 * (nivel));
					break;
					case "poderDetenerTiempo":
						ef = (GameObject)Instantiate (efectoEspecial, new Vector3(Camera.main.transform.position.x, 0, 0), Quaternion.identity);
						//modificar esto segun lo que diga en script central
						ef.SendMessage ("setTiempo", (3 + nivel));
					break;
					case "poderCortarPuntas":
						ef = (GameObject)Instantiate (efectoEspecial, new Vector3(Camera.main.transform.position.x, 100, 0), efectoEspecial.transform.rotation);
						//tij = GameObject.Find("TijerasChicas");
						ef.SendMessage("activar");
						//modificar esto segun lo que diga en script monstruo
						//ef.SendMessage ("setTiempo", 5 + 3 * (nivel));
					break;
				}
				if(PlayerPrefs.GetInt("sonido", 1) == 1)
					GetComponent<AudioSource>().PlayOneShot(sonidoPoder);
				cooldownImage.gameObject.SetActive(true);
				//cooldownFijoImage.gameObject.SetActive(true);
				print (funcion);
				peluqueroObj.SendMessage(funcion, nivel);
				//addExp(expUso);
				cooldownActual = Time.time;
			}
		}
	}
}
