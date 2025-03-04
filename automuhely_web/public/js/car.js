import {BASE_URL, APP_URL, getUserCredentials, openCustomModal, modal} from './main.js';

var myCarOriginal;
var myCarOriginalCarLicense;
var myCarNewLicense;
var myCarsOriginal;

$(document).ready(function(){
    //Bármilyen változtatás előtt elmentjük az oldal állapotát
    myCarsOriginal = $('#myCars').html();

    //Módosítja az autó rendszámát
    $(document).on("click", ".modifyLicenseNumber", function(){
        var myCar = $(this).closest('.myCar');
        myCarNewLicense = myCar.find('.licenseNumberInput').val();

        /*
            A rendszám módosítás gombnak kettő állapota van, így ezek alapján dönti el a program, hogy melyik esetben mi történik.
            Ha a Véglegesítés feliratra kattintunk, akkor elmentjük az új rendszámot.
            Azonban ha a Rendszám módosítása feliratra kattintunk, akkor a rendszám helyére egy bemeneti mező kerül benne a jelenlegi rendszámmal.
        */
        if($(this).html() == "Véglegesítés" && myCarNewLicense != ""){
            $.ajax({
                type: "GET",
                url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars/" + myCarOriginalCarLicense,
                dataType: "json",
                success: function (response) {
                    $.ajax({
                        type: "PUT",
                        url: BASE_URL + "routes/api.php/users/" + getUserCredentials().userId+"/cars/" + response["jarmu_id"],
                        data: JSON.stringify({newLicense: myCarNewLicense}),
                        dataType: "json",
                        contentType: "application/json",
                        success: function (response) {
                            openCustomModal("Sikeres módosítás", response.message);
                            fetchCars();
                        },
                        error: function(xhr){
                            const errorResponse = xhr.responseText;
                            openCustomModal("Hiba", errorResponse);
                        }
                    });     
                }
            });
        }
        else if($(this).html() == "Rendszám módosítása"){
            //Elmentjük az autó kártya állapotát, ha esetleg a módosítás közben meggondolná magát a felhasználó.
            myCarOriginal = $(this).closest('.myCar').html();
            myCarOriginalCarLicense = $(this).closest('.myCar').find('.licenseNumber').html();

            //A rendszám helyére egy bemeneti mező kerül benne a jelenlegi rendszámmal
            var licenseNumber = '<input class="licenseNumberInput customInput" type="text" value='+myCarOriginalCarLicense+' required></input>';
            myCar.find('.licenseNumber').replaceWith(licenseNumber);

            //A rendszám módosítása gomb felirata megváltozik
            myCar.find('.modifyLicenseNumber').html("Véglegesítés");

            //Megjelenik a mégsem gomb, ha a felhasználó mégsem szeretne módosítani a rendszámon
            $(this).closest('.myCarButtons').html($(this).closest('.myCarButtons').html() + '<button class="cancelModifyLicenseNumber customButton">Mégsem</button>');

            //A lemondás gombot átmenetileg letiltjuk
            myCar.find('.cancelService').addClass('disabledButton');
            myCar.find('.cancelService').prop("disabled", true);
        }
    })

    //Ha a felhasználó meggondolja magát a rendszám módosítás során, akkor visszaállítjuk az autó kártyát eredeti állapotába
    $(document).on("click", ".cancelModifyLicenseNumber", function(){
        $(this).closest('.myCar').html(myCarOriginal);
    })

    var selectedCarLicenseNumber = null;

    //Megjeleníti az adott autóhoz tartozó jelenleg is aktív szolgáltatásokat
    $(document).on("click", ".cancelService", function(){
        selectedCarLicenseNumber = null;

        var myCar = $(this).closest(".myCar");
        var licenseNumber = myCar.find(".licenseNumber").html();
        selectedCarLicenseNumber = licenseNumber;

        $.ajax({
            type: "GET",
            url: BASE_URL + 'routes/api.php/users/'+getUserCredentials().userId+'/cars/'+licenseNumber+'/services',
            dataType: "json",
            success: function (response) {
                var text = '<div id="cancelServiceContainer">'+
                            '<select class="customSelect" id="cancelServiceSelect"><option value="valasszon">Válasszon egy szolgáltatást!</option>';
                response.forEach(service => {
                    text += '<option value="'+service['szolgaltatas_id']+'">'+service['nev']+'</option>';
                });
                text += '</select>'+
                        '<button class="customButton" id="cancelServiceBtn">Lemondás</button>'+
                        '</div>';
                openCustomModal("Lemondás", text);
            }
        })
    });

    //Lemondja a felhasználó átal kiválasztott az adott autóhoz tartozó szolgáltatást
    $(document).on("click", "#cancelServiceBtn", function(){
        $.ajax({
            type: "PUT",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars/"+selectedCarLicenseNumber+"/services",
            dataType: "json",
            data: JSON.stringify({serviceName : $("#cancelServiceSelect option:selected").text()}),
            contentType: "application/json",
            success: function (response) {
                openCustomModal("Sikeres lemondás", response.message);
                fetchCars();
            },
            error: function(xhr){
                console.log(xhr);
            }
        });
    });

    //Megjelenít egy megerősítő ablakot az autó törlése előtt
    $(document).on("click", ".deleteCar", function(){
        selectedCarLicenseNumber = null;

        var myCar = $(this).closest(".myCar");
        var licenseNumber = myCar.find(".licenseNumber").html();
        selectedCarLicenseNumber = licenseNumber;

        var text = '<p>Biztosan törölni szeretné az autót?</p>'+
                    '<div id="cancelDeleteCarButtons">'+
                        '<button id="carDeleteYes" class="customButton">Törlés</button>'+
                        '<button id="carDeleteNo" class="customButton">Mégsem</button>'+
                    '</div>';

        openCustomModal("Autó törlése", text);
    });

    //Kitörli az adott autót
    $(document).on("click", "#carDeleteYes", function(){
        modal.remove();
        $.ajax({
            type: "DELETE",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars/"+selectedCarLicenseNumber,
            success: function (response) {
                if(response.success){
                    fetchCars();
                }
            },
            error: function(xhr){
                openCustomModal("Hiba", JSON.parse(xhr.responseText)["message"]);
            }
        });
        selectedCarLicenseNumber = null;
    });

    //Bezárja a törlés ablakot
    $(document).on("click", "#carDeleteNo", function(){
        selectedCarLicenseNumber = null;
        modal.remove();
    });

    //Megjeleníti az autóval kapcsolatos különböző adatokat
    $(document).on("click", ".carDetails button", function(){
        let buttonText = $(this).text();
        let parentCar = $(this).closest(".myCar");
        let licenseNumber = parentCar.find(".licenseNumber").text();
        
        //Megjeleníti az autó előző javításait
        if (buttonText === "Előző javítások") {
            $.ajax({
                type: "GET",
                url: BASE_URL + "routes/api.php/users/" + getUserCredentials().userId + "/cars/" + licenseNumber,
                dataType: "json",
                success: function(response) {
                    console.log(response);
                    let repairsList = response["elozo_javitasok"].length ? response["elozo_javitasok"] : '<p>Nincs adat.</p>';
                    openCustomModal("Előző javítások", repairsList);
                }
            });
        }
        
        //Megjeleníti az autóhoz tartozó aktív szolgáltatásokat
        if (buttonText === "Aktív szolgáltatásaim") {
            let servicesList = '<table class="activeServices"><tr><th>Szolgáltatás</th><th>Állapot</th></tr>';
            $.ajax({
                type: "GET",
                url: BASE_URL + "routes/api.php/users/" + getUserCredentials().userId + "/cars/" + licenseNumber + "/services",
                dataType: "json",
                success: function(response) {
                    if(response != null){ //Ellenőrizzük, hogy van e aktív szolgáltatás, hogy ha nincs, akkor ne legyen gond a formázással
                        response.forEach(function(service){
                            servicesList += '<tr><td>'+service['nev']+'</td><td>'+service['allapot']+'</td></tr>';
                        });
                    }
                    else{
                        servicesList = '<p>Nincs aktív szolgáltatás.</p>';

                    }
                    servicesList += '</table>';
                    openCustomModal("Aktív szolgáltatások", servicesList);
                }
            });
        }
    });
});

