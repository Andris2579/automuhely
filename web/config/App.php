<?php

namespace Config;

require_once './../vendor/autoload.php';

use Dotenv\Dotenv;

$dotenv = Dotenv::createImmutable('C:/xampp/htdocs/automuhely_web/');
$dotenv->load();

class App{
    public static string $BASE_URL;
    public static string $APP_PATH;

    public static function init(): void {
        self::$BASE_URL = $_ENV['BASE_URL'];
        self::$APP_PATH = $_ENV['APP_PATH'];
    }
}

App::init();