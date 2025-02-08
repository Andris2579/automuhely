import { register, login, logoutAuth, userSettingsSave } from './auth.js';

export const BASE_URL = '/automuhely_web/';
export const APP_URL = 'C:/xampp/htdocs/automuhely/web/';

window.register = register;
window.login = login;
window.logout = logoutAuth;
window.userSettingsSave = userSettingsSave;

let token = null;

export function setToken(newToken) {
    token = newToken;
    localStorage.setItem('authToken', newToken);
}

export function getToken() {
    if (!token) {
        token = localStorage.getItem('authToken');
    }
    return token;
}

export function clearToken() {
    token = null;
    localStorage.removeItem('authToken');
}

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
    if((window.location == ("http://localhost" + BASE_URL + "public/index.html")) && getUserCredentials() != null){
        $('#main_message').find('#register_and_loginLink').remove();
        $('#servicesLink').html('Tekintse meg szolgáltatásainkat <a href="public/pages/services.html">itt</a>!');
    }
    if((window.location == ("http://localhost" + BASE_URL + "public/pages/login.html") || window.location == ("http://localhost" + BASE_URL + "public/pages/register.html")) && getUserCredentials() != null){
        alert("A felhasználó már regisztrált/bejelentkezett.");
        window.location = "http://localhost" + BASE_URL + "public/index.html";
    }
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
    $(document).on("click", "#logoHamburger", function(event) {
        const nav = $("nav");
    
        if (nav.css("display") === "none") {
            nav.fadeIn(600);
        }
        else {
            nav.fadeOut(400);
        }
    
        event.stopPropagation();
    });
    $(document).on("click", function(event) {
        const nav = $('nav');
        if($('#logoHamburger').css("display") === "block"){
            if (!$(event.target).closest("#logoHamburger, nav").length) {
                nav.fadeOut(400);
            }
        }
    });
    $(document).on("click", "#profile", function(event){
        const extraButtons = $("#extraButtons");
    
        if (extraButtons.css("display") === "none") {
            extraButtons.fadeIn(600);
        }
        else {
            extraButtons.fadeOut(400);
        }
    
        event.stopPropagation();
    })
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

