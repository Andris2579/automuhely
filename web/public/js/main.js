import { registerAuth, loginAuth, logoutAuth, userSettingsSave } from './auth.js';
import { openProfileSmallMenu } from './ui.js';
import { userSettingsPageLoad} from './pages.js';

export const BASE_URL = '/automuhely_web/';
export const APP_URL = 'C:/xampp/htdocs/automuhely/web/';

window.userSettingsPageLoad = userSettingsPageLoad;
window.register = registerAuth;
window.login = loginAuth;
window.logout = logoutAuth;
window.openProfileSmallMenu = openProfileSmallMenu;
window.userSettingsSave = userSettingsSave;

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/header",
        dataType: "json",
        success: function (response) {
            $('header').html(response);
            if(window.location.href == ('http://localhost' + BASE_URL + "public/pages/login.html") ||
            window.location.href == ('http://localhost' + BASE_URL + "public/pages/register.html")){
                $.ajax({
                    type: "GET",
                    url: BASE_URL + "routes/api.php/userLoggedIn",
                    dataType: "json",
                    success: function (response) {
                        window.location = BASE_URL + "public/index.html";
                    }
                });
            }
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
    $(document).on("click","#logoHamburger", function(event){
        const nav = $('nav');
        if (nav.css('display') === 'none') {
            nav.css('display', 'inline');
        } else {
            nav.css('display', 'none');
        }

        event.stopPropagation();
    });
    $(document).on("click", function(event) {
        const nav = $('nav');
    
        if (!$(event.target).closest("#logoHamburger, nav").length) {
            nav.css('display', 'none');
        }
    });
});

