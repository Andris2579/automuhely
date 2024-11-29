<?php

namespace App\Models;
use Config\Database;

class Service{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM szervizcsomagok;";
        $result = $db->query($query);
        $services = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $services;
    }

    public static function find($id){
        $db = Database::connect();
        $query = "SELECT * FROM szervizcsomagok WHERE id = $id";
        $result = $db->query($query);
        $service = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $service;
    }
}