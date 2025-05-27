let map;
let marker;

function initMap() {
    // Inicializa o mapa
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -23.55052, lng: -46.633308 }, // Coordenadas iniciais (São Paulo)
        zoom: 12,
    });

    // Adiciona um marcador ao clicar no mapa
    map.addListener("click", (event) => {
        const location = event.latLng;

        // Se já existir um marcador, mova-o
        if (marker) {
            marker.setPosition(location);
        } else {
            // Cria um novo marcador
            marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true, // Permite arrastar o marcador
            });
        }

        // Atualiza os campos ocultos com a latitude e longitude
        document.getElementById("latitude").value = location.lat();
        document.getElementById("longitude").value = location.lng();
    });
}
