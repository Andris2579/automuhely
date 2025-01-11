<?php

namespace App\Controllers;
use App\Models\Service;

class ServiceController{
    public static function allService(){
        echo json_encode(Service::all());
    }

    public static function singleService($id){
        return Service::find($id);
    }

    public static function bookService($data){
        return Service::book($data);
    }
}