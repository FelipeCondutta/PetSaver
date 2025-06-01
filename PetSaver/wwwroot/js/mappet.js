// Adicione a constante aqui, no início do arquivo
const DEFAULT_LOCATION = { lat: -23.550520, lng: -46.633308 };

// Função initMap
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
                    .then(response => {
                        if (!response.ok) {
                            throw new Error("Erro ao buscar os pets: " + response.statusText);
                        }
                        return response.json();
                    })
                    .then(pets => {
                        pets.forEach(pet => {
                            if (pet.latitude && pet.longitude) {
                                const petLocation = { lat: pet.latitude, lng: pet.longitude };
                                const infoContent = `
                                    <div>
                                        <h3>${pet.description || "Pet encontrado"}</h3>
                                        <img src="${pet.imageUrl}" alt="Imagem do Pet" style="width:100px;height:100px;" />
                                    </div>
                                `;
                                addMarkerWithInfo(petLocation, map, pet.description || "Pet encontrado", infoContent);
                            } else {
                                console.warn("Pet sem coordenadas: ", pet);
                            }
                        });
                    })
                    .catch(error => console.error("Erro ao buscar os pets:", error));
            },
            (error) => {
                showError(error);
                // Use a constante DEFAULT_LOCATION como fallback
                const map = new google.maps.Map(document.getElementById("map"), {
                    center: DEFAULT_LOCATION,
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
        // Use a constante DEFAULT_LOCATION como fallback
        const map = new google.maps.Map(document.getElementById("map"), {
            center: DEFAULT_LOCATION,
            zoom: 12,
            styles: estiloMapa,
            streetViewControl: false,
            mapTypeControl: false,
        });
    }
}
