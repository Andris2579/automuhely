<?php

namespace App\Controllers;
use App\Models\CarBrand;

class CarBrandController{
    //Visszaadja az összes autómárkát
    public static function getAllBrands(){
        return CarBrand::all();
    }
}