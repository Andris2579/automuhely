<?php

namespace App\Controllers;
use App\Models\Car;

class CarController{
    public static function userCar($data){
        return Car::singleCar($data);
    }
    public static function userCars($userId){
        if(isset($userId)){
            $cars = Car::find($userId);
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