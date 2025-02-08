<?php

namespace App\Models;
use Config\Database;
use App\Models\Client;
use App\Models\CarType;
use Exception;

class Car{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT rendszam, gyartas_eve, motor_adatok, alvaz_adatok, elozo_javitasok FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.helhasznalo_id = f_u.felhasznalo_id";
        $result = $db->query($query);
        $users = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $users;
    }

    public static function singleCar($data){
        $db = Database::connect();
        $query = "SELECT * FROM jarmuvek WHERE rendszam = '".$data['licenseNumber']."';";
        $result = $db->query($query);
        $car = $result->fetch_assoc();
        return $car;
    }

    public static function find($userId){
        $db = Database::connect();
        $query = "SELECT j.jarmu_id, j.rendszam, j.gyartas_eve, j.motor_adatok, j.alvaz_adatok, j.elozo_javitasok, t.tipus, i.allapot FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id INNER JOIN tipus AS t ON t.tipus_id = j.tipus_id LEFT JOIN idopontfoglalasok AS i ON i.jarmu_id = j.jarmu_id WHERE f.felhasznalo_id = '$userId';";
        $result = $db->query($query);
        $car = $result->fetch_all(MYSQLI_ASSOC);
        return $car;
    }

    public static function update($data){
        try{
            $db = Database::connect();
            
            $carId = $data["carId"];
            $newLicenseNumber = $data['newLicense'];

            if(self::validateCar($newLicenseNumber) == 0){
                $query = "UPDATE jarmuvek SET rendszam = '$newLicenseNumber' WHERE jarmu_id = $carId;";
                $db->query($query);
                return ['success' => true, 'message' => "Sikeresen módosította autója rendszámát!", 'code' => 200];
            }
            else{
                return ['success' => false, 'message' => "Ilyen rendszám már létezik!", 'code' => 400];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }

    public static function create($data){
        try{
            $userId = $data['userId'];
            $rendszam = $data['rendszam'];
            $elozo_javitasok = $data['elozo_javitasok'];
            $tipus = (int)$data['tipus'];
            $marka = (int)$data['marka'];
    
            if(self::validateCar($rendszam) == 0){
                try{
                    $client = Client::single($userId);
                    $client_id = $client[0]['ugyfel_id'];
                    $car = CarType::singleType($marka, $tipus);
                    $car_id = $car[0]['tipus_id'];
                    $db = Database::connect();
        
                    $query = "INSERT INTO jarmuvek (rendszam, tipus_id, elozo_javitasok) VALUES ('$rendszam', '$car_id', '$elozo_javitasok');";
                    $db->query($query);
        
                    $query = "INSERT INTO ugyfel_jarmuvek (ugyfel_id, jarmu_id) VALUES ('$client_id', (SELECT jarmu_id FROM jarmuvek WHERE rendszam = '$rendszam'));";
                    $db->query($query);
    
                    return ["success" => true, "message" => "Sikeres autó hozzáadás!", 'code' => 201];
                }
                catch(Exception $e){
                    return ["success" => null, "message" => $e->getMessage(), 'code' => 500];
                }
            }
            else{
                return ["success" => false, "message" => "Ilyen rendszámú autó már létezik!", 'code' => 400];
            }
        }
        catch(Exception $e){
            return ["success" => null, "message" => $e->getMessage(), 'code' => 500];
        }
    }

    public static function validateCar($rendszam){
        $db = Database::connect();
        $query = "SELECT j.rendszam FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id WHERE j.rendszam = '$rendszam'";
        $result = $db->query($query);
        return $result->num_rows;
    }

    public static function serviceState($rendszam){
        $db = Database::connect();
        $query = "SELECT i.allapot FROM jarmuvek AS j INNER JOIN idopontfoglalasok AS i ON j.jarmu_id = i.jarmu_id WHERE j.rendszam = '$rendszam';";
        $result = $db->query($query);
        return $result->fetch_all(MYSQLI_ASSOC);
    }
}