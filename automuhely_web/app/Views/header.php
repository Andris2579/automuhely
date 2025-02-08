<?php

use Config\App;

if($_SERVER['REQUEST_METHOD'] == 'GET'){
    $text='<a href="'.App::$BASE_URL.'public/index.html"><img id="logo" src="'.App::$BASE_URL.'public/assets/imgs/logoX.png"></a>
            <img id="logoHamburger" src="'.App::$BASE_URL.'public/assets/imgs/hamburger.png">
            <nav>
            <a id="home" href="'.App::$BASE_URL.'public/index.html">Fő oldal</a>
            <a id="services_page" href="'.App::$BASE_URL.'public/pages/services.html">Szolgáltatások</a>';
    if(isset($_GET['userId'])){
        $text.='<a id="my_cars" href="'.App::$BASE_URL.'public/pages/cars.html">Autóim</a>';
    }
    $text.= '<a id="about_us" href="'.App::$BASE_URL.'public/pages/about_us.html">Rólunk</a>';
    if(isset($_GET['userId'])){
        $text.='<div id="profileMenu">
                    <button id="profile">';
        $text .= $_GET['username'];
        $text .= '</button>
                    <br>
                    <div id="extraButtons">
                        <button id="userSettingsPageLoad">Profil beállítások</button>
                        <button id="logout" onclick=logout(event)>Kijelentkezés</button>
                    </div>';
        $text .= '</div>';
    }
    else{
        $text.='<a id="register" href="'.App::$BASE_URL.'public/pages/register.html">Regisztráció</a>
                <a id="login" href="'.App::$BASE_URL.'public/pages/login.html">Bejelentkezés</a>';
    }
    $text.='</nav>';
    echo json_encode($text);
}