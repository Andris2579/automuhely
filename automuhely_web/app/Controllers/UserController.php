<?php

namespace App\Controllers;
use App\Models\User;
use Config\App;
use Exception;
use Firebase\JWT\JWT;

class UserController{
    public static function singleUser($userId){
        return User::find($userId);
    }

    public static function createUser($data){
        return User::create($data);
    }

    public static function loginUser($data){
        try{
            $result = User::checkUser($data);
            if($result){
                try{
                    $userId = User::find($data['username'])['felhasznalo_id'];
                    $payload = [
                        'userId' => $userId,
                        'username' => $data['username'],
                        'iat' => time(),
                        'exp' => time() + (60 * 60)
                    ];
        
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

    public static function updateUser($data){
        return User::update($data);
    }
}