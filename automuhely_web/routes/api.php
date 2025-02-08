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
use App\Models\User;

$method = $_SERVER['REQUEST_METHOD'];
$requestUri = explode('?', $_SERVER['REQUEST_URI'])[0];
$uriSegments = explode('/', trim($requestUri, '/'));

$params = [];

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

$baseIndex = array_search('api.php',$uriSegments);
$exactUri = array_slice($uriSegments, $baseIndex + 1);

foreach ($exactUri as $segment) {
    if (is_numeric($segment)) {
        $params[] = (int) $segment;
    }
}

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

function sendResponse($data) {
    http_response_code($data['code'] ?? 200);
    echo json_encode($data);
    exit;
}

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
        '/header' => function() {
            include App::$APP_PATH.'app/Views/header.php';
        },
        '/footer' => function() {
            include App::$APP_PATH.'app/Views/footer.php';
        },
        '/cars/brands' => function() {
            sendResponse(CarBrandController::allBrand());
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
        }
    ],
    'DELETE' => [],
];

$routeInfo = parseRoute($method, $exactUri, $routes);
if($routeInfo){
    $handler = $routeInfo['handler'];

    $handler($params);
}
else{
    sendResponse(["success" => false, "message" => "Nincs ilyen endpoint!", "code" => 404]);
}