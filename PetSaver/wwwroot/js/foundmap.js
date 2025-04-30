function initMap() {
    // Localização padrão (São Paulo)
    const defaultLocation = { lat: -23.55052, lng: -46.633308 };

    // Criação do mapa
    const map = new google.maps.Map(document.getElementById("map"), {
        center: defaultLocation,
        zoom: 12,
    });

    // Adiciona um marcador no mapa
    new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: "Localização padrão",
    });
}

window.initMap = initMap;