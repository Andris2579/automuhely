<?php

namespace App\Models;
use Config\Database;

class CarType{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM tipus";
        $result = $db->query($query);
        $types = $result->fetch_all(MYSQLI_ASSOC);
        return $types;
    }

    public static function specific($data){
        $brand = $data;
        $db = Database::connect();
        $query = "SELECT tipus_id, tipus FROM tipus WHERE marka_id = '$brand'";
        $result = $db->query($query);
        $types = $result->fetch_all(MYSQLI_ASSOC);
        return $types;
    }

    public static function singleType($brand, $type){
        $db = Database::connect();
        $query = "SELECT * FROM tipus WHERE marka_id = $brand AND tipus_id = $type";
        $result = $db->query($query);
        $types = $result->fetch_all(MYSQLI_ASSOC);

        return $types;
    }
}