using UnityEngine;
using System.Collections;

public class globalVariables : MonoBehaviour {
	
	public static int[] estacionesNivel = {1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
	//aqui estan todos los asientos, asi que debe corresponder al numero de arriba
	public static int[] monstruosDificultadNivel = {0,
													0, 0,
													1, 0,
													1, 1,
													0, 1,
													2, 1,
													2, 2,
													3, 2,
													3, 1,
													0, 1, 0,
													0, 2, 0,
													1, 2, 0,
													1, 2, 1,
													1, 1, 1,
													1, 2, 2,
													1, 3, 2, 
													1, 3, 1,
													2, 3, 2,
													2, 3, 4,
													2, 2, 4,
													2, 3, 4, 
													4, 3, 4,
													4, 4, 4,
													2, 4, 2, 1,
													0, 3, 2, 1,
													2, 3, 3, 1, 
													2, 3, 3, 2,
													2, 2, 2, 2,
													1, 2, 2, 1,
													3, 2, 3, 2, 
													3, 3, 3, 2,
													2, 3, 3, 2,
													2, 4, 3, 3,
													3, 4, 3, 3,
													2, 3, 3, 2,
													2, 4, 3, 2,
													3, 4, 3, 2,
													3, 4, 3, 3,
													3, 4, 4, 3, 
													3, 4, 4, 4,
													4, 4, 4, 4,
													2, 4, 4, 4, 2,
													2, 2, 4, 2, 2,
													2, 2, 2, 2, 2,
													2, 2, 3, 2, 2,
													3, 2, 3, 2, 2, 
													3, 2, 4, 2, 3,
													3, 3, 4, 2, 3,
													3, 3, 4, 2, 2,
													3, 3, 4, 2, 4,
													3, 4, 4, 2, 4,
													2, 4, 3, 2, 2,
													3, 4, 3, 3, 2,
													3, 3, 2, 3, 2,
													2, 3, 3, 3, 2,
													3, 4, 3, 3, 2, 
													3, 4, 3, 3, 3,
													3, 4, 3, 4, 3,
													2, 4, 3, 4, 2,
													2, 4, 4, 4, 2,
													3, 4, 4, 4, 3,
													4, 4, 4, 4, 3,
													4, 4, 4, 4, 4};
	//experiencia para subir de nivel
	public static int[] niveles = {0, 300, 900, 2000, 5000, 10000, 19000, 35000, 55000, 85000};
	//public static int puntajeParaVida = 100;
	//public static int[] bonus = {10, 5, 5, 5, 10};
	static string[] misionesIng = {"Make 500 cuts", "Cut 30 top hair segments", "Attend 10 clients", "Use Jesse's slice ability 10 times", "Use Jesse's freeze ability 10 times", 
									"Stay alive for 3 minutes or more", "Make an upgrade", "Make 2000 cuts", "Cut 60 top hair segments", "Attend 15 clients", 
									"Use Jesse's slice ability 20 times", "Use Jesse's freeze ability 20 times", "Stay alive for 6 minutes or more", "Use a special scissor object", "Play with 2 barbershop objects on", 
									"Select Rosa as your barber", "Use 1 extra life", "Use Rosa's slice ability 10 times", "Use Rosa's superspeed ability 10 times", "Make 5000 cuts", 
		"Use the x2 gold object", "Play with 4 barbershop objects on", "Select Mr Brayan as your barber", "Use Ron's superstrength ability 10 times", "Use Ron's freeze time ability 10 times", 
		"Use Jesse's slice ability 30 times", "Use Jesse's freeze ability 30 times", "Attend 25 clients", "Use Rosa's slice ability 20 times", "Use Rosa's superspeed ability 20 times", 
		"Stay alive for 9 minutes or more", "Use 2 extra lives", "Play with 6 barbershop objects on", "Make 9000 cuts", "Use 3 extra lives", 
		"Use Ron's superstrength ability 20 times", "Use Ron's freeze time ability 20 times", "Play with 8 barbershop objects on", "Use the x3 gold object", "Use Rosa's slice ability 30 times", 
		"Use Rosa's superspeed ability 30 times", "Make 15000 cuts", "Cut 90 top hair segments", "Use Ron's superstrength ability 30 times", "Use Ron's freeze time ability 30 times", 
									"Play with an ability in level 5 or more", "Make an upgrade to max level"};
	static string[] misionesEsp = {"Realiza 500 cortes", "Corta 30 topes de pelo", "Atiende a 10 clientes", "Usa corte vertical de Jesse 10 veces", "Usa congelar de Jesse 10 veces", 
									"Mantente vivo 3 minutos", "Realiza una mejora", "Realiza 2000 cortes", "Corta 60 topes de pelo", "Atiende a 15 clientes", 
									"Usa corte vertical de Jesse 20 veces", "Usa congelar de Jesse 20 veces", "Mantente vivo 6 minutos", "Usa una tijera especial", "Juega con 2 objetos de peluquería activados", 
									"Selecciona a Rosa", "Usa una vida extra", "Usa corte horizontal de Rosa 10 veces", "Usa supervelocidad de Rosa 10 veces", "Realiza 5000 cortes", 
									"Usa el objeto Moneda x2", "Juega con 4 objetos de peluquería activados", "Selecciona a Ron", "Usa superfuerza de Ron 10 veces", "Usa detener tiempo de Ron 10 veces", 
									"Usa corte vertical de Jesse 30 veces", "Usa congelar de Jesse 30 veces", "Atiende a 10 clientes", "Usa corte horizontal de Rosa 10 veces", "Usa supervelocidad de Rosa 10 veces", 
		"Mantente vivo 9 minutos", "Usa 2 vidas extra", "Juega con 6 objetos de peluquería activados", "Realiza 9000 cortes", "Usa 3 vidas extra", 
		"Usa superfuerza de Ron 20 veces", "Usa detener tiempo de Ron 20 veces", "Juega con 8 objetos de peluquería activados", "Usa el objeto Moneda x3", "Usa corte horizontal de Rosa 30 veces", 
		"Usa supervelocidad de Rosa 30 veces", "Realiza 15000 cortes", "Corta 90 topes de pelo", "Usa superfuerza de Ron 30 veces", "Usa detener tiempo de Ron 30 veces", 
									"Juega con una habilidad en nivel 5 o más", "Deja una mejora en el nivel máximo"};
	static string[] misionesJap = {"500カットをとります","短い髪は30を停止","10クライアントを提供しています","ジェシーの垂直断面を10回使用","10回ジェシーフリーズが使用",
"15顧客にサービスを提供","60停止短い髪","2000カットを作る","改善を行う","3分生き続ける",
		"切羽ジェシーは20回使用","フリーズジェシーは20回使用","6分生き続ける ","特別なハサミを使用しています","美容師を活性化した2つのオブジェクトと遊んだ",
		"余分な生活を使用しています","選択ローズ ","使用水平カットは10回バラ","米国のスーパースピードは10倍の増加となりました","5000カットを作る",
"オブジェクト通貨×2を使用します","4オブジェクト活性化した美容師と遊んだ","ロンは選択された","スーパー強度のラム酒は10回使用","停止時間は10回ロンを使用しています",
		"切羽ジェシーは30回使用","フリーズジェシーは30回使用","10クライアントを提供しています ","使用水平カットを10回バラ","米国のスーパースピードを10回バラ",
		"使用2余分な生活","9分生き続ける","9000カットを作る","使用3余分な生活","活性化6オブジェクト美容師と遊んだ",
		"時間が20回ロンを使用して停止","スーパー強さのラム酒は20回使用","美容師を活性化した8オブジェクトと遊んだ","対象通貨X3を使用しています","使用水平カットを30回バラ",
"停止時間が30回ロンを使用しています","スーパー強度のラム酒は30回使用","15000ヒットカット","短い髪90キャップ","スーパースピードが30倍に上昇した使用",
"レベル5以上でスキルと遊ぶ","最大レベルの改善を残す"};
	static string[] misionesKor = {"10 배 동결 제시 사용", "이새의 수직 단면이 10 배 사용", "짧은 머리 30 정지", "500 컷을 간다", "10 고객들에게 서비스를 제공",
		"15 고객들에게 서비스를 제공", "60 정지 짧은 머리", "2000 인하한다", "개선한다", "삼분 살아있어",
		"2 개체를 활성화 미용사와 함께 연주", "특수 가위를 사용한다", "육분 살아있어", "동결 제씨는 20 번 사용", "Coalface 제시는 20 시간 사용",
		"5000 인하한다", "미국은 슈퍼 스피드가 10 배 증가", "미국은 수평 절단 10 배 증가", "여분의 생명을 사용합니다", "선택 장미",
		"론을 선택", "4 개체 활성화 미용사와 함께 연주", "객체 통화 (X2)를 사용합니다", "슈퍼 강도 럼 10 번 사용", "정지 시간은 론 10 번을 사용한다",
		"미국은 슈퍼 스피드는 10 배 증가했다", "미국이 수평 절단 10 배 증가", "10 고객들에게 서비스를 제공", "동결 제씨는 30 번 사용", "Coalface 제시는 30 시간 사용",
		"9000 인하한다", "6 개체 활성화 미용사와 함께 연주", "미국이 여분의 삶", "미국 3 여분의 삶을", "살아 구분을 그대로",
		"최고 강도 럼 20 번 사용", "미국은 수평 컷은 30 배 증가", "8 개​​체 활성화 미용사와 함께 연주", "정지 시간은 20 시간 론을 사용합니다", "객체 통화 X3를 사용",
		"정지 시간이 30 배 론을 사용합니다", "슈퍼 강도 럼 30 번 사용", "15000 안타 인하", "짧은 머리 (90) 모자", "슈퍼 스피드가 30 배 상승 사용",
		"레벨 5 이상에서 기술로 재생", "최대 수준 향상을 남긴다"};
	static string[] misionesPor = {"Fazer 500 cortes", "Walking 30 paradas de cabelo", "Atender a 10 clientes", "Use a seção vertical de Jesse 10X", "Use congelar Jesse 10x",
		"Fique vivo três minutos", "Faça uma melhoria", "Faça 2.000 cortes", "Walking 60 paradas de cabelo", "Atender a 15 clientes",
		"Use a seção vertical de Jesse 20 vezes", "Uso de Jesse congelar 20 vezes", "Stay Alive seis minutos", "Use uma tesoura especial", "Brinque com 2 objetos cabeleireiro ativado",
		"Selecione um Rose", "Use uma vida extra", "Use corte horizontal de Rosa 10 vezes", "Use SuperSpeed ​​Rosa 10 vezes", "Make 5000 cortes",
		"Usar o x2 objeto moeda", "Jogue com 4 objetos cabeleireiro ativado", "Selecione um Ron", "Uso de Ron 10x super-força", "parar o tempo Ron Use 10x",
		"Use a seção vertical de Jesse 30 vezes", "Uso de Jesse congelar 30 vezes", "Atender a 10 clientes", "Use corte horizontal de Rosa 10 vezes", "Use SuperSpeed ​​Rosa 10 vezes",
		"Fique vivo nove minutos", "Usa 2 vidas extra", "Jogo com 6 objetos cabeleireiro ativado", "Make 9000 cortes", "Usa 3 vidas extras",
		"Ron Use super-força 20 vezes", "tempo de parada Ron Use 20 vezes", "Tocar com 8 objetos cabeleireiro ativado", "Use o x3 objeto moeda", "Use corte horizontal da Rosa de 30 vezes",
		"Usar superspeed Rosa 30 vezes", "15000 Faça cortes", "Walking 90 paradas de cabelo", "Ron Use super-força 30 vezes", "tempo de parada Ron Use 30 vezes",
		"Jogar com uma habilidade no nível 5 ou superior", "Deixar uma melhora no nível máximo"};
	static string[] misionesRus = {"Займет 500 сокращений", "короткие волосы останавливается 30", "обслуживает 10 клиентов", "вертикальный разрез Джесси используется 10 раз", "используется 10 раз Джесси заморозить",
"Остаться в живых три минуты", "делает улучшение", "составляет 2000 порезы", "60 остановок короткие волосы", "служит 15 клиентов",
"Забоя Джесси используется 20 раз", "замораживание Джесси используется 20 раз", "остаться в живых шесть минут", "использует специальные ножницы", "играл с 2 объектов активированного парикмахера",
"Выбор роза", "использует дополнительную жизнь", "США горизонтальный срез выросли на 10 раз", "США SuperSpeed ​​выросли в 10 раз,", "делает 5000 сокращения",
"Использует объект валюты x2", "играл с 4 объектов активированного парикмахера", "выбран Рона", "супер сила ром используется 10 раз", "Время остановки использует 10 раз Рон",
"Забоя Джесси используется 30 раз", "замораживание Джесси используется 30 раз", "обслуживает 10 клиентов", "США горизонтальный срез выросли на 10 раз", "США SuperSpeed ​​выросли в 10 раз",
"Остаться в живых девять минут", "США 2 дополнительные жизни", "играл с 6 объектов активированного парикмахера", "делает 9000 сокращения", "США 3 дополнительных жизней",
"Супер сила ром используется 20 раз", "Время остановки использует 20 раз Рон", "играл с 8 объектов активированного парикмахера", "использует объект валюты x3", "США горизонтальный срез розы в 30 раз",
"Использование SuperSpeed ​​выросли в 30 раз", "15000 хитов сокращения", "короткие волосы 90 крышки", "супер сила ром используется 30 раз", "Время остановки использует 30 раз Рон",
"Игра с мастерством на уровне 5 или выше", "оставляет улучшение максимального уровня"};
	static string[] misionesFra = {"Faire 500 coupes", "Walking 30 arrêts cheveux", "Occupez-vous de 10 clients", "Utilisez section verticale de Jesse 10X", "utilisation de geler Jesse 10x",
		"Restez en vie trois minutes", "Faire une amélioration", "Faire 2000 coupes", "Walking 60 arrêts cheveux", "Occupez-vous de 15 clients",
		"Utiliser la section verticale de Jesse 20 fois", "Utilisation de Jesse geler 20 fois", "Stay Alive six minutes", "Utilisez des ciseaux spéciaux", "Jouez avec deux objets coiffure activé",
		"Sélectionner un Rose", "Utiliser une vie supplémentaire", "Utiliser coupe horizontale de Rosa 10 fois", "utilisation superspeed Rosa 10 fois", "Faire 5000 coupures",
		"Utilisez le x2 devise de l'objet", "Jouez avec quatre objets coiffure activé", "Sélectionner un Ron", "utilisation de Ron 10x super force», «arrêter le temps Ron utilisation 10x",
		"Utiliser la section verticale de Jesse 30 fois", "Utilisation de Jesse geler 30 fois", "Occupez-vous de 10 clients», «Use coupe horizontale de Rosa 10 fois", "utilisation superspeed Rosa 10 fois",
		"Restez en vie neuf minutes", "Utilise deux vies supplémentaires", "Jouez avec 6 objets coiffure activé", "Faire 9000 coupes", "Utilise 3 vies supplémentaires",
		"Ron utilisation super force 20 fois", "temps d'arrêt Ron utilisation 20 fois", "Jouer avec 8 objets coiffure activé", "Utilisez le x3 de devise de l'objet", "Utiliser coupe horizontale de Rosa 30 fois",
		"Utilisez SuperSpeed ​​Rosa 30 fois", "15000 Faire des coupes", "Walking 90 arrêts cheveux", "Ron utilisation super force 30 fois", "temps d'arrêt Ron utilisation 30 fois",
		"Jouez avec une compétence au niveau 5 ou plus", "Laissez une amélioration dans le niveau maximum"};
	static string[] misionesDeu = {"Stellen Sie 500 Schnitte", "Walking 30 Stationen Haar", "Cater zu 10 Kunden", "Verwenden vertikalen Schnitt Jesse 10X", "Verwenden einfrieren Jesse 10x",
		"Bleib am Leben, 3 Minuten", "Stellen Sie eine Verbesserung", "Make 2000 Schnitte", "Walking 60 Haltestellen Haar", "Cater zu 15 Kunden",
		"Mit vertikalen Schnitt Jesse 20 Mal", "Nutzung von Jesse einfrieren 20 Mal", "Bleib am Leben, 6 Minuten", "mit einer speziellen Schere", "Spiel mit 2 Objekte aktiviert Friseur",
		"Wählen Sie ein Rose", "Verwenden Sie ein Extra-Leben", "Verwenden horizontalen Schnitt von Rosa 10-mal", "Use Superspeed-Rosa 10-mal", "Make 5000 Schnitte",
		"Verwenden Sie die Objektwährung x2", "Spiel mit 4 Objekte aktiviert Friseur", "Wählen Sie ein Ron", "Einsatz von Ron 10x Super-Stärke", "Stoppzeit Ron Verwenden 10x",
		"Mit vertikalen Schnitt Jesse 30 Mal", "Nutzung von Jesse einfrieren 30 Mal", "Cater zu 10 Kunden", "Verwenden horizontalen Schnitt von Rosa 10-mal", "Use Superspeed-Rosa 10 Mal",
		"Bleib am Leben, 9 Minuten", "Benötigt 2 Extraleben", "Spiel mit 6 Objekte aktiviert Friseur", "Make 9000 Schnitte", "Verwendet 3 Extraleben",
		"Ron Verwenden Super-Stärke 20 Mal", "Stoppzeit Ron Verwenden 20 Mal", "Spielen Sie mit 8 Objekten aktiviert Friseur", "Verwenden Sie die Objektwährung x3", "Verwenden horizontalen Schnitt von Rosa 30 Mal",
		"Mit Superspeed-Rosa 30-mal", "15000 Stellen Schnitte", "Walking 90 Haltestellen Haar", "Ron Verwenden Super-Stärke 30 Mal", "Stoppzeit Ron Verwenden 30 Mal",
		"Spiel mit einem Fähigkeit auf Stufe 5 oder höher", "Lassen Sie eine Verbesserung in der Maximalpegel"};
	static string[] misionesIta = {"Make 500 tagli", "A piedi i capelli 30 tappe", "Cater a 10 clienti", "Usa sezione verticale di Jesse 10X", "Usa congelare Jesse 10x",
		"Resta vivo tre minuti", "Make un miglioramento", "Make 2.000 tagli", "Walking capelli 60 tappe", "Cater a 15 clienti",
		"Usa sezione verticale di Jesse 20 volte", "Uso di Jesse congelare 20 volte", "rimanere in vita sei minuti", "Usa una speciale forbice", "Gioca con 2 oggetti parrucchiere attivato",
		"Seleziona una Rosa", "Usa una vita extra", "Usa taglio orizzontale di Rosa 10 volte", "Usa superspeed Rosa 10 volte", "Make 5000 tagli",
		"Utilizzare l'x2 valuta oggetto", "Gioca con 4 oggetti parrucchiere attivato", "Seleziona una Ron", "Uso di Ron 10x super forza", "fermare il tempo Ron Usa 10x",
		"Usa sezione verticale di Jesse 30 volte", "Uso di Jesse congelare 30 volte", "Cater a 10 clienti", "Usa taglio orizzontale di Rosa 10 volte", "Usa superspeed Rosa 10 volte",
		"Rimanere in vita nove minuti", "Usa 2 vite extra", "Gioca con 6 oggetti parrucchiere attivato", "Make 9000 tagli", "Usa 3 vite extra",
		"Ron Usa super forza 20 volte", "tempo di arresto Ron uso 20 volte", "Gioca con 8 oggetti parrucchiere attivato", "Usa il x3 valuta oggetto", "Usa taglio orizzontale di Rosa 30 volte",
		"Usa SuperSpeed ​​Rosa 30 volte", "15000 tagli", "Walking capelli 90 tappe", "Ron Usa super forza 30 volte", "tempo di arresto Ron uso 30 volte",
		"Gioca con un'abilità a livello 5 o superiore", "Lascia un miglioramento del livello massimo"};
	public static int[] misionesCondicion = { 500, 30, 10, 10, 10, 
												3, 1, 2000, 60, 15, 
												20, 20, 6, 1, 1, 
												1, 1, 10, 10, 5000, 
												1, 1, 1, 10, 10, 
												30, 30, 25, 20, 20, 
												9, 1, 1, 9000, 3,
												20, 20, 1, 1, 30, 
												30, 15000, 90, 30, 30, 
												1, 1};
	public static int[] misionesRecompensa = { 5, 7, 5, 7, 7, 
												5, 7, 7, 20, 10,
												15, 15, 15, 7, 15,
												10, 15, 7, 7, 12,
												10, 10, 15, 7, 7,
												22, 22, 25, 15, 15,
												30, 40, 10, 20, 30,
												15, 15, 10, 15, 22,
												22, 30, 32, 22, 22,
												20, 50};
	static string[] misionesImagen = {"tijerasIcono", "serpiente","clientes", "poderCorteIcono","poderCongelarIcono",
										"relojIcono", "upgradeIcono","tijerasIcono", "serpiente","clientes",
										"poderCorteIcono", "poderCongelarIcono","relojIcono", "tijeraDoradaIcono","visualIcono",
										"retrato", "vidas", "poderCorteIcono", "poderCongelarIcono","tijerasIcono",
										"monedaX2Icono", "ronnyVisualIcono","retrato", "poderCorteIcono", "poderCongelarIcono",
										"poderCorteIcono", "poderCongelarIcono","clientes", "poderCorteIcono", "poderCongelarIcono",
										"relojIcono", "vidas","ronnyVisualIcono", "tijerasIcono","vidas",
										"poderCorteIcono", "poderCongelarIcono","ronnyVisualIcono", "monedaX3Icono","poderCorteIcono",
										"poderCongelarIcono", "tijerasIcono","serpiente", "poderCorteIcono", "poderCongelarIcono",
										"upgradeIcono", "upgradeIcono"};
	
	public static int[] costoPeluquero = {0, 350, 500};
	
	public static string obtenerMision(int id){
		if(PlayerPrefs.GetString("Language").Contains("Eng")){
			return misionesIng[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Esp")){
			return misionesEsp[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Jap")){
			return misionesJap[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Kor")){
			return misionesKor[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Por")){
			return misionesPor[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Rus")){
			return misionesRus[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Fra")){
			return misionesFra[id];
		}
		if(PlayerPrefs.GetString("Language").Contains("Deu")){
			return misionesDeu[id];
		}
		
		if(PlayerPrefs.GetString("Language").Contains("Ita")){
			return misionesIta[id];
		}
		return misionesIng[id];
	}
	
	public static string obtenerMisionImagen(int id){
		return misionesImagen[id];
	}
	
	public static void establecerMisiones(){
		bool encontrado = false;
		for(int i = 0; i< 3; i++){
			encontrado = false;
			if(!PlayerPrefs.HasKey("misionSlot"+i)){
				print ("buscando mision "+i);
				for(int j = 0; j < misionesIng.Length; j++){
					//0: no utilizada
					//1: seleccionada
					//2: terminada
					if(PlayerPrefs.GetInt("mision"+j, 0) == 0){
						print ("encontrado " + j);
						PlayerPrefs.SetInt("misionSlot"+i, j);
						PlayerPrefs.SetInt("mision"+j, 1);
						encontrado = true;
						break;
					}
				}
				if(!encontrado){
					print ("buscando entre terminados");
					for(int j = 0; j < 10; j++){
						int r = Random.Range(0, misionesIng.Length);
						if(PlayerPrefs.GetInt("mision"+r, 0) == 2){
							PlayerPrefs.SetInt("misionSlot"+i, r);
							PlayerPrefs.SetInt("mision"+r, 1);
							encontrado = true;
							print ("encontrado"+r);
							break;
						}
					}
				}
			}
		}
	}
	
	public static bool[] revisarMisiones(int[] valores){
		bool[] retorno = {false, false, false};
		for(int i = 0; i < 3; i++){
			if(valores[PlayerPrefs.GetInt("misionSlot"+i)] >= misionesCondicion[PlayerPrefs.GetInt("misionSlot"+i)]){
				PlayerPrefs.SetInt("mision"+PlayerPrefs.GetInt("misionSlot"+i), 2);
				retorno[i] = true;	
				print ("mision terminada "+i);
			}
		}
		return retorno;
	}
	
	public static int calcularIncremento(int valor){
		if(Mathf.Abs(valor) > 1000) return 50;
		else 
			if(Mathf.Abs(valor) > 500) return 20;
			else 
				if(Mathf.Abs(valor) > 100) return 5;	
		return 1;
	}
	
}
