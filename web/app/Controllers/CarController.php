<?php

namespace App\Controllers;
use App\Models\Car;

class CarController{
    public static function allCar(){
        $cars = Car::all();
        echo json_encode($cars);
    }

    public static function userCars(){
        $cars = Car::find($_SESSION['user']['username']);
        echo json_encode($cars);
        if(isset($_SESSION['user']['logged_in'])){
            $cars = Car::find($_SESSION['user']['username']);
            echo json_encode($cars);
        }
        else{
            echo json_encode(null);
        }
    }

    public static function createCar($data){
        return Car::create($data);
    }

    public static function updateCar($data){
        return Car::update($data);
    }
}