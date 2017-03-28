
var centerPos = { lat: 14.5994, lng: -28.6731 };
var pos1 = { lat: 33.980796, lng: -80.962908 };
var pinColor = '1abbac';
var pinIcon = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor,
    new google.maps.Size(21, 34),
    new google.maps.Point(0, 0),
    new google.maps.Point(10, 34));

var pinColor2 = '75000b';
var pinIcon2 = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor2,
    new google.maps.Size(21, 34),
    new google.maps.Point(0, 0),
    new google.maps.Point(10, 34));
var contentString2 =
      '<div id="siteNotice">' +
      '<img width="150" height="100" src="../../img/USCGlobalHealthMapImage.jpg" class="img-responsive inline-block" alt="Responsive image" />' +
      '</div>';

window.onload = initializeAllMap;
function initializeAllMap() {
    var mapAll = new google.maps.Map(document.getElementById('mapAll'), {
        center: centerPos,
        zoom: 2,
        mapTypeId: 'hybrid',
        scrollwheel: false,
        disableDefaultUI: true,
    });
    var infowindow = new google.maps.InfoWindow({
        content: contentString2

    });
    marker1 = new google.maps.Marker({
        map: mapAll,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: pos1,
        icon: pinIcon2,
    });
    marker1.addListener('click', function () {
        infowindow.open(mapAll, marker1);
    });

    for (var i = 0, len = destinations.length; i < len; i++) {
        address = destinations[i];

        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({
            'address': address
        },
        function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                marker2 = new google.maps.Marker({
                    map: mapAll,
                    position: results[0].geometry.location,
                    animation: google.maps.Animation.DROP,
                    draggable: false,
                    icon: pinIcon,
                });
            }

        })
    }

}
