<?php

namespace App\Controllers;
use App\Models\Car;

class CarController{
    //Visszaadja a felhasználó egyetlen autójának adatait a rendszáma alapján
    public static function userCar($data){
        return Car::singleCar($data);
    }

    //Visszaadja a felhasználó összes autóját
    public static function userCars($userId){
        if(isset($userId)){
            $cars = Car::find($userId);
            return $cars;
        }
        else{
            return null;
        }
    }

    //Létrehoz a felhasználó számára egy autót a megadott adatok alapján
    public static function createCar($data){
        return Car::create($data);
    }

    //Frissíti a felhasználó egy adott autójának rendszámát
    public static function updateCar($data){
        return Car::update($data);
    }

    //Kitörli a felhasználó egy autóját a rendszáma alapján
    public static function deleteCar($data){
        return Car::delete($data);
    }

    //Visszaadja a felhasználó egy adott autójához tartozó összes szolgáltatást
    public static function getCarServices($data){
        return Car::carServices($data);
    }
}