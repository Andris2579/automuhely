<?php

header('Content-Type: application/json');

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
$uriSegments = explode('/', trim($requestUri, '/'));

$params = [];

//Eldönti, hogy a $method változónak megfelelően hogyan dolgozza fel és tárolja a fogadott adatokat
if($method =="GET"){
    $params = $_GET;
}
else if ($method == "PUT") {
    $input = file_get_contents("php://input");

    $params = json_decode($input, true);

    if (json_last_error() !== JSON_ERROR_NONE) {
        http_response_code(400);
        echo json_encode(["error" => "Invalid JSON format"]);
        exit;
    }
} else if ($method == "POST") {
    $params = $_POST;
}

//Kinyeri az url szükséges részét
$baseIndex = array_search('api.php',$uriSegments);
$exactUri = array_slice($uriSegments, $baseIndex + 1);
foreach ($exactUri as $segment) {
    if (is_numeric($segment)) {
        $params[] = (int) $segment;
    }
}

//A $method változó megfelelő értéke alapján kiválasztja a $routes asszociatív tömbből a megfelelő meghívandó függvényt
function parseRoute($method, $exactUri, $routes){
    global $params;
    $requestPath = '/'.implode('/', $exactUri);
    foreach($routes[$method] as $routePattern => $handler){
        $regex = preg_replace('/\{([a-zA-Z0-9_]+)\}/', '(?<$1>[^/]+)', $routePattern);
        $regex = "#^{$regex}$#";

        if(preg_match($regex, $requestPath, $matches)){
            foreach($matches as $key => $value){
                if(is_string($key)){
                    $params[$key] = $value;
                }
            }
            return['handler' => $handler, 'params' => $params];
        }
    }
    return null;
}

//Visszaküldi az Ajax kérésnek a választ
function sendResponse($data) {
    http_response_code($data['code'] ?? 200);
    echo json_encode($data);
    exit;
}

//Itt található a $method változó értékének megfelelően rendezett összes API végpont, amit a szerver kezelni tud.
$routes = [
    'GET' => [
        '/users/{userId}' => function($params){
            sendResponse(UserController::singleUser($params[0]));
        },
        '/users/{userId}/cars' => function($params) {
            sendResponse(CarController::userCars($params[0]));
        },
        '/users/{userId}/cars/{licenseNumber}' => function($params){
            sendResponse(CarController::userCar($params));
        },
        '/users/{userId}/cars/{licenseNumber}/services' => function($params){
            sendResponse(CarController::getCarServices($params));
        },
        '/header' => function() {
            include App::$APP_PATH.'app/Views/header.php';
        },
        '/footer' => function() {
            include App::$APP_PATH.'app/Views/footer.php';
        },
        '/cars/brands' => function() {
            sendResponse(CarBrandController::getAllBrands());
        },
        '/cars/{brandId}/types' => function($params) {
            sendResponse(CarTypeController::specificType($params[0]));
        },
        '/services' => function() {
            sendResponse(ServiceController::allService());
        },
    ],
    'POST' => [
        '/users/{userId}/cars' => function($params) {
                sendResponse(CarController::createCar($params));
        },
        '/users' => function($params) {
            sendResponse(UserController::createUser($params));
        },
        '/auth/login' => function($params) {
            sendResponse(UserController::loginUser($params));
        },
        '/auth/logout' => function() {
            sendResponse(["success" => true, "message" => "Sikeres kijelentkezés!"]);
        },
        '/users/{userId}/cars/{carId}/services' => function($params) {
            sendResponse(ServiceController::bookService($params));
        },

    ],
    'PUT' => [
        '/users/{userId}' => function($params){
            sendResponse(UserController::updateUser($params));
        },
        '/users/{userId}/cars/{carId}' => function($params){
            sendResponse(CarController::updateCar($params));
        },
        '/users/{userId}/cars/{licenseNumber}/services' => function($params){
            sendResponse(ServiceController::cancelService($params));
        }
    ],
    'DELETE' => [
        '/users/{userId}/cars/{carId}' => function($params){
            sendResponse(CarController::deleteCar($params));
        }
    ]
];

//Ha megfelelő API végpont érkezik, akkor a $routes tömbből meghívja a megfelelő függvényt. Ellenkező esetben 404-es hibakóddal tér vissza
$routeInfo = parseRoute($method, $exactUri, $routes);
if($routeInfo){
    $handler = $routeInfo['handler'];

    $handler($params);
}
else{
    sendResponse(["success" => false, "message" => "Nincs ilyen endpoint!", "code" => 404]);
}