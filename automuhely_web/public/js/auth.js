import {BASE_URL, APP_URL, getToken, setToken, clearToken, getUserCredentials} from './main.js';

$(document).ready(function() {
    $('#password_show').on('click', function() {
        if ($('#password').attr('type') === 'password') {
            $('#password').attr('type', 'text');
            $('#password_show').attr('src', BASE_URL + 'public/assets/imgs/eye_open.png');
        } else {
            $('#password').attr('type', 'password');
            $('#password_show').attr('src', BASE_URL + 'public/assets/imgs/eye_closed.png');
        }
    });
    $('#password_again_show').on('click', function() {
        if ($('#password_again').attr('type') === 'password') {
            $('#password_again').attr('type', 'text');
            $('#password_again_show').attr('src', BASE_URL + 'public/assets/imgs/eye_open.png');

        } else {
            $('#password_again').attr('type', 'password');
            $('#password_again_show').attr('src', BASE_URL + 'public/assets/imgs/eye_closed.png');
        }
    });
    if(window.location.href == ("http://localhost" + BASE_URL + 'public/pages/userSettings.html')){
        $.ajax({
            type: "GET",
            url: "routes/api.php/users/" + getUserCredentials().userId,
            dataType: "json",
            success: function (response) {
                var nev = response["nev"].split(" ");
                $('#sure_name').val(nev[0]);
                $('#first_name').val(nev.slice(1).join(" "));
                $('#username').val(response["felhasznalonev"]);
                $('#email').val(response["email"]);
                $('#phone_number').val(response["telefonszam"]);
                var cim = response["cim"].split(",");
                $('#zip_code').val(cim[0]);
                $('#city').val(cim[1]);
                $('#street').val(cim[2]);
                $('#house_number').val(cim[3]);
            }
        });
    }
});

function isFirstCharacterDigit(text) {
    const firstChar = text.charAt(0);
    return !isNaN(firstChar) && firstChar !== "";
}

export function register(event){
    event.preventDefault();

    var form = $('form')[0];

    if (!form.checkValidity()) {
        alert("Kérlek, tölts ki minden *-gal jelölt mezőt!");
    }
    else{
        const data = {
            name: $('#sure_name').val()+" "+$('#first_name').val(),
            username: $('#username').val(),
            email: $('#email').val(),
            password: $('#password').val(),
            password_again: $('#password_again').val(),
            phone_number: $('#phone_number').val()
        };
        const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{10,}$/;
    
        if(data.password != data.password_again){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html('A jelszó nem egyezik!');
        }
        else if (!passwordPattern.test(data.password)) {
            $('#error_message').removeAttr('hidden');
            $('#error_message').html('A jelszónak legalább 10 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűt, számot és speciális karaktert!');
        }
        else if(isFirstCharacterDigit($('#username').val())){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html('A felhasználónév nem kezdődhet számmal!');
        }
        else if(isFirstCharacterDigit($('#first_name').val()) || isFirstCharacterDigit($('#sure_name').val())){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html('A név nem kezdődhet számmal!');
        }
        else if(/\d/.test($('#first_name').val()) || /\d/.test($('#sure_name').val())){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html('A név nem tartalmazhat számot!');
        }
        else{
            $.ajax({
                type: "POST",
                url: BASE_URL + "routes/api.php/users",
                data: data,
                dataType: "json",
                success: function (response) {
                    if(response.success == true){
                        login(event);
                    }
                },
                error: function (xhr) {
                    const errorResponse = JSON.parse(xhr.responseText);
                    $('#error_message').removeAttr('hidden');
                    $('#error_message').html(errorResponse.message);
                }
            });
        }
    }
}

export function login(event){
    event.preventDefault();

    const data = {username: $('#username').val(), password: $('#password').val()};
    var form = $('form')[0];
    if(!form.checkValidity()){
        $('#error_message').removeAttr("hidden");
        $('#error_message').html("Kérlek, töltsön ki minden mezőt!");
    }
    else{
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/auth/login",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    setToken(response.token);
                    window.location.href = BASE_URL + "public/index.html";
                }
            },
            error: function (xhr) {
                const errorResponse = xhr.responseText;
                $('#error_message').removeAttr('hidden');
                $('#error_message').html(errorResponse['message']);
            }
        });
    }
}

export function logoutAuth(event){
    event.preventDefault();
    $.ajax({
        type: "POST",
        url: BASE_URL + "routes/api.php/auth/logout",
        dataType: "json",
        success: function (response) {
            alert(response.message);
            clearToken();
            window.location.href = BASE_URL + "public/index.html";
        }
    });
}

async function sha256Hash(password) {
    const encoder = new TextEncoder();
    const data = encoder.encode(password);
    const hashBuffer = await crypto.subtle.digest("SHA-256", data);
    
    return Array.from(new Uint8Array(hashBuffer))
        .map(b => b.toString(16).padStart(2, '0'))
        .join('');
}

export function userSettingsSave(event){
    event.preventDefault();
    const data = {
        "name": $('#sure_name').val() + " " + $('#first_name').val(),
        "username": $('#username').val(),
        "email": $('#email').val(),
        "phone_number": $('#phone_number').val(),
        "cim": $('#zip_code').val() + "," + $('#city').val() + "," + $('#street').val() + "," + $('#house_number').val()
    }
    var form = $('form')[0];

    if(!form.checkValidity()){
        $('#error_message').removeAttr('hidden');
        $('#error_message').html("Kérjük töltsön ki minden *-gal jelölt mezőt!");
    }
    else if($('#password').val() == $('#password_again').val()){
        $.ajax({
            type: "GET",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId,
            dataType: "json",
            success: function (response) {
                sha256Hash($('#password').val()).then(hashedPassword =>{
                    if(response["jelszo_hash"] == hashedPassword){
                        $.ajax({
                            type: "PUT",
                            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId,
                            data: JSON.stringify(data),
                            dataType: "json",
                            success: function (response) {
                                alert(response.message);
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
                    else{
                        $('#error_message').removeAttr("hidden");
                        $('#error_message').html("Hibás jelszó!");
                    }
                })
            }
        });
    }
    else{
        $('#error_message').removeAttr("hidden");
        $('#error_message').html("A két jelszó nem egyezik!");
    }
}