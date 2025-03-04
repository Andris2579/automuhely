<?php

namespace App\Controllers;
use App\Models\Service;

class ServiceController{
    //Visszaadja az összes szolgáltatást
    public static function allService(){
        return Service::all();
    }

    //Lefoglal egy adott szolgáltatást a felhasználó egy adott autójához
    public static function bookService($data){
        return Service::book($data);
    }

    //Lemond egy adott szolgáltatást a felhasználó egy adott autójához
    public static function cancelService($data){
        return Service::cancel($data);
    }
}