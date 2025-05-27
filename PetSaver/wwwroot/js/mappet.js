function initMap() {
    // Verifica se o navegador suporta geolocalização
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

                // Adiciona um marcador na localização do usuário
                addMarker(userLocation, map, "Você está aqui");

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

    // Função para adicionar um marcador no mapa
    function addMarker(location, map, title) {
        new google.maps.Marker({
            position: location,
            map: map,
            title: title
        });
    }

    // Função para tratar erros de geolocalização
    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                alert("Usuário negou a solicitação de geolocalização.");
                break;
            case error.POSITION_UNAVAILABLE:
                alert("Informações de localização indisponíveis.");
                break;
            case error.TIMEOUT:
                alert("A solicitação de localização expirou.");
                break;
            case error.UNKNOWN_ERROR:
                alert("Ocorreu um erro desconhecido.");
                break;
        }
    }
}

// Certifique-se de que a função initMap está disponível globalmente
window.initMap = initMap;