//Ha kiválaszt a felhasználó egy adott márkát az új autó létrehozásánál, akkor betölti a márkához tartozó autótípusokat
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

//Megjeleníti a felhasználó összes adatát
function fetchCars(){
    $('#myCars').html(myCarsOriginal);
    allCars = [];

    $.ajax({
        type: "GET",
        url: BASE_URL + 'routes/api.php/users/'+getUserCredentials().userId+'/cars',
        data: getUserCredentials(),
        dataType: "json",
        success: function (response) {
            //Ha van autója a felhasználónak, akkor minden autó kártyáját megfelelően felépíti
            if(Object.keys(response['cars']).length > 0){
                Object.values(response['cars']).forEach(car => {
                    let carHtml = '<div class="customCard myCar">'+
                                '<div class="carTitle">'+
                                    '<h2 class="licenseNumber">'+(car['rendszam'] == null ? '-' : car['rendszam'])+'</h2>'+
                                    '<h4>'+(car['tipus'] == null ? '-' : car['tipus'])+'</h4>'+
                                '</div>'+
                                '<div class="carDetails">'+
                                    '<div class="carDetailsContent">'+
                                        '<p>Márka:</p><p>'+(car['marka_neve'] == null ? '-' : car['marka_neve'])+'</p>'+
                                        '<p>Gyártás éve:</p><p>'+(car['gyartas_eve'] == null ? '-' : car['gyartas_eve'])+'</p>'+
                                    '</div>'+
                                    '<div class="carDetailsButtons">'+
                                        '<button class="customButton">Előző javítások</button>'+
                                        '<button class="customButton">Aktív szolgáltatásaim</button>'+
                                    '</div>'+
                                '</div>'+
                                '<div class="myCarButtons">';
                    
                    //Feltölti a szolgáltatások tömböt, hogy ezt később ne kelljen lekérni
                    var service = response['services'][car['rendszam']];
                    //Ha van aktív szolgáltatása a felhasználónak, akkor elhelyez egy Lemondás gombot
                    if(service != null && service.length > 0){
                        carHtml += '<button class="cancelService customButton">Lemondás</button>';
                    }

                    carHtml += '<button class="modifyLicenseNumber customButton">Rendszám módosítása</button>'+
                                '<button class="deleteCar customButton">Törlés</button>'+
                               '</div>'+
                            '</div>';
                    $('#myCars').html($('#myCars').html() + carHtml);
                })
            }
            else {
                $('#myCars').html('<h1>Adja hozzá első autóját!</h1>');
            }

            //Létrehozza az új autó létrehozásához az űrlapot
            var newCarForm = '<form class="customCard" id="newCar" method="POST">'+
                                '<h2>Új autó</h2>'+
                                '<fieldset>'+
                                    '<label for="rendszam">Rendszám</label>'+
                                    '<input class="customInput" type="text" name="rendszam" id="rendszam">'+

                                    '<label for="marka">Márka</label>'+
                                    '<select class="customSelect" name="marka" id="marka"></select>'+

                                    '<label for="tipus">Típus</label>'+
                                    '<select class="customSelect" name="tipus" id="tipus"><option value="valasszon">Válasszon egy típust!</option></select>'+

                                    '<label for="elozo_javitasok">Előző javítások</label>'+
                                    '<input class="customInput" type="text" name="elozo_javitasok" id="elozo_javitasok">'+
                                '</fieldset>'+
                                '<p id="error_message" hidden></p>'+
                                '<button class="customButton" id="newCarBtn" onclick="addCar(event)">Új Autó</button>'+
                            '</form>';

            $('#myCars').html($('#myCars').html() + newCarForm);
            allCars = $('#myCars').html();

            loadCarBrands(); //Betölti az összes autómárkát
        },
        error: function(xhr){
            $('main h1').remove();
            $('#myCarsSearchBar').remove();
            $('#myCars').html('<h1>Autói megtekintéséhez <a href="'+BASE_URL+'public/pages/register.html">regisztráljon</a>, vagy <a href="'+BASE_URL+'public/pages/login.html">jelentkezzen be</a>!</h1>');
            $('#newCar').html("");
        }
    });
}

