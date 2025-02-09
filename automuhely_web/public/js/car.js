import {BASE_URL, APP_URL, getUserCredentials} from './main.js';

var myCarOriginal;
var myCarOriginalCarLicense;
var myCarNewLicense;

$(document).ready(function(){
    $(document).on("click", ".modifyLicenseNumber", function(){
        var myCar = $(this).closest('.myCar');
        myCarNewLicense = myCar.find('.licenseNumberInput').val();
        if($(this).html() == "Véglegesítés" && myCarNewLicense != ""){
            $.ajax({
                type: "GET",
                url: BASE_URL + "/routes/api.php/users/"+getUserCredentials().userId+"/cars/" + myCarOriginalCarLicense,
                dataType: "json",
                success: function (response) {
                    $.ajax({
                        type: "PUT",
                        url: BASE_URL + "/routes/api.php/users/" + getUserCredentials().userId+"/cars/" + response["jarmu_id"],
                        data: JSON.stringify({newLicense: myCarNewLicense}),
                        dataType: "json",
                        contentType: "application/json",
                        success: function (response) {
                            alert(response["message"]);
                            fetchCars();
                        },
                        error: function(xhr){
                            const errorResponse = xhr.responseText;
                            alert(errorResponse);
                        }
                    });     
                }
            });
        }
        else if($(this).html() == "Rendszám módosítása"){
            myCarOriginal = $(this).closest('.myCar').html();
            myCarOriginalCarLicense = $(this).closest('.myCar').find('.licenseNumber').html();
            var licenseNumber = '<input class="licenseNumberInput" type="text" value='+myCarOriginalCarLicense+' required></input>';
            myCar.find('.licenseNumber').replaceWith(licenseNumber);
            myCar.find('.modifyLicenseNumber').html("Véglegesítés");
            $(this).closest('.myCarButtons').html($(this).closest('.myCarButtons').html() + '<button class="cancelModifyLicenseNumber">Mégsem</button>');
            myCar.find('.cancelService').addClass('disabledButton');
            myCar.find('.cancelService').prop("disabled", true);
        }
    })

    $(document).on("click", ".cancelModifyLicenseNumber", function(){
        $(this).closest('.myCar').html(myCarOriginal);
    })

    $(document).on("click", ".cancelService", function(){
        var myCar = $(this).closest(".myCar");
        var licenseNumber = myCar.find(".licenseNumber").html();
        $.ajax({
            type: "GET",
            url: BASE_URL + "/routes/api.php/users/"+getUserCredentials().userId+"/cars/"+licenseNumber,
            dataType: "json",
            success: function (response) {
                if(response["rendszam"])
            }
        });
    })
})

$(document).on('change', '#marka', function(){
    if($('#marka').val() != "valasszon"){
        $.ajax({
            type: "GET",
            url: BASE_URL + "/routes/api.php/cars/"+$('#marka').val()+"/types",
            dataType: "json",
            success: function (response) {
                var text='<option value="valasszon">Válasszon egy típust!</option>';
                response.forEach(type => {
                    text += '<option value="'+type['tipus_id']+'">'+type['tipus']+'</option>';
                });
                $("#tipus").html(text);
            }
        });
    }
    else{
        $('#tipus').html('<option value="valasszon">Válasszon egy típust!</option>');
    }
});

function fetchCars(){
    var text = '';
    $.ajax({
        type: "GET",
        url: BASE_URL + 'routes/api.php/users/'+getUserCredentials().userId+'/cars',
        data: getUserCredentials(),
        dataType: "json",
        success: function (response) {
            if(response.length > 0){
                response.forEach(car => {
                    text += '<div class="myCar">'+
                                '<p>Rendszám: </p><p class="licenseNumber">'+(car['rendszam'] == null ? '-' : car['rendszam'])+'</p>'+
                                '<p>Típus: </p><p>'+(car['tipus'] == null ? '-' : car['tipus'])+'</p>'+
                                '<p>Gyártás éve: </p><p>'+(car['gyartas_eve'] == null ? '-' : car['gyartas_eve'])+'</p>'+
                                '<p>Alváz adatok: </p><p>'+(car['alvaz_adatok'] == null ? '-' : car['alvaz_adatok'])+'</p>'+
                                '<p>Motor adatok: </p><p>'+(car['motor_adatok'] == null ? '-' : car['motor_adatok'])+'</p>'+
                                '<p>Előző javítások: </p><p>'+(car['elozo_javitasok'] == null ? '-' : car['elozo_javitasok'])+'</p>'+
                                '<p>Állapot: </p><p class="serviceStatus">'+(car['allapot'] == null ? '-' : car['allapot'])+'</p>'+
                                '<div class="myCarButtons">';
                    if(car['allapot'] != null){
                        text += '<button class="cancelService">Lemondás</button>';
                    }
                    text += '<button class="modifyLicenseNumber">Rendszám módosítása</button>'+
                        '</div>'+
                    '</div>';
                });
            }
            else{
                text += '<h1>Adja hozzá első autóját!</h1>';
            }
            $('#myCars').html(text);
            allCars = $('#myCars').html();

            loadCarBrands();
        },
        error: function(xhr){
            $('main h1').remove();
            $('#myCarsSearchBar').remove();
            $('#myCars').html('<h1>Autói megtekintéséhez <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a>, vagy <a href="'+BASE_URL+'public/pages/login.html">jelentkezzen be</a>!</h1>');
            $('#newCar').html("");
        }
    });
}

