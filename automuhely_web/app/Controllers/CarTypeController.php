<?php

namespace App\Controllers;
use App\Models\CarType;

class CarTypeController{
    public static function allType(){
        $types = CarType::all();
        return $types;
    }

    public static function specificType($data){
        return CarType::specific($data);
    }
}