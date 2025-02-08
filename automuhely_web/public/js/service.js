import { BASE_URL, getUserCredentials } from "./main.js";

export function fetchServices(){
    var text = "";
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/services",
        dataType: "json",
        success: function (response) {
            response.forEach(service => {
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
    selected_service.html('<h2>Kiválasztott szolgáltatás</h2>' + selected_div.html());
    selected_service.find('.book').remove();
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars",
        dataType: "json",
        success: function (response) {
            if(response){
                var cars = '<label for="cars">Autók: </label><select id="cars" name="cars"><option value="valasszon">Válasszon egy autót!</option>';
                response.forEach(car => {
                    if(car['allapot'] == null){
                        cars += '<option value='+car['jarmu_id']+'>'+car['rendszam']+'</option>';
                    }
                })
                cars += "</select>";
                var final_book = '<button id="final_book" onclick="finalize(event)">Véglegesítés</button>';
                selected_service.html(selected_service.html() + cars + final_book);
            }
            else{
                selected_service.html(selected_service.html() + '<p><a href="'+BASE_URL+'public/pages/login.html">Jelentkezzen be</a>, vagy <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a> a foglalás véglegesítéséhez!</p>');
            }
        }
    });
});


function finalize(event){
    event.preventDefault();

    var car_id = $('#cars').val();
    const data = {service_id: selected_div_id, car_id: car_id}

    if(car_id == "valasszon"){
        alert("Válasszon egy autót!");
    }
    else{
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars/"+car_id+"/services",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    alert(response.message);
                    window.location = BASE_URL + "public/pages/services.html";
                }
            },
            error: function (xhr){
                if(xhr.status === 400){
                    const errorResponse = JSON.parse(xhr.responseText);
                    alert(errorResponse.message);
                }
                else{
                    alert("Hiba történt! :(");
                }
            }
        });
    }
}

let allServices = null

$('#servicesSearchBar').on('input', function() {
    let foundServices = "";
    const searchTerms = $('#servicesSearchBar').val().toLowerCase().split(" ").filter(term => term);
    
    const serviceContainer = $('<div>').html(allServices);
    
    serviceContainer.find('.service').each(function() {
        const service = $(this);
        const serviceText = service.find('p').text().toLowerCase();
        
        const isMatch = searchTerms.some(term => serviceText.includes(term));
        
        if (isMatch) {
            foundServices += service.prop('outerHTML');
        }
    });
    
    $('#services').html(foundServices);

    if($('#services').html() == ""){
        $('#services').html(allServices);  
    }
});


$('#sort').on('change', function () {
    let foundServices = '';
    const sortOption = $('#sort').val();
    const searchTerm = $('#servicesSearchBar').val().toLowerCase();

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
