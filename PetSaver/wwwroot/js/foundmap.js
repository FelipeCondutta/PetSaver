let map;
let marker;

function initMap() {
    // Coordenadas padrão (São Paulo)
    let defaultCenter = { lat: -23.55052, lng: -46.633308 };
    let zoomLevel = 12;

    map = new google.maps.Map(document.getElementById("map"), {
        center: defaultCenter,
        zoom: zoomLevel,
    });

    // Tenta obter a localização do usuário
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                const userLocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                map.setCenter(userLocation);
                map.setZoom(15);
            },
            function () {
                // Se o usuário negar, mantém o centro padrão
            }
        );
    }

    // Adiciona um marcador ao clicar no mapa
    map.addListener("click", (event) => {
        const location = event.latLng;

        if (marker) {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true,
            });
        }

        document.getElementById("latitude").value = location.lat();
        document.getElementById("longitude").value = location.lng();
    });
}
