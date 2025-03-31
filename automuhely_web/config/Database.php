<?php

namespace Config;

use mysqli;

class Database{
    //Az alábbi adatok szükségesek a megfelelő adatbázis kapcsolat létrehozásához
    private static $host = "localhost";
    private static $username = "root";
    private static $password = "";
    private static $database = "automuhely";

    public static function connect(){
        return new mysqli(self::$host, self::$username, self::$password, self::$database);
    }
}