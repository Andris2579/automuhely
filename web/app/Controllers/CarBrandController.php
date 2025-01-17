<?php

namespace App\Controllers;
use App\Models\CarBrand;

class CarBrandController{
    public static function allBrand(){
        $brands = CarBrand::all();
        return $brands;
    }
}