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

$routes = [
    'POST' => [
        'user' => [UserController::class, 'createUser', 'Sikeres regisztráció!', 'Sikertelen registráció!'],
        'auth/login' => [UserController::class, 'loginUser', 'Sikeres bejelentkezés!', 'Sikertelen bejelentkezés!'],
        'auth/logout' => function(){
            session_unset();
            session_destroy();
            sendResponse(["success" => true, "message" => "Sikeres kijelentkezés"], 200);
        },
        'userSettingsSave' => [UserController::class, 'updateUser'],
        'car' => [CarController::class, 'createCar', 'Az autó sikeresen létrehozva!', 'Az autó létrehozása nem sikerült.'],
    ],
    'GET' => [
        'header' => function(){
            include App::$APP_PATH.'app/Views/header.php';
        },
        'footer' => function(){
            include App::$APP_PATH.'app/Views/footer.php';
        },
        'cars' => [CarController::class, 'userCars'],
        'carBrand' => [CarBrandController::class, 'allBrand'],
        'carType' => [CarTypeController::class, 'specificType'],
        'services' => [ServiceController::class, 'allService'],
    ],
    'PUT' => [
        'cars' => [CarController::class, 'updateCar', 'Az autó sikeresen frissítve!', 'Az autó frissítése nem sikerült.']
    ]
];


if (isset($routes[$method])) {
    $matchedRoute = null;
    foreach ($routes[$method] as $route => $action) {
        $regex = preg_replace('#\{[a-zA-Z_]+\}#', '([a-zA-Z0-9_-]+)', $route);
        $regex = "#^{$regex}$#";

        if (preg_match($regex, $endpoint, $matches)) {
            $matchedRoute = $action;
            break;
        }
    }

    if ($matchedRoute) {
        if (is_callable($matchedRoute)) {
            $response = $matchedRoute($params);
            if (is_bool($response)) {
                if ($response) {
                    sendResponse(["success" => true, "message" => $routes[$method][$route][2]], 200);
                } else {
                    sendResponse(["success" => false, "message" => $routes[$method][$route][3]], 400);
                }
            }
        } elseif (is_array($matchedRoute)) {
            [$controller, $function, $successMessage, $failMessage] = $matchedRoute;
            if (class_exists($controller) && method_exists($controller, $function)) {
                $controllerInstance = new $controller();
                $response = call_user_func([$controllerInstance, $function], $params);
                if (is_bool($response)) {
                    if ($response) {
                        sendResponse(["success" => true, "message" => $successMessage], 200);
                    } else {
                        sendResponse(["success" => false, "message" => $failMessage], 400);
                    }
                }
            } else {
                sendResponse(["error" => "Invalid route action"], 500);
            }
        } else {
            sendResponse(["error" => "Unknown route handler"], 500);
        }
    } else {
        sendResponse(["error" => "Route not found"], 404);
    }
} else {
    sendResponse(["error" => "Method not allowed"], 405);
}


/*if (isset($routes[$method][$endpoint])) {
    $action = $routes[$method][$endpoint];
    if (is_callable($action)) {
        $response = $action($params);
        if (is_bool($response)) {
            if ($response) {
                sendResponse(["success" => true, "message" => $routes[$method][$endpoint][2]], 200);
            } else {
                sendResponse(["success" => false, "message" => $routes[$method][$endpoint][3]], 400);
            }
        }
    } elseif (is_array($action)) {
        [$controller, $function] = $action;
        if (class_exists($controller) && method_exists($controller, $function)) {
            $controllerInstance = new $controller();
            $response = $controllerInstance->$function($params);
            if (is_bool($response)) {
                if ($response) {
                    sendResponse(["success" => true, "message" => $routes[$method][$endpoint][2]], 200);
                } else {
                    sendResponse(["success" => false, "message" => $routes[$method][$endpoint][3]], 400);
                }
            }
        } else {
            sendResponse(["error" => "Invalid route action"], 500);
        }
    } else {
        sendResponse(["error" => "Unknown route handler"], 500);
    }
} else {
    sendResponse(["error" => "Route not found"], 404);
}*/
