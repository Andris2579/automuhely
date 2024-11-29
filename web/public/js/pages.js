import { BASE_URL } from "./main.js";

export function userSettingsPageLoad(){
    $.ajax({
        type: "POST",
        url: BASE_URL + "app/Views/SettingsView.php",
        dataType: "json",
        success: function (response) {
            $('main').html(response);
        }
    });
}