//Betölti az összes autómárkát
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

//Létrehozza a felhasználó új autóját
function addCar(event){
    event.preventDefault();

    const data = {
        userId: getUserCredentials().userId,
        rendszam: $('#rendszam').val(),
        marka: $('#marka').val(),
        tipus: $('#tipus').val(),
        elozo_javitasok: $('#elozo_javitasok').val()
    };

    //Ellenőrzi, hogy a szükséges mezők megfelelően ki vannak e töltve
    if(data["marka"] != "valasszon" && data["tipus"] != "valasszon" && data["rendszam"] != ""){
        $.ajax({
            type: "POST",
            url: BASE_URL + "routes/api.php/users/"+getUserCredentials().userId+"/cars",
            data: data,
            dataType: "json",
            success: function (response) {
                if(response.success){
                    openCustomModal("Sikeres hozzáadás", response.message);
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
            $('#error_message').removeAttr('hidden');
            $('#error_message').html("Adjon meg egy rendszámot!");
        }
        else if(data["marka"] == "valasszon"){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html("Válasszon egy márkát!");
        }
        else if(data["tipus"] == "valasszon"){
            $('#error_message').removeAttr('hidden');
            $('#error_message').html("Válasszon egy típust!");
        }
    }
};

let allCars = null;

//Elvégzi a keresést az autók között, szóközzel elválasztva több szóra lehet keresni
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

    if($('#myCarsSearchBar').val() == ""){
        $('#myCars').html(allCars)
    }

    loadCarBrands();
});

//Elvégzi az autók szűrését
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
