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
                            text += '<div class="myCar"><table>'+
                                        '<tr><td>Rendszám: </td><td>'+(car['rendszam'] == null ? '-' : car['rendszam'])+'</td></tr>'+
                                        '<tr><td>Típus: </td><td>'+(car['tipus'] == null ? '-' : car['tipus'])+'</td></tr>'+
                                        '<tr><td>Gyártás éve: </td><td>'+(car['gyartas_eve'] == null ? '-' : car['gyartas_eve'])+'</td></tr>'+
                                        '<tr><td>Alváz adatok: </td><td>'+(car['alvaz_adatok'] == null ? '-' : car['alvaz_adatok'])+'</td></tr>'+
                                        '<tr><td>Motor adatok: </td><td>'+(car['motor_adatok'] == null ? '-' : car['motor_adatok'])+'</td></tr>'+
                                        '<tr><td>Előző javítások: </td><td>'+(car['elozo_javitasok'] == null ? '-' : car['elozo_javitasok'])+'</td></tr>'+
                                        '<tr><td>Állapot: </td><td>'+(car['allapot'] == null ? '-' : car['allapot'])+'</td></tr>'+
                                    '</table></div>';
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
            $('#myCarsSearchBar').remove();
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

$('#myCarsSearchBar').on('input', function(){
    let foundCars = '';
    const searchTerm = $('#myCarsSearchBar').val().toLowerCase();

    const carContainer = $('#myCars').html(allCars);

    carContainer.find('.myCar').each(function() {
        const car = $(this);
        const matches = car.find('td').filter(function() {
            return $(this).text().toLowerCase().includes(searchTerm);
        });

        if (matches.length > 0) {
            foundCars += car.prop('outerHTML');
        }
    });
    $('#myCars').html(foundCars);
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
