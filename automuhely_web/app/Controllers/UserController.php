<?php

namespace App\Controllers;
use App\Models\User;
use Config\App;
use Exception;
use Firebase\JWT\JWT;

class UserController{
    //Visszaadja egy adott felhasználó összes adatát
    public static function singleUser($userId){
        return User::find($userId);
    }

    //Létrehoz egy felhasználót
    public static function createUser($data){
        return User::create($data);
    }

    //Bejelentkezteti a felhasználót
    public static function loginUser($data){
        try{
            if(User::checkUser($data)){ //Ellenőrzi, hogy a felhasználó bejelentkezési adatai helyesek e
                try{
                    $userId = User::find($data['username'])['felhasznalo_id'];

                    //Összeállítja a JSON Web Token elküldendő adatait
                    $payload = [
                        'userId' => $userId,
                        'username' => $data['username'],
                        'iat' => time(),
                        'exp' => time() + (60 * 60)
                    ];
                    
                    //Legenerálja a JSON Web Token-t
                    $jwt = JWT::encode($payload, App::$JWT_SECRET, 'HS256');
            
                    return ['success' => true, 'message' => "Sikeres bejelentkezés!", 'code' => 200, 'token' => $jwt];
                }
                catch(Exception $e){
                    return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
                }
            }
            else{
                return ['success' => false, 'message' => "Hibás felhasználónév vagy jelszó!", 'code' => 404];
            }
        }
        catch(Exception $e){
            return ['success' => null, 'message' => $e->getMessage(), 'code' => 500];
        }

    }
    
    //Frissíti a felhasználó összes adatát
    public static function updateUser($data){
        return User::update($data);
    }
}