function initMap() {
    // Verifica se o navegador suporta geolocaliza��o
    if (navigator.geolocation) {
        // Solicita a localiza��o do usu�rio com alta precis�o
        navigator.geolocation.getCurrentPosition(
            (position) => {
                // Define a localiza��o do usu�rio como o centro do mapa
                const userLocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };

                // Estilo do mapa para remover pontos de interesse (POIs)
                const estiloMapa = [
                    {
                        featureType: "poi",
                        elementType: "all",
                        stylers: [{ visibility: "off" }] // Remove todos os locais (lojas, parques, hospitais, etc.)
                    }
                ];

                // Cria o mapa com a localiza��o do usu�rio como centro
                const map = new google.maps.Map(document.getElementById("map"), {
                    center: userLocation,
                    zoom: 15, // Aumenta o zoom para uma vis�o mais pr�xima
                    styles: estiloMapa, // Aplica o estilo personalizado
                    streetViewControl: false, // Remove o Pegman (Street View)
                    mapTypeControl: false, // Desativa controle de tipos de mapas
                });

                // Adiciona um marcador na localiza��o do usu�rio
                addMarker(userLocation, map);
            },
            (error) => {
                // Caso ocorra um erro ao obter a localiza��o, exibe uma mensagem de erro
                showError(error);
                // Define uma localiza��o padr�o (ex: S�o Paulo) como fallback
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
                enableHighAccuracy: true, // Solicita alta precis�o
                timeout: 5000, // Tempo m�ximo de espera para obter a localiza��o
                maximumAge: 0 // N�o usa cache de localiza��o
            }
        );
    } else {
        // Caso o navegador n�o suporte geolocaliza��o, exibe uma mensagem de erro
        alert("Geolocation is not supported by this browser.");
        // Define uma localiza��o padr�o (ex: S�o Paulo) como fallback
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
    function addMarker(location, map) {
        new google.maps.Marker({
            position: location,
            map: map,
            title: "Localiza��o Atual"
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
