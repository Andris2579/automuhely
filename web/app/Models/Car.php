<?php

namespace App\Models;
use Config\Database;
use App\Models\Client;
use App\Models\CarType;

class Car{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT rendszam, gyartas_eve, motor_adatok, alvaz_adatok, elozo_javitasok FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.helhasznalo_id = f_u.felhasznalo_id";
        $result = $db->query($query);
        $users = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $users;
    }

    public static function find($username){
        $db = Database::connect();
        $query = "SELECT j.jarmu_id, j.rendszam, j.gyartas_eve, j.motor_adatok, j.alvaz_adatok, j.elozo_javitasok, t.tipus, i.allapot FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id INNER JOIN tipus AS t ON t.tipus_id = j.tipus_id LEFT JOIN idopontfoglalasok AS i ON i.jarmu_id = j.jarmu_id WHERE f.felhasznalonev = '$username';";
        $result = $db->query($query);
        $car = $result->fetch_all(MYSQLI_ASSOC);
        return $car;
    }

    public static function update($data){
        $db = Database::connect();

        $username = $data['username'];
        $rendszam = $data['rendszam'];
        $elozo_javitasok = $data['elozo_javitasok'];
        $tipus = $data['tipus'];

        $query = "UPDATE jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.helhasznalo_id = f_u.felhasznalo_id INNER JOIN tipus AS t ON t.tipus_id = j.tipus_id SET j.rendszam = IF('$rendszam' = '', j.rendszam, '$rendszam'), j.elozo_javitasok = IF('$elozo_javitasok' = '', j.elozo_javitasok, '$elozo_javitasok'), t.tipus = IF('$tipus' = '', t.tipus, '$tipus') WHERE f.felhasznalonev = '$username';";
        return $db->query($query);
    }

    public static function create($data){
        $username = $_SESSION['user']['username'];
        $rendszam = $data['rendszam'];
        $elozo_javitasok = $data['elozo_javitasok'];
        $tipus = (int)$data['tipus'];
        $marka = (int)$data['marka'];

        if(self::validateCar($rendszam)){
            $client = Client::single($username);
            $client_id = $client[0]['ugyfel_id'];
            $car = CarType::singleType($marka, $tipus);
            $car_id = $car[0]['tipus_id'];
            $db = Database::connect();

            $query = "INSERT INTO jarmuvek (rendszam, tipus_id, elozo_javitasok) VALUES ('$rendszam', '$car_id', '$elozo_javitasok');";
            $db->query($query);

            $query = "INSERT INTO ugyfel_jarmuvek (ugyfel_id, jarmu_id) VALUES ('$client_id', (SELECT jarmu_id FROM jarmuvek WHERE rendszam = '$rendszam'));";
            $db->query($query);

            return true;
        }
        else{
            return false;
        }
    }

    public static function validateCar($rendszam){
        $db = Database::connect();
        $query = "SELECT j.rendszam FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id WHERE j.rendszam = '$rendszam'";
        $result = $db->query($query);
        if($result->num_rows > 0){
            return false;
        }
        else{
            return true;
        }
    }

    public static function serviceState($rendszam){
        $db = Database::connect();
        $query = "SELECT i.allapot FROM jarmuvek AS j INNER JOIN idopontfoglalasok AS i ON j.jarmu_id = i.jarmu_id WHERE j.rendszam = '$rendszam';";
        $result = $db->query($query);
        return $result->fetch_all(MYSQLI_ASSOC);
    }
}