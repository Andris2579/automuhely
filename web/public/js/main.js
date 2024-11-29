import { registerAuth, loginAuth, logoutAuth, userSettingsSave } from './auth.js';
import { openProfileSmallMenu } from './ui.js';
import { userSettingsPageLoad} from './pages.js';

export const BASE_URL = '/Projects/School/automuhely_web/';
export const APP_URL = 'C:/Minden/Iskola/automuhely/web/';

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
});