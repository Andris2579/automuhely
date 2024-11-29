<?php

use Config\App;

if($_SERVER['REQUEST_METHOD'] == 'GET'){
    $text='<header>
            <nav>
            <a id="services" href="'.App::$BASE_URL.'public/pages/services.html">Szolgáltatások</a>';
    if(isset($_SESSION['user']['logged_in']) && $_SESSION['user']['logged_in']){
        $text.='<a id="my_cars" href="'.App::$BASE_URL.'public/pages/cars.html">Autóim</a>';
    }
    $text.= '<button id="about_us">Rólunk</button>';
    if(isset($_SESSION['user']['logged_in']) && $_SESSION['user']['logged_in']){
        $text.='<div>
                    <button id="profile" onclick=openProfileSmallMenu()>';
        $text .= $_SESSION['user']['username'];
        $text .= '</button>
                    <div id="extraButtons" class="hidden">
                        <button id="userSettingsPageLoad" onclick=userSettingsPageLoad()>Profil beállítások</button>
                        <button id="logout" onclick=logout(event)>Kijelentkezés</button>
                    </div>
                </div>';
    }
    else{
        $text.='<a id="register" href="'.App::$BASE_URL.'public/pages/register.html">Regisztráció</a>
                <a id="login" href="'.App::$BASE_URL.'public/pages/login.html">Bejelentkezés</a>';
    }
    $text.='</nav>
        </header>';
    echo json_encode($text);
}