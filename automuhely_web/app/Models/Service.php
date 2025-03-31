<?php

namespace App\Models;
use Config\Database;
use Exception;

class Service{
    //Lekéri az összes szolgáltatást
    public static function all(){
        $db = Database::connect();
        $query = "SELECT * FROM szervizcsomagok;";
        $result = $db->query($query);
        $services = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $services;
    }

    //Lefoglal egy szolgáltatást
    public static function book($data){
        try{
            $service_id = (int)$data['service_id'];
            $car_id = (int)$data['car_id'];
    
            $db = Database::connect();
            $query = "INSERT INTO idopontfoglalasok (jarmu_id, csomag_id, allapot) VALUES ($car_id, $service_id, 'Foglalt');";
            $db->execute_query($query);
            if($db->affected_rows > 0){
                return ["success" => true, "message" => "Sikeres foglalás!", "code" => 201];
            }
            else{
                return ["success" => false, "message" => "Sikertelen foglalás!", "code" => 400];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }

    //Lemond egy szolgáltatást
    public static function cancel($data){
        try{
            $licenseNumber = $data["licenseNumber"];
            $serviceId = $data["serviceId"];

            $db = Database::connect();
            $query = "UPDATE idopontfoglalasok AS i INNER JOIN jarmuvek AS j ON i.jarmu_id = j.jarmu_id INNER JOIN szervizcsomagok AS szcs ON i.csomag_id = szcs.csomag_id SET i.allapot = 'Lemondva' WHERE (j.rendszam = '$licenseNumber' AND i.idopont_id = $serviceId AND i.allapot = 'Foglalt');";
            $db->execute_query($query);
            if($db->affected_rows > 0){
                return ['success' => true, 'message' => "Sikeresen lemondta a foglalást!", 'code' => 201];
            }
            else{
                return ['success' => false, 'message' => "Sikertelen lemondás!", 'code' => 400];
            }

        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }
}