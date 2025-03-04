<?php

namespace App\Models;
use Config\Database;

class Client{
    //Lekéri egy felhasználóhoz tartozó ügyfél összes adatát
    public static function single($userId){
        $db = Database::connect();
        $query = "SELECT * FROM ugyfelek AS u INNER JOIN felhasznalok_ugyfelek AS f_u ON u.ugyfel_id = f_u.ugyfel_id INNER JOIN felhasznalok AS f ON f_u.felhasznalo_id = f.felhasznalo_id WHERE f.felhasznalo_id = '$userId';";
        return $db->query($query)->fetch_assoc();
    }
}