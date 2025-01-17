import { BASE_URL } from "./main.js";

export function fetchServices(){
    var text = "";
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/services",
        dataType: "json",
        success: function (response) {
            response.data.forEach(service => {
                text += '<div class="service">'+
                            '<p>'+service['nev']+'</p>'+
                            '<p>'+service['leiras']+'</p>'+
                            '<p>'+service['ar']+' Ft</p>'+
                            '<button class="book"><a href="public/pages/services.html#selected_service">Foglalás</a></button>'+
                        '</div>';
            });
            $('#services').html(text);
            allServices = $('#services').html();
        }
    });
}

window.fetchServices = fetchServices;
window.finalize = finalize;

let selected_div = null;
var selected_div_id = null;

$(document).on('click', '.book', function(){
    let service_divs = $('#services .service');
    selected_div = $(this).closest('.service');

    service_divs.each(function(index, element){
        if($(element).find('p').html() == selected_div.find('p').html()){
            selected_div_id = index+1;
        }
    })

    let selected_service = $('#selected_service');
    selected_service.removeAttr('hidden');
    selected_service.html('<h1>Kiválasztott szolgáltatás</h1>' + selected_div.html());
    selected_service.find('.book').remove();
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/cars",
        dataType: "json",
        success: function (response) {
            if(response){
                var cars = '<label for="cars">Autók: </label><select id="cars" name="cars"><option value="valasszon">Válasszon egy autót!</option>';
                response.forEach(car => {
                    if(car['allapot'] == null){
                        cars += '<option value='+car['jarmu_id']+'>'+car['rendszam']+'</option>';
                    }
                })
                cars += "</select><br>";
                var final_book = '<button id="final_book" onclick="finalize()">Véglegesítés</button>';
                selected_service.html(selected_service.html() + cars + final_book);
            }
            else{
                selected_service.html(selected_service.html() + '<br><h1><a href="'+BASE_URL+'public/pages/login.html">Jelentkezzen be</a>, vagy <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a> a foglalás véglegesítéséhez!</h1>');
            }
        }
    });
});

function finalize(){
    var car_id = $('#cars').val();
    const data = {service_id: selected_div_id, car_id: car_id}
    $.ajax({
        type: "POST",
        url: BASE_URL + "routes/api.php/service",
        data: data,
        dataType: "json",
        success: function (response) {
            alert(response.message);
            window.location = BASE_URL + "public/pages/services.html";
        }
    });
}

let allServices = null

$('#searchBar').on('input', function(){
    let foundServices = "";
    const searchTerm = $('#searchBar').val().toLowerCase();

    const serviceContainer = $('<div>').html(allServices);

    serviceContainer.find('.service').each(function() {
        const service = $(this);
        const matches = service.find('p').filter(function() {
            return $(this).text().toLowerCase().includes(searchTerm);
        });

        if (matches.length > 0) {
            foundServices += service.prop('outerHTML');
        }
    });
    $('main #services').html(foundServices);
});

$('#sort').on('change', function () {
    let foundServices = '';
    const sortOption = $('#sort').val();
    const searchTerm = $('#searchBar').val().toLowerCase();

    const serviceContainer = $('<div>').html(allServices);

    let servicesArray = [];
    serviceContainer.find('.service').each(function () {
        const service = $(this);
        servicesArray.push(service);
    });

    switch (sortOption) {
        case "abc_asc":
            servicesArray.sort(function (a, b) {
                const nameA = a.find('p').first().text().toLowerCase();
                const nameB = b.find('p').first().text().toLowerCase();
                return nameA.localeCompare(nameB);
            });
            break;
        case "abc_desc":
            servicesArray.sort(function (a, b) {
                const nameA = a.find('p').first().text().toLowerCase();
                const nameB = b.find('p').first().text().toLowerCase();
                return nameB.localeCompare(nameA);
            });
            break;
        case "popularity":
            break;
    }

    servicesArray.forEach(function (service) {
        const matches = service.find('p').filter(function () {
            return $(this).text().toLowerCase().includes(searchTerm);
        });

        if (matches.length > 0) {
            foundServices += service.prop('outerHTML');
        }
    });
    $('#services').html(foundServices);
});
