<?php
namespace App\Models;
use Config\Database;
use Exception;

class User{
    //Lekéri egy felhasználó összes adatát
    public static function find($data){
        $db = Database::connect();
        $username = $data;
        $userId = $data;
        $query = "SELECT f.felhasznalo_id, f.felhasznalonev, f.jelszo_hash, u.nev, u.telefonszam, u.cim, u.email FROM felhasznalok AS f INNER JOIN felhasznalok_ugyfelek AS f_u ON f.felhasznalo_id = f_u.felhasznalo_id INNER JOIN ugyfelek AS u ON f_u.ugyfel_id = u.ugyfel_id WHERE (f.felhasznalonev = '$username' OR f.felhasznalo_id = '$userId');";
        return $db->query($query)->fetch_assoc();
    }

    //Frissíti a felhasználó összes adatát
    public static function update($data){
        try{
            $db = Database::connect();
            $name = $data['name'];
            $username = $data['username'];
            $email = $data['email'];
            $phone_number = $data['phone_number'];
            $cim = $data['cim'];
            $password = $data['password'];
            
            $password = strlen($password) > 0 ? "SHA2('$password',256)" : 'f.jelszo_hash';
            $query = "UPDATE felhasznalok AS f INNER JOIN felhasznalok_ugyfelek ON f.felhasznalo_id = felhasznalok_ugyfelek.felhasznalo_id INNER JOIN ugyfelek AS u ON felhasznalok_ugyfelek.ugyfel_id = u.ugyfel_id SET u.nev = ?, u.email = ?, u.telefonszam = ?, u.cim = ?, f.jelszo_hash = ".$password." WHERE f.felhasznalonev = '$username';";
            $stmt = $db->prepare($query);
            $stmt->bind_param("ssss", $name, $email, $phone_number, $cim);
            $stmt->execute();

            return ['success' => true, 'message' => 'Sikeresen fissítette a felhasználói adatokat!', 'code' => 200];
        }
        catch(Exception $e){
            return ['success' => false, 'message' => $e->getMessage(), 'code' => 500];
        }
    }

    //Létrehozza a felhasználót, regisztrálja
    public static function create($data){
        try{
            $username = $data['username'];
            $password = $data['password'];
            $role = "Ügyfél";
            $name = $data['name'];
            $phone_number = $data['phone_number'];
            $email = $data['email'];

            $db = Database::connect();

            if (!self::validateUsername($username) && !self::validateEmail($email)) { //Ellenőrzi, hogy nem e foglalt az adott felhasználónév
                try{
                    $query1 = "INSERT INTO felhasznalok (felhasznalonev, jelszo_hash, szerep) VALUES (?, SHA2(?, 256), '$role');";
                    $stmt = $db->prepare($query1);
                    $stmt->bind_param("ss", $username, $password);
                    $stmt->execute();
        
                    $felhasznalo_id = $db->insert_id;
        
                    $query2 = "INSERT INTO ugyfelek (nev, telefonszam, email) VALUES (?, ?, ?);";
                    $stmt = $db->prepare($query2);
                    $stmt->bind_param("sss", $name, $phone_number, $email);
                    $stmt->execute();
        
                    $ugyfel_id = $db->insert_id;
        
                    $query3 = "INSERT INTO felhasznalok_ugyfelek (felhasznalo_id, ugyfel_id) VALUES (?, ?);";
                    $stmt = $db->prepare($query3);
                    $stmt->bind_param("ii", $felhasznalo_id, $ugyfel_id);
                    $stmt->execute();
        
                    return ['success' => true, 'message' => "Sikeres regisztráció!", 'code' => 201];
                }
                catch(Exception $e){
                    return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
                }
            }
            else if(self::validateUsername($username)){
                return ['success' => false, 'message' => "Ez a felhasználónév már foglalt!", 'code' => 400];
            }
            else if(self::validateEmail($email)){
                return ['success' => false, 'message' => "Ez az email cím már használatban van!", 'code' => 400];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }

    //Ellenőrzi, hogy nem e foglalt az adott felhasználónév
    public static function validateUsername($username){
        $db = Database::connect();
        $query = "SELECT felhasznalonev FROM felhasznalok WHERE felhasznalonev = ?;";

        $stmt = $db->prepare($query);
        $stmt->bind_param("s", $username);
        $stmt->execute();
        $stmt->store_result();
        return $stmt->num_rows > 0;
    }

    //Ellenőrzi, hogy nem e foglalt az adott felhasználónév
    public static function validateEmail($email){
        $db = Database::connect();
        $query = "SELECT email FROM ugyfelek WHERE email = ?;";

        $stmt = $db->prepare($query);
        $stmt->bind_param("s", $email);
        $stmt->execute();
        $stmt->store_result();
        return $stmt->num_rows > 0;
    }

    //Ellenőrzi, hogy a felhasználó helyes bejelentkezési adatokat adott e meg
    public static function checkUser($data){
        $db = Database::connect();
        $username = $data['username'];
        $password = $data['password'];
        $query = "SELECT * FROM felhasznalok WHERE felhasznalonev = ? AND jelszo_hash = SHA2(?,256);";

        $stmt = $db->prepare($query);
        $stmt->bind_param("ss", $username, $password);
        $stmt->execute();
        $stmt->store_result();
        return $stmt->num_rows > 0;
    }
}