function loadCarBrands(){
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/cars/brands",
        dataType: "json",
        success: function (response) {
            var text = '<option value="valasszon">Válasszon egy márkát!</option>';
            response.forEach(brand => {
                text += '<option value="'+brand['marka_id']+'">'+brand['marka_neve']+'</option>';
            });
            $("#marka").html($("#marka").html() + text);
        }
    });
};

function addCar(event){
    event.preventDefault();
    const data = {
        userId: getUserCredentials().userId,
        rendszam: $('#rendszam').val(),
        marka: $('#marka').val(),
        tipus: $('#tipus').val(),
        elozo_javitasok: $('#elozo_javitasok').val()
    };
    if(data["marka"] != "valasszon" && data["tipus"] != "valasszon" && data["rendszam"] != ""){
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    alert(response.message);
                    fetchCars();
                    $('#marka').val('valasszon');
                    $('#tipus').val('valasszon');
                }
            },
            error: function (xhr){
                const errorResponse = JSON.parse(xhr.responseText);
                $('#error_message').removeAttr('hidden');
                $('#error_message').html(errorResponse.message);
            }
        });
    }
    else{
        if(data["rendszam"] == ""){
            alert("Adjon meg egy rendszámot!");
        }
        else if(data["marka"] == "valasszon"){
            alert("Válasszon egy márkát!");
        }
        else if(data["tipus"] == "valasszon"){
            alert("Válasszon egy típust!");
        }
    }
};

let allCars = null;

$('#myCarsSearchBar').on('input', function() {
    let foundCars = '';
    const searchTerms = $('#myCarsSearchBar').val().toLowerCase().split(" ").filter(term => term);

    $('#myCars').html(allCars);
    
    $('#myCars').find('.myCar').each(function() {
        const car = $(this);
        const carText = car.text().toLowerCase();

        const isMatch = searchTerms.some(term => carText.includes(term));

        if (isMatch) {
            foundCars += car.prop('outerHTML');
        }
    });
    
    $('#myCars').html(foundCars);

    if($('#myCars').html() == ""){
        $('#myCars').html(allCars)
    }
});


$('#myCarsSort').on('change', function () {
    let foundCars = '';
    const sortOption = $('#myCarsSort').val();
    const searchTerm = $('#myCarsSearchBar').val().toLowerCase();

    const carContainer = $('#myCars').html(allCars);

    let carsArray = [];
    carContainer.find('.car').each(function () {
        const car = $(this);
        carsArray.push(car);
    });

    switch (sortOption) {
        case "abc_asc":
            carsArray.sort(function (a, b) {
                const nameA = a.find('p').first().text().toLowerCase();
                const nameB = b.find('p').first().text().toLowerCase();
                return nameA.localeCompare(nameB);
            });
            break;
        case "abc_desc":
            carsArray.sort(function (a, b) {
                const nameA = a.find('p').first().text().toLowerCase();
                const nameB = b.find('p').first().text().toLowerCase();
                return nameB.localeCompare(nameA);
            });
            break;
    }

    carsArray.forEach(function (car) {
        const matches = car.find('p').filter(function () {
            return $(this).text().toLowerCase().includes(searchTerm);
        });

        if (matches.length > 0) {
            foundCars += car.prop('outerHTML');
        }
    });

    $('#myCars').html(foundCars);
});


window.fetchCars = fetchCars;
window.addCar = addCar;
