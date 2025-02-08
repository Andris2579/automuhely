<?php

namespace App\Models;
use Config\Database;

class Client{
    public static function single($userId){
        $db = Database::connect();
        $query = "SELECT * FROM ugyfelek AS u INNER JOIN felhasznalok_ugyfelek AS f_u ON u.ugyfel_id = f_u.ugyfel_id INNER JOIN felhasznalok AS f ON f_u.felhasznalo_id = f.felhasznalo_id WHERE f.felhasznalo_id = '$userId';";
        $result = $db->query($query);
        $client = $result->fetch_all(MYSQLI_ASSOC);
        return $client;
    }
}