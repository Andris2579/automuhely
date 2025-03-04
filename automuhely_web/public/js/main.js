import { register, login, logoutAuth, userSettingsSave } from './auth.js';

//A .env fájl tartalmához hasonlóan ezek is környezeti változók
export const BASE_URL = '/automuhely_web/';
export const APP_URL = 'C:/xampp/htdocs/automuhely/web/';

window.register = register;
window.login = login;
window.logout = logoutAuth;
window.userSettingsSave = userSettingsSave;

let token = null;

//Beállítja a JWT-t
export function setToken(newToken) {
    token = newToken;
    localStorage.setItem('authToken', newToken);
}

//Lekéri a JWT-t
export function getToken() {
    if (!token) {
        token = localStorage.getItem('authToken');
    }
    return token;
}

//Törli a JWT-t
export function clearToken() {
    token = null;
    localStorage.removeItem('authToken');
}

//Lekéri a JWT-ben tárolt felhasználó adatait
export function getUserCredentials() {
    const token = localStorage.getItem('authToken');
    if (token) {
        try {
            const decoded = jwt_decode(token);
            return {
                userId: decoded.userId,
                username: decoded.username
            };
        } catch (error) {
            console.error("Failed to decode token:", error);
            return null;
        }
    }
    return null;
}

$(document).ready(function () {
    //Ha a felhasználó már bejelentkezett, akkor ne legyen elérhető a a bejelentkezés és regisztráció oldal
    if((window.location == ("http://localhost" + BASE_URL + "public/index.html")) && getUserCredentials() != null){
        $('#main_message').find('#register_and_loginLink').remove();
        $('#servicesLink').html('Tekintse meg szolgáltatásainkat <a href="public/pages/services.html">itt</a>!');
    }
    if((window.location == ("http://localhost" + BASE_URL + "public/pages/login.html") || window.location == ("http://localhost" + BASE_URL + "public/pages/register.html")) && getUserCredentials() != null){
        openCustomModal("Hiba", "A felhasználó már regisztrált/bejelentkezett.");
        window.location = "http://localhost" + BASE_URL + "public/index.html";
    }

    //Betölti a fej- és láblécet
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/header",
        data: getUserCredentials(),
        dataType: "json",
        success: function (response) {
            $('header').html(response);
        }
    });
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/footer",
        dataType: "json",
        success: function (response) {
            $('footer').html(response);
        }
    });

    //Kis képernyőn megjelenő hambrger menü gombra kattintáskor megjelenik a navigációs menü
    $(document).on("click", "#logoHamburger", function(event) {
        const nav = $("nav");
    
        if (nav.css("display") === "none") {
            nav.fadeIn(600);
        }
        else {
            const extraButtons = $("#extraButtons");
            extraButtons.fadeOut(400);
            nav.fadeOut(400);
        }
    
        event.stopPropagation();
    });

    //Ha a navigációs menü látszik, és nem rá kattintunk, akkor eltűnik
    $(document).on("click", function(event) {
        const nav = $('nav');
        if($('#logoHamburger').css("display") === "block"){
            if (!$(event.target).closest("#logoHamburger, nav").length) {
                const extraButtons = $("#extraButtons");
                extraButtons.fadeOut(400);
                nav.fadeOut(400);
            }
        }
    });

    //A profil-ra kattintáskor megjelennek további, a felhasználó profiljához kapcsolódó gombok
    $(document).on("click", "#profile", function(event){
        const extraButtons = $("#extraButtons");
    
        if (extraButtons.css("display") === "none") {
            extraButtons.fadeIn(600);
            extraButtons.css("display", "flex");
        }
        else {
            extraButtons.fadeOut(400);
        }
    
        event.stopPropagation();
    });

    //Ha a felhasználó profil plusz gombjai látszanak, és nem rájuk kattintunk, akkor eltűnnek
    $(document).on("click", function(event) {
        const extraButtons = $('#extraButtons');
        if (extraButtons.css("display") !== "none") {
            if (!$(event.target).closest("#extraButtons").length) {
                extraButtons.fadeOut(300);
            }
        }
    });

    $(document).on("click", "#userSettingsPageLoad", function(event){
        window.location = "/automuhely_web/public/pages/userSettings.html";
    });
});

export let modal = document.createElement('div');

//Az egyedi értesítési ablak felépítése
export function openCustomModal(title, content) {
    modal.classList.add('custom-modal');
    modal.innerHTML = `
        <div class="modal-content">
            <span class="close-button">&times;</span>
            <h2>${title}</h2>
            <div class="modal-body">${content}</div>
        </div>
    `;

    document.body.appendChild(modal);

    modal.querySelector('.close-button').addEventListener('click', function() {
        modal.remove();
    });
}

