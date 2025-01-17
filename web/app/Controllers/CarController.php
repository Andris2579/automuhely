<?php

namespace App\Controllers;
use App\Models\Car;

class CarController{
    public static function allCar(){
        $cars = Car::all();
        return $cars;
    }

    public static function userCars(){
        if(isset($_SESSION['user']['logged_in'])){
            $cars = Car::find($_SESSION['user']['username']);
            return $cars;
        }
        else{
            return null;
        }
    }

    public static function createCar($data){
        return Car::create($data);
    }

    public static function updateCar($data){
        return Car::update($data);
    }
}