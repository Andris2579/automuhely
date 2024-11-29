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
    }

    public static function createCar($data){
        return Car::create($data);
    }

    public static function updateCar($data){
        return Car::update($data);
    }
}