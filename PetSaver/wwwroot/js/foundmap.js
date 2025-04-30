function initMap() {
    // Localiza��o padr�o (S�o Paulo)
    const defaultLocation = { lat: -23.55052, lng: -46.633308 };

    // Cria��o do mapa
    const map = new google.maps.Map(document.getElementById("map"), {
        center: defaultLocation,
        zoom: 12,
    });

    // Adiciona um marcador no mapa
    new google.maps.Marker({
        position: defaultLocation,
        map: map,
        title: "Localiza��o padr�o",
    });
}

window.initMap = initMap;