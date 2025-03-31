<?php

namespace App\Controllers;
use App\Models\CarType;

class CarTypeController{
    //Visszaadja egy márka alá tartózó összes autótípust
    public static function specificType($data){
        return CarType::specific($data);
    }
}