<?php

namespace App\Controllers;
use App\Models\CarType;

class CarTypeController{
    public static function allType(){
        $types = CarType::all();
        echo json_encode($types);
    }

    public static function specificType($data){
        $types = CarType::specific($data);
        echo json_encode($types);
    }
}