<?php

namespace App\Controllers;
use App\Models\User;

class UserController{
    public static function allUser(){
        $users = User::all();
        echo json_encode($users);
    }

    public static function singleUser($username){
        return User::find($username);
    }

    public static function createUser($data){
        return User::create($data);
    }

    public static function loginUser($data){
        $result = User::checkUser($data);
        if($result){
            $username = $data['username'];
            $_SESSION['user'] = ['username' => $username, 'logged_in' => true];
            return ["success" => true, "message" => "Sikeres bejelentkezés!"];
        }
        else{
            return ["success" => false, "message" => "Sikertelen bejelentkezés!"];
        }
    }

    public static function updateUser($data){
        return User::update($data);
    }

    public static function userLoggedIn(){
        return isset($_SESSION['user']);
    }
}