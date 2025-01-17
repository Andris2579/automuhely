<?php

session_start();

require_once './../config/App.php';
require_once './../vendor/autoload.php';

use Config\App;
use App\Controllers\CarController;
use App\Controllers\UserController;
use App\Controllers\CarBrandController;
use App\Controllers\CarTypeController;
use App\Controllers\ServiceController;

$method = $_SERVER['REQUEST_METHOD'];
$requestUri = explode('?', $_SERVER['REQUEST_URI'])[0];
$uri = explode('/', $requestUri);
$params = $method == 'GET' ? $_GET : $_POST;

$exactUri = [];
$baseIndex = array_search('api.php',$uri);
for($i = $baseIndex+1; $i < count($uri); $i++){
    $exactUri[] = $uri[$i];
}

$endpoint = implode('/', $exactUri);

function sendResponse($data, $statusCode = 200) {
    http_response_code($statusCode);
    echo json_encode($data);
    exit;
}

switch($method){
    case "GET":
        switch($endpoint){
            case 'header':
                include App::$APP_PATH.'app/Views/header.php';
                break;
            case 'footer':
                include App::$APP_PATH.'app/Views/footer.php';
                break;
            case 'cars':
                sendResponse(CarController::userCars());
                break;
            case 'carBrand':
                sendResponse(CarBrandController::allBrand());
                break;
            case 'carType':
                sendResponse(CarTypeController::specificType($params));
                break;
            case 'services':
                sendResponse(["success" => true, "data" => ServiceController::allService()]);
                break;
            case 'userLoggedIn':
                if(UserController::userLoggedIn()){
                    sendResponse(UserController::userLoggedIn());
                }
                break;
        }
        break;
    case "POST":
        switch($endpoint){
            case 'user':
                UserController::createUser($params);
                sendResponse($data);
                break;
            case 'auth/login':
                sendResponse(UserController::loginUser($params));
                break;
            case 'auth/logout':
                session_unset();
                session_destroy();
                sendResponse(["success" => true, "message" => "Sikeres kijelentkezés"], 200);
                break;
            case 'userSettingsSave':
                UserController::updateUser($params);
                break;
            case 'car':
                CarController::createCar($params);
                break;
            case 'service':
                ServiceController::bookService($params);
                break;
        }
        break;
    case "PUT":
        switch($endpoint){
            case 'cars':
                CarController::updateCar($params);
                break;
        }
        break;
}