<?php

namespace App\Models;
use Config\Database;
use App\Models\Client;
use App\Models\CarType;
use Exception;

class Car{
    //Lekéri egy autó összes adatát
    public static function singleCar($data){
        $db = Database::connect();

        $query = "SELECT j.jarmu_id, j.rendszam, j.gyartas_eve, j.motor_adatok, j.alvaz_adatok, j.elozo_javitasok, t.tipus FROM ugyfel_jarmuvek AS u_j INNER JOIN jarmuvek AS j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id INNER JOIN tipus AS t ON t.tipus_id = j.tipus_id WHERE j.rendszam = '".$data['licenseNumber']."';";
        $result = $db->query($query);
        $car = $result->fetch_assoc();

        return $car;
    }

    //Lekéri egy autóhoz tartozó összes szolgáltatást
    public static function carServices($data){
        $db = Database::connect();
        $query = "SELECT i.idopont_id, szcs.nev, i.allapot FROM jarmuvek AS j INNER JOIN idopontfoglalasok AS i ON j.jarmu_id = i.jarmu_id INNER JOIN szervizcsomagok AS szcs ON i.csomag_id = szcs.csomag_id WHERE j.rendszam = '".$data['licenseNumber']."';";
        return $db->query($query)->fetch_all(MYSQLI_ASSOC);
    }

    //Lekéri egy autó összes adatát, és a hozzá tartozó összes szolgáltatást
    public static function find($userId){
        $db = Database::connect();
        $query = "SELECT j.jarmu_id, j.rendszam, j.gyartas_eve, j.motor_adatok, j.alvaz_adatok, j.elozo_javitasok, t.tipus, m.marka_neve FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id INNER JOIN ugyfelek AS u ON u.ugyfel_id = u_j.ugyfel_id INNER JOIN felhasznalok_ugyfelek AS f_u ON f_u.ugyfel_id = u.ugyfel_id INNER JOIN felhasznalok AS f ON f.felhasznalo_id = f_u.felhasznalo_id INNER JOIN tipus AS t ON t.tipus_id = j.tipus_id INNER JOIN marka AS m ON t.marka_id = m.marka_id WHERE f.felhasznalo_id = $userId GROUP BY j.rendszam;";
        $result = $db->query($query)->fetch_all(MYSQLI_ASSOC);

        $response['cars'] = [];
        $response['services'] = [];

        foreach ($result as $car) {
            $response['cars'][$car['rendszam']] = $car;
        }

        $carsServices = [];

        foreach ($response['cars'] as $car) {
            $query = "SELECT szcs.nev, i.allapot FROM jarmuvek AS j INNER JOIN idopontfoglalasok AS i ON j.jarmu_id = i.jarmu_id INNER JOIN szervizcsomagok AS szcs ON i.csomag_id = szcs.csomag_id WHERE j.rendszam = '".$car['rendszam']."'AND i.allapot = 'Foglalt';";
        
            $carServices = $db->query($query)->fetch_all(MYSQLI_ASSOC);
        
            if (!isset($carsServices[$car['rendszam']])) {
                $carsServices[$car['rendszam']] = [];
            }
        
            foreach ($carServices as $service) {
                $carsServices[$car['rendszam']][] = $service;
            }
        }

        $response['services'] = $carsServices;

        return $response;
    }

    //Frissíti egy autó rendszámát
    public static function update($data){
        try{
            $db = Database::connect();
            
            $carId = $data["carId"];
            $newLicenseNumber = $data['newLicense'];

            if(self::validateCar($newLicenseNumber) == 0){ //Ellenőrzi, hogy nincs e még egy azonos rendszám az adatbázisban
                $query = "UPDATE jarmuvek SET rendszam = ? WHERE jarmu_id = $carId;";
                $stmt = $db->prepare($query);
                $stmt->bind_param("s", $newLicenseNumber);
                $stmt->execute();
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

    //Létrehoz egy új autót a felhasználó számára
    public static function create($data){
        try{
            $userId = $data['userId'];
            $licenseNumber = $data['rendszam'];
            $previous_fixes = $data['elozo_javitasok'];
            $type = (int)$data['tipus'];
            $brand = (int)$data['marka'];
    
            if(self::validateCar($licenseNumber) == 0){ //Ellenőrzi, hogy nincs e még egy azonos rendszám az adatbázisban
                try{
                    $db = Database::connect();
                    $query = "SELECT * FROM ugyfelek AS u INNER JOIN felhasznalok_ugyfelek AS f_u ON u.ugyfel_id = f_u.ugyfel_id INNER JOIN felhasznalok AS f ON f_u.felhasznalo_id = f.felhasznalo_id WHERE f.felhasznalo_id = '$userId';";
                    $client =  $db->query($query)->fetch_assoc();
                    $client_id = $client['ugyfel_id'];

                    $car = CarType::singleType($brand, $type);
                    $car_id = $car['tipus_id'];
        
                    $query = "INSERT INTO jarmuvek (rendszam, tipus_id, elozo_javitasok) VALUES (?, '$car_id', ?);";
                    $stmt = $db->prepare($query);
                    $stmt->bind_param("ss", $licenseNumber, $previous_fixes);
                    $stmt->execute();
        
                    $query = "INSERT INTO ugyfel_jarmuvek (ugyfel_id, jarmu_id) VALUES ('$client_id', (SELECT jarmu_id FROM jarmuvek WHERE rendszam = ?));";
                    $stmt = $db->prepare($query);
                    $stmt->bind_param("s", $licenseNumber);
                    $stmt->execute();
    
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

    //Ellenőrzi egy adott rendszámmal rendelkező autók számát
    public static function validateCar($licenseNumber){
        $db = Database::connect();
        $query = "SELECT j.rendszam FROM jarmuvek AS j WHERE j.rendszam = '$licenseNumber'";
        $result = $db->query($query);
        return $result->num_rows;
    }

    //Kitöröli a felhasználó egy autóját
    public static function delete($data){
        $rendszam = $data['carId'];
        try{
            $db = Database::connect();
            $query = "SELECT * FROM idopontfoglalasok AS i INNER JOIN jarmuvek AS j ON i.jarmu_id = j.jarmu_id WHERE (j.rendszam = '$rendszam' AND i.allapot != 'Befejezett' AND i.allapot != 'Lemondva');";
            $result = $db->query($query);
            if($result->num_rows > 0){
                return ['success' => false, 'message' => "Nem lehet törölni az autót, mert aktív szerviz időpontja van!", 'code' => 400];
            }
            else{
                $query = "DELETE u_j FROM jarmuvek AS j INNER JOIN ugyfel_jarmuvek AS u_j ON j.jarmu_id = u_j.jarmu_id WHERE j.rendszam = '$rendszam';";
                $db->query($query);
                return ['success' => true, 'message' => "Sikeres törlés!", 'code' => 200];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }
}