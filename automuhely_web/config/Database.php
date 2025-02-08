<?php

namespace Config;

use mysqli;

class Database{
    private static $host = "localhost";
    private static $username = "root";
    private static $password = "";
    private static $database = "automuhely";
    private static $conn;

    public static function connect(){
        self::$conn = new mysqli(self::$host, self::$username, self::$password, self::$database);
        return self::$conn;
    }
}