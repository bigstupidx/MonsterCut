<?php

function conectar(){
	$link = mysql_connect('localhost', 'nemorisg_medusa', 'o0e[krGNyhNv');
	if (!$link) {
		die('Not connected : ' . mysql_error());
	}

	$db_selected = mysql_select_db('nemorisg_medusa', $link);
	if (!$db_selected) {
		die ('Can\'t use DB : ' . mysql_error());
	}
	//return $db_selected;
}
conectar();

function ejecutarQuery($query){
	$result = mysql_query($query);
	if (!$result) {
		$message  = 'Invalid query: ' . mysql_error() . "\n";
		$message .= 'Whole query: ' . $query;
		die($message);
	}
	return $result;
}

$operacion=$_GET['operacion'];
if($operacion == 1) activar($_POST['pk_usuario'], $_POST['campo'], $_POST['playerPref']);
if($operacion == 2) obtenerParametro($_POST['idFacebook'], $_POST['campo']);
if($operacion == 3) encontrarAmigo($_POST['idFacebook'], $_POST['idFacebookAmigo']);
if($operacion == 4) validate_receipt($_POST['receipt'], $_POST['sandbox']);
if($operacion == 5) encontrarAmigos($_POST['idFacebook'], $_POST['idFacebookAmigos']);
if($operacion == 7) guardarRegistrationID("'".$_POST['param0']."'");

function guardarRegistrationID($registrationID){
	$result = ejecutarQuery("insert ignore into PushNotifications(RegistrationID) values($registrationID);");
}

function obtenerRegistrationID(){
	$result=ejecutarQuery("select RegistrationID from PushNotifications");
	$arreglo = array();
	while($row = mysql_fetch_assoc($result)) {
		$arreglo[] = $row['RegistrationID'];
	}
	return $arreglo;
}

function validate_receipt($receipt_data, $sandbox_receipt = FALSE) {
        if ($sandbox_receipt) {
            $url = "https://sandbox.itunes.apple.com/verifyReceipt/";
        }
        else {
            $url = "https://buy.itunes.apple.com/verifyReceipt";
        }
        $ch = curl_init($url);
        $data_string = json_encode(array(
            'receipt-data' => $receipt_data,
        ));
        curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
        curl_setopt($ch, CURLOPT_POSTFIELDS, $data_string);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, TRUE);
        curl_setopt($ch, CURLOPT_HTTPHEADER, array(
            'Content-Type: application/json',
            'Content-Length: ' . strlen($data_string))
        );
        $output = curl_exec($ch);
        $httpCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
        curl_close($ch);
        if (200 != $httpCode) {
            die("Error validating App Store transaction receipt. Response HTTP code $httpCode");
        }
        $decoded = json_decode($output, TRUE);
        echo $output;
}
function encontrarAmigo($idFacebook, $idFacebookAmigo){
	//revisa si ya se añadio
	$result=ejecutarQuery("SELECT idFacebook FROM amigo where (idFacebook = '$idFacebook' and idFacebookAmigo = '$idFacebookAmigo') or (idFacebook = '$idFacebookAmigo' and idFacebookAmigo = '$idFacebook');");
	$row = mysql_fetch_assoc($result);
	//si no, revisa si el amigo existe en la BD
	if($row == null){
		$resultUsuario=ejecutarQuery("SELECT idFacebook FROM usuario where idFacebook = '$idFacebookAmigo';");
		$rowUsuario = mysql_fetch_assoc($resultUsuario);
		//si el usuario existe en la BD
		if($rowUsuario != null){
			$resultAdd=ejecutarQuery("insert into amigo(idFacebook, idFacebookAmigo) values('$idFacebook', '$idFacebookAmigo');");
			echo "1";
		}
		//el amigo no está jugando
		else{
			echo "0";
		}
	}
	//la relacion existe en la tabla amigo
	else{
		echo "1";
	}
	
}

function encontrarAmigos($idFacebook, $idFacebookAmigos){
	$return = "";
	//revisa si ya se añadio
	$idAmigos = explode("|", $idFacebookAmigos);
	$query = "SELECT idFacebook FROM usuario where ";
	for($i = 0; $i < count($idAmigos); $i++){
		if($idFacebook != $idAmigos[$i]){
			$query .= "(idFacebook = '$idAmigos[$i]')";
			if($i != count($idAmigos) - 1) $query .= " or ";
		}
	}
	$query .= " group by idFacebook;";
	$result=ejecutarQuery($query);
	while($row = mysql_fetch_assoc($result)){
		$return .= $row['idFacebook']."|";
		//si no, revisa si el amigo existe en la BD
		/*if($row == null){
			//$resultUsuario=ejecutarQuery("SELECT idFacebook FROM usuario where idFacebook = '$idFacebookAmigo';");
			//$rowUsuario = mysql_fetch_assoc($resultUsuario);
			//si el usuario existe en la BD
			if($rowUsuario != null){
			//	$resultAdd=ejecutarQuery("insert into amigo(idFacebook, idFacebookAmigo) values('$idFacebook', '$idFacebookAmigo');");
				echo "1";
			}
			//el amigo no está jugando
			else{
				echo "0";
			}
		}
		//la relacion existe en la tabla amigo
		else{
			echo "1";
		}*/
	}
	echo $return;
}

//obtiene el valor de un parametro
function obtenerParametro($idFacebook, $nombreCampo){
	$result=ejecutarQuery("SELECT $nombreCampo FROM usuario where idFacebook = $idFacebook;");
	$row = mysql_fetch_assoc($result);
	echo $row["$nombreCampo"];
}

//almacena la activacion de un playerpref
function activar($pk_usuario, $nombreCampo, $playerPref){
	validarUsuario($pk_usuario);
	$result=ejecutarQuery("update usuario set $nombreCampo = '$playerPref' where pk_usuario = '$pk_usuario';");
}

//si el usuario no existe, lo crea
function validarUsuario($pk_usuario){
	$resultUser = ejecutarQuery("SELECT pk_usuario FROM usuario where pk_usuario = '$pk_usuario';");
	$rowUser = mysql_fetch_assoc($resultUser);
	if($rowUser['pk_usuario'] == ''){
		//usuario no existe
		$result=ejecutarQuery("insert into usuario(pk_usuario) values('$pk_usuario')");
	}
	echo "1";
}

?>