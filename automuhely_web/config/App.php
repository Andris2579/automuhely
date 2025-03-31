<?php

namespace Config;

require_once './../vendor/autoload.php';

use Dotenv\Dotenv;

//Betölti a környezeti változókat a megadott elérési útvonalról
$dotenv = Dotenv::createImmutable('C:/xampp/htdocs/automuhely_web/');
$dotenv->load();

class App{
    public static string $BASE_URL;
    public static string $APP_PATH;

    public static string $JWT_SECRET;

    //Beállítja a környezeti változók megfelelő értékeit
    public static function init(): void {
        self::$BASE_URL = $_ENV['BASE_URL'];
        self::$APP_PATH = $_ENV['APP_PATH'];
        self::$JWT_SECRET = $_ENV['JWT_SECRET'];
    }
}

App::init();