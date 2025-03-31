<?php

use Config\App;

//Felépíti a fejléc tartalmát
$text='<a href="'.App::$BASE_URL.'public/index.html"><img id="logo" src="'.App::$BASE_URL.'public/assets/imgs/logoX.png"></a>
        <p class="websiteTitle">Autóműhely</p>
        <img id="logoHamburger" src="'.App::$BASE_URL.'public/assets/imgs/hamburger.png">
        <nav>
        <div id="navLinks">
            <a id="services_page" href="'.App::$BASE_URL.'public/pages/services.html">Szolgáltatások</a>';

if(isset($_GET['userId'])){ //Ha bejelentkezett a felhasználó, akkor megjelenik az Autóim menüpont
    $text.='<a id="my_cars" href="'.App::$BASE_URL.'public/pages/cars.html">Autóim</a>';
}

$text.= '</div>';

if(isset($_GET['userId'])){ //Ha bejelentkezett a felhasználó, akkor megjelenik a profil menü, és a hozzá tartozó gombok is
    $text.='<div id="profileMenu">
                <button id="profile" class="customButton">';
    $text .= $_GET['username'];
    $text .= '</button>
                <div id="extraButtons">
                    <button class="customButton" id="userSettingsPageLoad">Profil beállítások</button>
                    <button class="customButton" id="logout" onclick=logout(event)>Kijelentkezés</button>
                </div>';
    $text .= '</div>';
}
else{ //A nem bejelentkezett felhasználóknak megjelenik a Regisztráció és Bejelentkezés gomb
    $text.='<a id="register" href="'.App::$BASE_URL.'public/pages/register.html">Regisztráció</a>
            <a id="login" href="'.App::$BASE_URL.'public/pages/login.html">Bejelentkezés</a>';
}

$text.='</nav>';

echo json_encode($text);