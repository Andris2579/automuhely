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
    
            $query = "UPDATE felhasznalok AS f INNER JOIN felhasznalok_ugyfelek ON f.felhasznalo_id = felhasznalok_ugyfelek.felhasznalo_id INNER JOIN ugyfelek AS u ON felhasznalok_ugyfelek.ugyfel_id = u.ugyfel_id SET u.nev = '$name', u.email = '$email', u.telefonszam = '$phone_number', u.cim = '$cim' WHERE f.felhasznalonev = '$username';";
            $db->query($query);

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

            if (!self::validateUsername($username)) { //Ellenőrzi, hogy nem e foglalt az adott felhasználónév
                try{
                    $query1 = "INSERT INTO felhasznalok (felhasznalonev, jelszo_hash, szerep) VALUES ('$username', SHA2('$password', 256), '$role');";
                    $db->execute_query($query1);
        
                    $felhasznalo_id = $db->insert_id;
        
                    $query2 = "INSERT INTO ugyfelek (nev, telefonszam, email) VALUES ('$name', '$phone_number', '$email');";
                    $db->execute_query($query2);
        
                    $ugyfel_id = $db->insert_id;
        
                    $query3 = "INSERT INTO felhasznalok_ugyfelek (felhasznalo_id, ugyfel_id) VALUES ('$felhasznalo_id', '$ugyfel_id');";
                    $db->execute_query($query3);
        
                    return ['success' => true, 'message' => "Sikeres regisztráció!", 'code' => 201];
                }
                catch(Exception $e){
                    return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
                }
            }
            else{
                return ['success' => false, 'message' => "Ez a felhasználónév már foglalt!", 'code' => 400];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }
    }

    //Ellenőrzi, hogy nem e foglalt az adott felhasználónév
    public static function validateUsername($username){
        $db = Database::connect();
        $query = "SELECT felhasznalonev FROM felhasznalok WHERE felhasznalonev = '$username';";
        $result = $db->query($query);
        return $result->num_rows > 0;
    }

    //Ellenőrzi, hogy a felhasználó helyes bejelentkezési adatokat adott e meg
    public static function checkUser($data){
        $db = Database::connect();
        $username = $data['username'];
        $password = $data['password'];
        $query = "SELECT * FROM felhasznalok WHERE felhasznalonev = '$username' AND jelszo_hash = SHA2('$password',256);";
        return $db->query($query)->num_rows > 0;
    }
}