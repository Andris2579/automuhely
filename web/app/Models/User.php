<?php
namespace App\Models;
use Config\Database;

class User{
    public static function all(){
        $db = Database::connect();
        $query = "SELECT f.felhasznalo_id, f.felhasznalonev, f.jelszo_hash, f.szerep, u.nev, u.elerhetoseg, u.cim FROM felhasznalok AS f INNER JOIN felhasznalok_ugyfelek ON f.felhasznalo_id = felhasznalok_ugyfelek.felhasznalo_id INNER JOIN ugyfelek AS u ON felhasznalok_ugyfelek.ugyfel_id = u.ugyfel_id;";
        $result = $db->query($query);
        $users = $result->fetch_all(MYSQLI_ASSOC);
        $result->free();
        return $users;
    }

    public static function find($username){
        $db = Database::connect();
        $query = "SELECT f.felhasznalo_id, f.felhasznalonev, f.jelszo_hash, f.szerep, u.nev, u.telefonszam, u.cim, u.email FROM felhasznalok AS f INNER JOIN felhasznalok_ugyfelek ON f.felhasznalo_id = felhasznalok_ugyfelek.felhasznalo_id INNER JOIN ugyfelek AS u ON felhasznalok_ugyfelek.ugyfel_id = u.ugyfel_id WHERE felhasznalonev = '$username'";
        $result = $db->query($query);
        $user = $result->fetch_assoc();
        return $user;
    }

    public static function update($data){
        $db = Database::connect();
        $username = $data['username'];
        $email = $data['email'];
        $password = $data['password'];
        $phone_number = $data['phone_number'];
        $cim = $data['cim'];

        $query = "UPDATE felhasznalok AS f INNER JOIN felhasznalok_ugyfelek ON f.felhasznalo_id = felhasznalok_ugyfelek.felhasznalo_id INNER JOIN ugyfelek AS u ON felhasznalok_ugyfelek.ugyfel_id = u.ugyfel_id SET f.jelszo_hash = IF('$password' = '', f.jelszo_hash, PASSWORD('$password')), u.email = IF('$email' = '', u.email, '$email'), u.telefonszam = IF('$phone_number' = '', u.telefonszam, '$phone_number'), u.cim = IF('$cim' = '', u.cim, '$cim') WHERE f.felhasznalonev = '$username'";
        return $db->query($query);
    }

    public static function create($data){
        $db = Database::connect();
        $username = $data['username'];
        $password = $data['password'];
        $password_again = $data['password_again'];
        $role = "Ügyfél";
        $name = $data['name'];
        $phone_number = $data['phone_number'];
        $email = $data['email'];
        if (!self::validateUsername($username)) {
            $query1 = "INSERT INTO felhasznalok (felhasznalonev, jelszo_hash, szerep) VALUES ('$username', SHA2('$password', 256)), '$role')";
            $db->execute_query($query1);

            $felhasznalo_id = $db->insert_id;

            $query2 = "INSERT INTO ugyfelek (nev, telefonszam, email) VALUES ('$name', '$phone_number', '$email')";
            $db->execute_query($query2);

            $ugyfel_id = $db->insert_id;

            $query3 = "INSERT INTO felhasznalok_ugyfelek (felhasznalo_id, ugyfel_id) VALUES ('$felhasznalo_id', '$ugyfel_id')";
            $db->execute_query($query3);

            $_SESSION['user'] = ['username' => $username, 'logged_in' => true];
            return "Sikeres registráció!";
        }
        else{
            return "Ilyen felhasználónév már létezik!";
        }
    }

    public static function validateUsername($username){
        $db = Database::connect();
        $query = "SELECT felhasznalonev FROM felhasznalok WHERE felhasznalonev = '$username';";
        $result = $db->query($query);
        return $result->num_rows > 0;
    }

    public static function checkUser($data){
        $db = Database::connect();
        $username = $data['username'];
        $password = $data['password'];
        $query = "SELECT * FROM felhasznalok WHERE felhasznalonev = '$username' AND jelszo_hash = PASSWORD('$password');";
        $result = $db->query($query);
        if($result->num_rows > 0){
            return true;
        }
        else{
            return false;
        }
    }
}