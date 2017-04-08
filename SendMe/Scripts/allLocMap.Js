
var centerPosAll = { lat: 33.980796, lng: -80.962908 };
var posAll1 = { lat: 33.980796, lng: -80.962908 };
var pinColorAll = '1abbac';
var pinIconAll = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColorAll,
    new google.maps.Size(21, 34),
    new google.maps.Point(0, 0),
    new google.maps.Point(10, 34));

var pinColorAll2 = '75000b';
var pinIconAll2 = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColorAll2,
    new google.maps.Size(21, 34),
    new google.maps.Point(0, 0),
    new google.maps.Point(10, 34));

var contentStringAll2 =
      '<div id="siteNotice">' +
      '<img width="150" height="100" src="../../img/USCGlobalHealthMapImage.jpg" class="img-responsive inline-block" alt="Responsive image" />' +
      '</div>';


///Call the map
var mapAll = new google.maps.Map(document.getElementById('mapAll'), {
    center: centerPosAll,
    zoom: 2,
    mapTypeId: 'hybrid',
    scrollwheel: false,
    disableDefaultUI: true,
});
var infowindowAll = new google.maps.InfoWindow({
    content: contentStringAll2
});

//First marker
markerAll1 = new google.maps.Marker({
    map: mapAll,
    draggable: false,
    animation: google.maps.Animation.DROP,
    position: posAll1,
    icon: pinIconAll2,
});
markerAll1.addListener('click', function () {
    infowindowAll.open(mapAll, markerAll1);
});

for (var i = 0, len = destinations.length; i < len; i++) {
    address = destinations[i];
    ///Geo loc for all non school positions
    var geocoderAll = new google.maps.Geocoder();
    geocoderAll.geocode({
        'address': address
    },

    ///Second Marker
    function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            markerAll2 = new google.maps.Marker({
                map: mapAll,
                position: results[0].geometry.location,
                animation: google.maps.Animation.DROP,
                draggable: false,
                icon: pinIconAll,
            });
 

            var geolocLat = results[0].geometry.location.lat();
            var geolocLng = results[0].geometry.location.lng();
            var polylinesLoc = [
            { lat: geolocLat, lng: geolocLng },
            { lat: 33.980796, lng: -80.962908 },
            ];

            ///The Lines
            var polyline = new google.maps.Polyline({
                path: polylinesLoc,
                geodesic: true,
                strokeColor: '#730023',
                strokeOpacity: 1,
                strokeWeight: 2
            });

            polyline.setMap(mapAll);
        }
    })

}

////
