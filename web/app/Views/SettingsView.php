<?php
namespace App\Views;
require_once __DIR__ . '/../../vendor/autoload.php';
use App\Controllers\UserController;

session_start();

$result = UserController::singleUser($_SESSION['user']['username']);

$text = "";

$text.= '<form action="" method="POST">
        <label for="email">Email</label><input type="email" name="email" id="email" value="'.$result['email'].'">
        <label for="password">Jelszó</label><input type="password" name="password" id="password">
        <label for="phone_number">Telefonszám</label><input type="tel" name="phone_number" id="phone_number" value="'.$result['telefonszam'].'">
        <label for="zip_code">Irányítószám</label><input type="text" name="zip_code" id="zip_code"';

$cim = explode(',', $result['cim']);

if(count($cim) <= 1){
            $text .= '>
            <label for="city">Város</label><input type="text" name="city" id="city">
            <label for="street">Utca</label><input type="text" name="street" id="street">
            <label for="house_number">Házszám</label><input type="text" name="house_number" id="house_number">
            <button id="user_settings_save" onclick=userSettingsSave(event)>Mentés</button>
        </form>';
}
else{
            $text .=' value="'.$cim[0].'">
            <label for="city">Város</label><input type="text" name="city" id="city" value="'.$cim[1].'">
            <label for="street">Utca</label><input type="text" name="street" id="street" value="'.$cim[2].'">
            <label for="house_number">Házszám</label><input type="text" name="house_number" id="house_number" value="'.$cim[3].'">
            <button id="user_settings_save" onclick=userSettingsSave(event)>Mentés</button>
        </form>';
}

echo json_encode($text);
