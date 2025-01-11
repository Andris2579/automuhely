import {BASE_URL, APP_URL} from './main.js';

$(document).on('change', '#marka', function(){
    if($('#marka').val() != "valasszon"){
        $.ajax({
            type: "GET",
            url: BASE_URL + "/routes/api.php/carType",
            data: {marka: $('#marka').val()},
            dataType: "json",
            success: function (response) {
                var text="";
                response.forEach(type => {
                    text += '<option value="'+type['tipus_id']+'">'+type['tipus']+'</option>';
                });
                $("#tipus").html($("#tipus").html() + text);
            }
        });
    }
    else{
        $('#tipus').html('<option value="valasszon">Válasszon egy típust!</option>');
    }
});

function fetchCars(){
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/userLoggedIn",
        dataType: "json",
        success: function () {
            var text = '';
            $.ajax({
                type: "GET",
                url: BASE_URL + "routes/api.php/cars",
                dataType: "json",
                success: function (response) {
                    if(response.length > 0){
                        response.forEach(car => {
                            text += '<div class="car">'+
                                        '<p>Rendszám: '+(car['rendszam'] == null ? '-' : car['rendszam'])+'</p><br>'+
                                        '<p>Típus: '+(car['tipus'] == null ? '-' : car['tipus'])+'</p><br>'+
                                        '<p>Gyártás éve: '+(car['gyartas_eve'] == null ? '-' : car['gyartas_eve'])+'</p><br>'+
                                        '<p>Alváz adatok: '+(car['alvaz_adatok'] == null ? '-' : car['alvaz_adatok'])+'</p><br>'+
                                        '<p>Motor adatok: '+(car['motor_adatok'] == null ? '-' : car['motor_adatok'])+'</p><br>'+
                                        '<p>Előző javítások: '+(car['elozo_javitasok'] == null ? '-' : car['elozo_javitasok'])+'</p><br>'+
                                        '<p>Állapot: '+(car['allapot'] == null ? '-' : car['allapot'])+'</p><br>'+
                                    '</div>';
                        });
                    }
                    else{
                        text += '<h1>Adja hozzá első autóját!</h1><br>';
                    }
                    $('#myCars').html(text);
                    allCars = $('#myCars').html();
        
                    loadCarBrands();
                }
            });
        },
        error: function(xhr){
            $('main h1').remove();
            $('main #searchBar').remove();
            $('#myCars').html('<h1>Autói megtekintéséhez <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a>, vagy <a href="'+BASE_URL+'public/pages/login.html">jelentkezzen be</a>!</h1>');
            $('#newCar').html("");
        }
    });
}

function loadCarBrands(){
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/carBrand",
        dataType: "json",
        success: function (response) {
            var text = '';
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
        rendszam: $('#rendszam').val(),
        marka: $('#marka').val(),
        tipus: $('#tipus').val(),
        elozo_javitasok: $('#elozo_javitasok').val()
    };
    if(data["marka"] != "valasszon" && data["tipus"] != "valasszon" && data["rendszam"] != ""){
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/car",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    fetchCars();
                    $('#marka').val('0');
                    $('#tipus').val('0');
                }
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

$('#searchBar').on('input', function(){
    let foundCars = '';
    const searchTerm = $('#searchBar').val().toLowerCase();

    const carContainer = $('<div>').html(allCars);

    carContainer.find('.car').each(function() {
        const car = $(this);
        const matches = car.find('p').filter(function() {
            return $(this).text().toLowerCase().includes(searchTerm);
        });

        if (matches.length > 0) {
            foundCars += car.prop('outerHTML');
        }
    });
    $('#myCars').html(foundCars);
});

$('#sort').on('change', function () {
    let foundCars = '';
    const sortOption = $('#sort').val();
    const searchTerm = $('#searchBar').val().toLowerCase();

    const carContainer = $('<div>').html(allCars);

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
