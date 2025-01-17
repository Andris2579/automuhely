import {BASE_URL, APP_URL} from './main.js';

$(document).ready(function() {
    $('#password_show').on('change', function() {
        if ($(this).is(':checked')) {
            $('#password').attr('type', 'text');
            $('#password_again').attr('type', 'text');
        } else {
            $('#password').attr('type', 'password');
            $('#password_again').attr('type', 'password');
        }
    });
});

function isFirstCharacterDigit(text) {
    const firstChar = text.charAt(0);
    return !isNaN(firstChar) && firstChar !== "";
}

export function registerAuth(event){
    event.preventDefault();

    const data = {
        name: $('#sure_name').val()+" "+$('#first_name').val(),
        username: $('#username').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        password_again: $('#password_again').val(),
        phone_number: $('#phone_number').val()
    };
    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$/;

    if(data.password != data.password_again){
        $('#error_message').removeAttr('hidden');
        $('#error_message').html('A jelszó nem egyezik!');
    }
    else if (!passwordPattern.test(data.password)) {
        $('#error_message').removeAttr('hidden');
        console.log(data.password);
        console.log(data.password_again);
        $('#error_message').html('A jelszónak legalább 10 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűt, számot és speciális karaktert!');
    }
    else if(isFirstCharacterDigit($('#username').val())){
        $('#error_message').removeAttr('hidden');
        $('#error_message').html('A felhasználónév nem kezdődhet számmal!');
    }
    else{
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/user",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    alert("Sikeres regisztráció!");
                    window.location.href = BASE_URL + "public/index.html";
                }
            },
            error: function (xhr) {
                if (xhr.status === 400) {
                    const errorResponse = JSON.parse(xhr.responseText);
                    alert(errorResponse.message);
                    window.location.href = BASE_URL + "public/pages/register.html";
                } else {
                    alert("Hiba történt! :(");
                }
            }
        });
    }
}

export function loginAuth(event){
    event.preventDefault();
    const data = {username: $('#username').val(), password: $('#password').val()};
    var url = BASE_URL + "routes/api.php/auth/login";
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: "json",
        success: function (response) {
            if(response.success){
                alert(response.message);
                window.location.href = BASE_URL + "public/index.html";
            }
        },
        error: function (xhr) {
            alert(xhr.responseJSON.message);
            window.location.href = BASE_URL + "public/index.html";
        }
    });
}

export function logoutAuth(event){
    event.preventDefault();
    $.ajax({
        type: "POST",
        url: BASE_URL + "routes/api.php/auth/logout",
        dataType: "json",
        success: function (response) {
            alert("Sikeres kijelentkezés!");
                window.location.href = BASE_URL + "public/index.html";
        }
    });
}

export function userSettingsSave(event){
    event.preventDefault();
    const data = {
        action: 'userSettingsSave',
        username: $('#profile').text(),
        email: $('#email').val(),
        password: $('#password').val(),
        phone_number: $('#phone_number').val(),
        cim: $('#zip_code').val()+","+$('#city').val()+","+$('#street').val()+","+$('#house_number').val()
    }
    $.ajax({
        type: "POST",
        url: BASE_URL + "routes/api.php",
        data: data,
        dataType: "json",
        success: function (response) {
            console.log(response);
            if(response.success){
                alert("Sikeresen frissítette a felhasználói adatokat!");
                location.reload();
            }
        },
        error: function (xhr) {
            console.error("Raw Server Response: ", xhr.responseText);
            try {
                const errorResponse = JSON.parse(xhr.responseText);
                alert(errorResponse.message);
            } catch (e) {
                console.error("Failed to parse JSON: ", e);
                alert("Unexpected server response.");
            }
        }
    });
}