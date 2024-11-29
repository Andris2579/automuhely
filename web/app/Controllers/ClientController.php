<?php

namespace App\Controllers;
use App\Models\Client;

class ClientController{
    public static function getClient($username){
        return Client::single($username);
    }
}