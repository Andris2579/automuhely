import { BASE_URL } from "./main.js";

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
                            '<button class="book"><a href="#selected_service">Foglalás</a></button>'+
                        '</div>';
            });
            $('#services').html($('#services').html() + text);
        }
    });
}

window.fetchServices = fetchServices;

let selected_div = null;

$(document).on('click', '.book', function(){
    let service_divs = $('#services .service');
    selected_div = $(this).closest('.service');
    var selected_div_id = null;

    service_divs.each(function(index, element){
        if($(element).find('p').html() == selected_div.find('p').html()){
            selected_div_id = index;
            alert(index);
        }
    })

    let selected_service = $('#selected_service');
    selected_service.removeAttr('hidden');
    selected_service.html('<h1>Kiválasztott szolgáltatás</h1>' + selected_div.html());
    selected_service.find('.book').remove();
    var d = new Date();
    var date_input = '<label for="date">Időpont: </label><input type="date" id="date" name="date" value="'+d.getFullYear() + "-" + (d.getMonth()+1) + "-" + d.getDate()+'">';
    selected_service.html(selected_service.html() + date_input)
});