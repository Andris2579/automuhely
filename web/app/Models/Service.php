<?php

namespace App\Models;
use Config\Database;
use DateTime;

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

    public static function book($data){
        $service_id = (int)$data['service_id'];
        $car_id = (int)$data['car_id'];
        $date = new DateTime($data['date']);
        $date = $date->format('Y-m-d');

        $db = Database::connect();
        $query = "INSERT INTO idopontfoglalasok (jarmu_id, csomag_id, idopont, allapot) VALUES ($car_id, $service_id, $date, 'Foglalt');";
        $db->execute_query($query);
        if($db->affected_rows > 0){
            return true;
        }
        else{
            return false;
        }
    }
}