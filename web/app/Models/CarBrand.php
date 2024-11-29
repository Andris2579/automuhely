<?php

namespace App\Models;
use Config\Database;

class CarBrand{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM marka;";
        $result = $db->query($query);
        $brands = $result->fetch_aLL(MYSQLI_ASSOC);
        return $brands;
    }
}