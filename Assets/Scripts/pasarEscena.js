var tiempo:float=3.0;
var escena:String;
var verBotones:boolean=true;
var buscarCentral=false;
var mensaje="PLAY";
var textura:Texture;
//var loading:Transform;
private var componente;
//private var boton:GUI.Button;
function Awake(){
	Time.timeScale = 1f;
}

function OnGUI(){
	if(verBotones){
		//GUI.Box(Rect (380,225,173,128), "");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(textura==null){
			//GUI.Box(Rect (473,403,117,20), "");
			if (GUI.Button (Rect (423,353,157,20), mensaje+" in fastest quality     ")) {
				//if(loading!=null) loading.guiText.text="Loading...";
				QualitySettings.currentLevel = QualityLevel.Fastest;
				Application.LoadLevel(escena);
			}
			if (GUI.Button (Rect (423,378,157,20), mensaje+" in good quality     ")) {
				//if(loading!=null) loading.guiText.text="Loading...";
				QualitySettings.currentLevel = QualityLevel.Good;
				Application.LoadLevel(escena);
			}
			if (GUI.Button (Rect (423,403,157,20), mensaje+" in fantastic quality")) {
				//if(loading!=null) loading.guiText.text="Loading...";
				QualitySettings.currentLevel = QualityLevel.Fantastic;
				Application.LoadLevel(escena);
			}
		}
		else{
			if (GUI.Button (Rect (345,360,243,58), textura)) {
				//if(loading!=null) loading.guiText.text="Loading...";
				Application.LoadLevel(escena);
			}
		}	
	}
}
function Update () {
	if(tiempo!=-1){
		//tiempo-=Time.deltaTime;
		if(tiempo < Time.timeSinceLevelLoad){ 
			var t=GameObject.Find(".TransicionEscena");
			if(t!=null) t.SendMessage("pasarEscena", escena);
			else Application.LoadLevel(escena); 
			
		}
	}
}