function initMap() {
    // Verifica se o navegador suporta geolocaliza��o
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const userLocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };

                const estiloMapa = [
                    {
                        featureType: "poi",
                        elementType: "all",
                        stylers: [{ visibility: "off" }]
                    }
                ];

                const map = new google.maps.Map(document.getElementById("map"), {
                    center: userLocation,
                    zoom: 15,
                    styles: estiloMapa,
                    streetViewControl: false,
                    mapTypeControl: false,
                });

                // Adiciona um marcador na localiza��o do usu�rio
                addMarker(userLocation, map, "Voc� est� aqui");

                // Busca os pets salvos no banco e os exibe no mapa
                fetch('/Map/GetSavedPets')
                    .then(response => response.json())
                    .then(pets => {
                        pets.forEach(pet => {
                            const petLocation = { lat: pet.Latitude, lng: pet.Longitude };
                            addMarker(petLocation, map, pet.Description || "Pet encontrado");
                        });
                    })
                    .catch(error => console.error("Erro ao buscar os pets:", error));
            },
            (error) => {
                showError(error);
                const defaultLocation = { lat: -23.550520, lng: -46.633308 };
                const map = new google.maps.Map(document.getElementById("map"), {
                    center: defaultLocation,
                    zoom: 12,
                    styles: estiloMapa,
                    streetViewControl: false,
                    mapTypeControl: false,
                });
            },
            {
                enableHighAccuracy: true,
                timeout: 5000,
                maximumAge: 0
            }
        );
    } else {
        alert("Geolocation is not supported by this browser.");
        const defaultLocation = { lat: -23.550520, lng: -46.633308 };
        const map = new google.maps.Map(document.getElementById("map"), {
            center: defaultLocation,
            zoom: 12,
            styles: estiloMapa,
            streetViewControl: false,
            mapTypeControl: false,
        });
    }

    // Fun��o para adicionar um marcador no mapa
    function addMarker(location, map, title) {
        new google.maps.Marker({
            position: location,
            map: map,
            title: title
        });
    }

    // Fun��o para tratar erros de geolocaliza��o
    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                alert("Usu�rio negou a solicita��o de geolocaliza��o.");
                break;
            case error.POSITION_UNAVAILABLE:
                alert("Informa��es de localiza��o indispon�veis.");
                break;
            case error.TIMEOUT:
                alert("A solicita��o de localiza��o expirou.");
                break;
            case error.UNKNOWN_ERROR:
                alert("Ocorreu um erro desconhecido.");
                break;
        }
    }
}

// Certifique-se de que a fun��o initMap est� dispon�vel globalmente
window.initMap = initMap;
