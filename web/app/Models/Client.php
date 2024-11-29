<?php

namespace App\Models;
use Config\Database;

class Client{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM ugyfelek;";
        $result = $db->query($query);
        $clients = $result->fetch_all(MYSQLI_ASSOC);
        return $clients;
    }

    public static function single($username){
        $db = Database::connect();
        $query = "SELECT * FROM ugyfelek AS u INNER JOIN felhasznalok_ugyfelek AS f_u ON u.ugyfel_id = f_u.ugyfel_id INNER JOIN felhasznalok AS f ON f_u.felhasznalo_id = f.felhasznalo_id WHERE f.felhasznalonev = '$username';";
        $result = $db->query($query);
        $client = $result->fetch_all(MYSQLI_ASSOC);
        return $client;
    }
}