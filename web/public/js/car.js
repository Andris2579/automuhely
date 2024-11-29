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
                $("#tipus").html(text);
            }
        });
    }
    else{
        $('#tipus').html('<option value="valasszon">Válasszon egy típust!</option>');
    }
});

function fetchCars(){
    const data = {
        username: $('#username').val()
    };
    var text = $('main').html();
    $.ajax({
        type: "GET",
        url: BASE_URL + "routes/api.php/cars",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if(response.length > 0){
                response.forEach(car => {
                    text += '<div class="car">'+
                                'Rendszám: '+car['rendszam']+'<br>'+
                                'Típus: '+car['tipus']+'<br>'+
                                'Gyártás éve: '+car['gyartas_eve']+'<br>'+
                                'Alváz adatok: '+car['alvaz_adatok']+'<br>'+
                                'Motor adatok: '+car['motor_adatok']+'<br>'+
                                'Előző javítások: '+car['elozo_javitasok']+'<br>'+
                                'Állapot: '+car['allapot']+'<br>'+
                                '<button id="editCar" onclick="modifyCar()">Módosít</button><br>'+
                                '<button id="modifyCar" onclick="deleteCar()">Törlés</button><br>'+
                            '</div>';
                });
            }
            else{
                text += '<h1>Adja hozzá első autóját!</h1><br>'
            }
            text +='<br><div>'+
                        '<h1>Új autó</h1><br>'+
                        '<form action="POST">'+
                            '<label for="rendszam">Rendszám</label><input type="text" name="rendszam" id="rendszam"><br>'+
                            '<label for="marka">Márka</label><select name="marka" id="marka"><option value="valasszon">Válasszon egy márkát!</option></select><br>'+
                            '<label for="tipus">Típus</label><select name="tipus" id="tipus"><option value="valasszon">Válasszon egy típust!</option></select><br>'+
                            '<label for="elozo_javitasok">Előző javítások</label><input type="text" name="elozo_javitasok" id="elozo_javitasok"><br>'+
                            '<button onclick="addCar()">Új Autó</button>'+
                        '</form>'
                    '</div>';
            $('main').html(text);

            loadCarBrands();
        }
    });
}

function loadCarBrands(){
    $.ajax({
        type: "GET",
        url: BASE_URL + "/routes/api.php/carBrand",
        dataType: "json",
        success: function (response) {
            var text = $('#marka').html();
            response.forEach(brand => {
                text += '<option value="'+brand['marka_id']+'">'+brand['marka_neve']+'</option>';
            });
            $("#marka").html(text);
        }
    });
};

function addCar(){
    const data = {
        rendszam: $('#rendszam').val(),
        marka: $('#marka').val(),
        tipus: $('#tipus').val(),
        elozo_javitasok: $('#elozo_javitasok').val()
    };
    $.ajax({
        type: "POST",
        url: BASE_URL + "routes/api.php/car",
        data: data,
        dataType: "json",
        success: function (response) {

        }
    });
};

/*$(document).on('click', 'button', function() {
    let selected_div = $(this).closest('.car');
    selected_div.html("fasz");
});

function deleteCar(){

}*/

window.fetchCars = fetchCars;
window.addCar = addCar;
//window.deleteCar = deleteCar;