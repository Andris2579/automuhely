<?php

use Config\App;

if($_SERVER['REQUEST_METHOD'] == 'GET'){
    $text='<img id="logoHamburger" src="'.App::$BASE_URL.'public/assets/imgs/logoX.png">
            <nav>
            <a id="services_page" href="'.App::$BASE_URL.'public/pages/services.html">Szolgáltatások</a>';
    if(isset($_SESSION['user']['logged_in']) && $_SESSION['user']['logged_in']){
        $text.='<a id="my_cars" href="'.App::$BASE_URL.'public/pages/cars.html">Autóim</a>';
    }
    $text.= '<a id="about_us" href="'.App::$BASE_URL.'public/pages/about_us.html">Rólunk</a>';
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
    $text.='</nav>';
    echo json_encode($text);
}