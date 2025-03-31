<?php

namespace App\Models;
use Config\Database;

class CarType{
    //Lekéri egy adott márka alá tartozó autótípusokat
    public static function specific($data){
        $brand = $data;
        $db = Database::connect();
        $query = "SELECT tipus_id, tipus FROM tipus WHERE marka_id = '$brand'";
        return $db->query($query)->fetch_all(MYSQLI_ASSOC);
    }

    //Lekéri egy adott autótípus összes adatát
    public static function singleType($brand, $type){
        $db = Database::connect();
        $query = "SELECT * FROM tipus WHERE marka_id = $brand AND tipus_id = $type";
        return $db->query($query)->fetch_assoc();
    }
}