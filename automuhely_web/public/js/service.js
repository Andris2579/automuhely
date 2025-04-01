import { BASE_URL, getUserCredentials, openCustomModal } from "./main.js";

//Betölti és megjeleníti a szolgáltatásokat
export function fetchServices(){
    var text = "";
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/services",
        dataType: "json",
        success: function (response) {
            console.log(response);
            //Felépíti a szolgáltatások kártyát
            response.forEach(service => {
                text += '<div class="customCard service">'+
                            '<div class="service_container">'+
                                '<h2>'+service['nev']+'</h2>'+
                                '<p>'+service['leiras']+'</p>'+
                                '<p>Ár: '+service['ar']+' Ft</p>'+
                                '<button class="customButton book">Foglalás</button>'+
                            '</div>'+
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

//Kiválasztja a kiválasztott szolgáltatást, és ezt megjeleníti az űrlapban, ahol a véglegesítés végezhető el
$(document).on('click', '.book', function(){
    if($('#selected_service').length === 0){
        $('#services').append('<form id="selected_service" class="customCard" method="POST" hidden></form>');
    }
    //Megkeresi a kiválasztott szolgáltatást
    let service_divs = $('#services .service');
    selected_div = $(this).closest('.service');

    service_divs.each(function(index, element){
        if($(element).find('p').html() == selected_div.find('p').html()){
            selected_div_id = index+1;
        }
    })

    window.location.href = "public/pages/services.html#selected_service";

    let selected_service = $('#selected_service');
    selected_service.removeAttr('hidden');
    selected_service.html('<h2>Kiválasztott szolgáltatás</h2>' + selected_div.html());
    selected_service.find('.book').remove();

    //Betölti a kiválasztott szolgáltatás adatait és a felhasználó autóit
    if(getUserCredentials() != null){
        $.ajax({
            type: "GET",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars",
            dataType: "json",
            success: function (response) {
                if(Object.keys(response['cars']).length > 0){
                    var cars = '<label for="cars">Autók: </label><select class="customSelect" id="cars" name="cars"><option value="valasszon">Válasszon egy autót!</option>';
                    Object.values(response['cars']).forEach(car => {
                        cars += '<option value='+car['jarmu_id']+'>'+car['rendszam']+'</option>';
                    })
                    cars += "</select>";
                    var final_book = '<button class="customButton final_book" onclick="finalize(event)">Véglegesítés</button>'+
                                        '<p id="error_message" hidden></p>';
                    selected_service.html(selected_service.html() + cars + final_book);
                }
                else{
                    selected_service.append('<p>Először adja hozzá első autóját az oldalhoz!</p>');
                }
            }
        });
    }
    else{
        selected_service.append('<p id="service_message"><a href="'+BASE_URL+'public/pages/login.html">Jelentkezzen be</a>, vagy <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a> a foglalás véglegesítéséhez!</p>');
    }
});

//Véglegesíti a foglalást
function finalize(event){
    event.preventDefault();

    var car_id = $('#cars').val();
    const data = {service_id: selected_div_id, car_id: car_id}

    //Ellenőrzi, hogy van e kiválasztott autó a szolgáltatáshoz
    if(car_id == "valasszon"){
        $('#error_message').removeAttr('hidden');
        $('#error_message').html("Válasszon egy autót!");
    }
    else{
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars/"+car_id+"/services",
            data: JSON.stringify(data),
            contentType: 'application/json',
            dataType: "json",
            success: function (response) {
                console.log(response);
                fetchServices();
                openCustomModal("Sikeres foglalás", '<p>'+response['message']+'</p>');
            },
            error: function (xhr){
                if(xhr.status === 400){
                    const errorResponse = JSON.parse(xhr.responseText);
                    openCustomModal("Hiba", errorResponse.message);
                }
                else{
                    openCustomModal("Hiba", "Hiba történt a foglalás során!");
                }
            }
        });
    }
}

let allServices = null

//Keres a szolgáltatások között, szóközzel elválasztva több elemre
$('#servicesSearchBar').on('input', function() {
    let foundServices = "";
    const searchTerms = $('#servicesSearchBar').val().toLowerCase().split(" ").filter(term => term);
    
    const serviceContainer = $('<div>').html(allServices);
    
    //Végig iterál az összes szolgáltatások kártyán
    serviceContainer.find('.service').each(function() {
        const service = $(this);
        const serviceText = service.find('p, h2').text().toLowerCase();
        
        const isMatch = searchTerms.some(term => serviceText.includes(term));
        
        if (isMatch) {
            foundServices += service.prop('outerHTML');
        }
    });
    
    $('#services').html(foundServices);

    if($('#servicesSearchBar').val() == ""){
        $('#services').html(allServices);  
    }
});
