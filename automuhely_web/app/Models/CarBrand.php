<?php

namespace App\Models;
use Config\Database;

class CarBrand{
    //Lekéri az összes márkát
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM marka;";
        return $db->query($query)->fetch_aLL(MYSQLI_ASSOC);
    }
}