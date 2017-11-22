using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class IAPNemoris : MonoBehaviour
{ 
/*#if UNITY_IPHONE || UNITY_ANDROID
	public bool isTest = true;
	public List<IAPProduct> _productos;
	public bool compraTerminada=true;
	public int[] montosMonedas;
	public storeItem[] productos;
	public string[] androidSkus = new string[] { "coinpack1", "coinpack2", "coinpack3", "coinpack4", "coinpack5" };
	public string[] iosProductIds = new string[] { "coinpack1", "coinpack2", "coinpack3", "coinpack4", "coinpack5" };
	public GameObject imagenLoading;

	string receiptActual;

	public bool debug = true;
	string retorno = "";

	void Start(){
		imagenLoading.SetActive (false);
		var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAq+KovKFwErE4qjzdGNtbUaNUPlkCB+JOHUHZ0V/dq6QLrErOYjQWGtmXGrDoRRks6WlAiZrVLYEGnfCNliLVnvWjytwI50CIMOFqR3BQxfzEsWx+933/F5LPhdmasz2GSCHmkAlAnmENQHKoG/zHQqXZjxdzLrRYNMMJ3Hl4Vfi/Apv+8KjqCjRwqW2lIllmjPis6giNDmJ2cRvVbX9oAiiYTT4VwUS5OiuyWgbZA3a0dvjpbM1A/hXf7Jk5YUY8fePiJVaEcY0Z6tsDMyT1Z42FC0ZqA0hESrsxCqv3SLW//MmaMOIoTUo9AZBiTW0qpxRvSzu03wn06A47VyLVJQIDAQAB";
		#if UNITY_ANDROID
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAq+KovKFwErE4qjzdGNtbUaNUPlkCB+JOHUHZ0V/dq6QLrErOYjQWGtmXGrDoRRks6WlAiZrVLYEGnfCNliLVnvWjytwI50CIMOFqR3BQxfzEsWx+933/F5LPhdmasz2GSCHmkAlAnmENQHKoG/zHQqXZjxdzLrRYNMMJ3Hl4Vfi/Apv+8KjqCjRwqW2lIllmjPis6giNDmJ2cRvVbX9oAiiYTT4VwUS5OiuyWgbZA3a0dvjpbM1A/hXf7Jk5YUY8fePiJVaEcY0Z6tsDMyT1Z42FC0ZqA0hESrsxCqv3SLW//MmaMOIoTUo9AZBiTW0qpxRvSzu03wn06A47VyLVJQIDAQAB";
		#endif	
		#if UNITY_IPHONE
		//no estoy seguro de que esto sirva para ios
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmffbbQPr/zqRjP3vkxr1601/eKsXm5kO2NzQge8m7PeUj5V+saeounyL34U8WoZ3BvCRKbw6DrRLs2DMoVuCLq7QtJggBHT/bBSHGczEXGIPjWpw6OQb24EWM0PaTRTH2x2mC/X6RwIKcPLJFmy68T38Eh0DXnF4jjiIoaD0W8AYLjLzv0WvbIfgtJlvmmwvI2/Kta1LRnW3/Ggi5jb9UmXZAUIBz8kQtSH5FUCmFOQHMzekfg8rQ4VO1nlWhnB58UPwsxWt/DNyDfqv2VMeA2+VJG0fkiMl/6vWA7+ianVTU3owXcvxJHseEDUVYo1wEKfhK7ErGB7sxDJx5wHXAwIDAQAB";
		#endif	
		IAP.init (key);
	}

	public void OnEnable(){
		#if UNITY_IPHONE
		StoreKitManager.purchaseSuccessfulEvent += purchase_sucessful;
		#endif
		#if UNITY_ANDROID
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		#endif	
	}

	public void OnDisable(){
		#if UNITY_IPHONE
		StoreKitManager.purchaseSuccessfulEvent -= purchase_sucessful;
		#endif
		#if UNITY_ANDROID
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		#endif	
	}
	
	//esto se ejecuta solo en android
	void billingSupportedEvent()
	{
		retorno = "billing supported";
		cargarProductos();
	}

	#if UNITY_ANDROID
	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		retorno = ( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
	}
	#endif
	
	void queryInventoryFailedEvent( string error )
	{
		retorno += "queryInventoryFailedEvent: " + error ;
	}

	#if UNITY_IPHONE
	public void purchase_sucessful(StoreKitTransaction transaction){
		receiptActual = transaction.base64EncodedTransactionReceipt;
		StartCoroutine("validarReceipt", transaction.productIdentifier);
	}
	#endif
	public void cargarProductos(){
		retorno = "cargando productos";
		IAP.requestProductData( iosProductIds, androidSkus, _productos =>
		{
			Debug.Log( "Product list received " + _productos.Count );
			retorno = "Product list received " + _productos.Count;
			Utils.logObject( _productos );

			productos = new storeItem[_productos.Count];
			for(int i = 0; i < productos.Length; i++) {
				productos[i] = new storeItem();
				productos[i].currency = _productos[i].currencyCode;
				productos[i].id = _productos[i].productId;
				productos[i].monedas = montosMonedas[i];
				productos[i].nombre = _productos[i].title;
				productos[i].precio = _productos[i].price;
				print ("agregado " + productos[i].nombre);
				retorno += "agregado " + productos[i].nombre;
			}
			print ("productos sumados");
			retorno += "productos sumados";
		});

	}

	public void comprar(int id){
		compraTerminada = false;
		imagenLoading.SetActive (true);
		//IAP.receipt = "";
		receiptActual = "";
		retorno = "comprar";
		#if UNITY_ANDROID
		var productId = androidSkus[id];
		#elif UNITY_IPHONE
		var productId = iosProductIds[id];
		#endif
		IAP.purchaseConsumableProduct( productId, (didSucceed, error) =>
		{
			Debug.Log( "purchasing product " + productId + " result: " + didSucceed );
			retorno = "purchasing product " + productId + " result: " + didSucceed;
			if(didSucceed){
				#if UNITY_ANDROID
				compraTerminada = true;
				retorno = "compra exitosa";
				retorno += montosMonedas[obtenerIndiceMonto(productId)] + " " + obtenerIndiceMonto(productId) + " " + productId;
				gameObject.SendMessage("compraExitosa", montosMonedas[obtenerIndiceMonto(productId)], SendMessageOptions.DontRequireReceiver);
				imagenLoading.SetActive (false);
				#elif UNITY_IPHONE
				//VALIDAR RECEIPT
				//StartCoroutine("validarReceipt", productId);
				#endif
				PlayerPrefs.SetInt("activateAds", 0);
			}
			else{
				gameObject.SendMessage("compraFallida", 0, SendMessageOptions.DontRequireReceiver);
				imagenLoading.SetActive (false);
				compraTerminada = true;
			}

		});
	}

	public IEnumerator validarReceipt(string productId){
		while (receiptActual == ""){ //IAP.receipt == "") {
			yield return new WaitForSeconds(0.5f);
		}

		//Note: your data can only be numbers and strings.  This is not a solution for object serialization or anything like that.

		WWWForm form = new WWWForm();
		form.AddField ("receipt", receiptActual); //IAP.receipt );
		form.AddField( "sandbox", "" + isTest );
		var download = new WWW( "http://www.nemorisgames.com/medusa/funciones.php?operacion=4", form);
		yield return download;
		if(download.error != null) {
			print( "Error downloading: " + download.error );
			retorno = "Error downloading: " + download.error;
			//mostrarError("Error de conexion");
			yield return false;
		} else {
			string retorno = download.text;
			print ("retorno receipt " + retorno);
			retorno = "retorno receipt " + retorno;
			if(retorno == ""){
				//error :(
				//mostrarError("Error de conexion");
			}
			else{
				//exito!
				//escribe en consola lo que recibe. no se esta parseando
				//JSONObject j = new JSONObject(retorno);
				//accessData(j);

				gameObject.SendMessage("compraExitosa", montosMonedas[obtenerIndiceMonto(productId)], SendMessageOptions.DontRequireReceiver);

				//Application.LoadLevel(Application.loadedLevelName);
			}
		}	
		imagenLoading.SetActive (false);
		compraTerminada = true;
	}
	
	int obtenerIndiceMonto(string id){
		for(int i = 0; i < productos.Length; i++){
			if(productos[i].id == id) return i;	
		}
		return -1;
	}

	void OnGUI(){
		if (!debug)
			return;
		GUI.Box (new Rect (0, 0, Screen.width, 200), "ret: " + retorno);

	}

#endif
*/
}

[System.Serializable]
public class storeItem{
	public string nombre;
	public int monedas;
	public string id;
	public string precio;
	public string currency;
}
