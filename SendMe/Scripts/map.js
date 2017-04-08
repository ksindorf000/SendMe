//Start Location
var contentString1 =
      '<div id="siteNotice">' +
      '<img width="150" height="100" src="../../img/USCGlobalHealthMapImage.jpg" class="img-responsive inline-block" alt="Responsive image" />' +
      '</div>';
//lat long
var address = currentCountry +", "+ currentCity;
var centerPos = { lat: 14.5994, lng: -28.6731 };
var pos1 = { lat: 33.980796, lng: -80.962908 };
var pos2;
var plane;

//Create map

    var map = new google.maps.Map(document.getElementById('map'), {
        center: centerPos,
        zoom: 2,
        mapTypeId: 'hybrid',
        scrollwheel: false,
        disableDefaultUI: true
    });
    var infowindow = new google.maps.InfoWindow({
        content: contentString1

    });
    var pinColor = '98012e';
    var pinIcon = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor,
        new google.maps.Size(21, 34),
        new google.maps.Point(0,0),
        new google.maps.Point(10, 34));

        //Drop Marker 1
    marker1 = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: pos1,
        icon: pinIcon,
    });
    marker1.addListener('click', function () {
        infowindow.open(map, marker1);
    });

    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'address': address
    },
    function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            marker2 = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
                animation: google.maps.Animation.DROP,
                draggable: false,
                icon: pinIcon,
            });
        }
        var latString = results[0].geometry.location.lat().toString();
        var lngString = results[0].geometry.location.lng().toString();
        var lat = Number(latString)
        var lng = Number(lngString)
        pos2 = { lat: lat, lng: lng };

            plane = new google.maps.Polyline({
            path: [pos1, pos2],
            icons: [{
                icon: planeSymbol,
                offset: '100%',
                strokeWeight: 3,
                fillOpacity: 3,
                geodesic: true
            }],
            map: map
        });

        animate(plane);
    });

    //The Plane
    var planeSymbol = {
        path: 'M22.1,15.1c0,0-1.4-1.3-3-3l0-1.9c0-0.2-0.2-0.4-0.4-0.4l-0.5,0c-0.2,0-0.4,0.2-0.4,0.4l0,0.7c-0.5-0.5-1.1-1.1-1.6-1.6l0-1.5c0-0.2-0.2-0.4-0.4-0.4l-0.4,0c-0.2,0-0.4,0.2-0.4,0.4l0,0.3c-1-0.9-1.8-1.7-2-1.9c-0.3-0.2-0.5-0.3-0.6-0.4l-0.3-3.8c0-0.2-0.3-0.9-1.1-0.9c-0.8,0-1.1,0.8-1.1,0.9L9.7,6.1C9.5,6.2,9.4,6.3,9.2,6.4c-0.3,0.2-1,0.9-2,1.9l0-0.3c0-0.2-0.2-0.4-0.4-0.4l-0.4,0C6.2,7.5,6,7.7,6,7.9l0,1.5c-0.5,0.5-1.1,1-1.6,1.6l0-0.7c0-0.2-0.2-0.4-0.4-0.4l-0.5,0c-0.2,0-0.4,0.2-0.4,0.4l0,1.9c-1.7,1.6-3,3-3,3c0,0.1,0,1.2,0,1.2s0.2,0.4,0.5,0.4s4.6-4.4,7.8-4.7c0.7,0,1.1-0.1,1.4,0l0.3,5.8l-2.5,2.2c0,0-0.2,1.1,0,1.1c0.2,0.1,0.6,0,0.7-0.2c0.1-0.2,0.6-0.2,1.4-0.4c0.2,0,0.4-0.1,0.5-0.2c0.1,0.2,0.2,0.4,0.7,0.4c0.5,0,0.6-0.2,0.7-0.4c0.1,0.1,0.3,0.1,0.5,0.2c0.8,0.2,1.3,0.2,1.4,0.4c0.1,0.2,0.6,0.3,0.7,0.2c0.2-0.1,0-1.1,0-1.1l-2.5-2.2l0.3-5.7c0.3-0.3,0.7-0.1,1.6-0.1c3.3,0.3,7.6,4.7,7.8,4.7c0.3,0,0.5-0.4,0.5-0.4S22,15.3,22.1,15.1z',
        fillColor: '#000',
        fillOpacity: 8,
        scale: 1.4,
        anchor: new google.maps.Point(11, 11),
        strokeWeight: 0
    };
    //The Line
    

//The animation
function animate(plane) {
    var count = 0;
    window.setInterval(function () {
        count = (count + 1) % 200;

        var icons = plane.get('icons');
        icons[0].offset = (count / 2) + '%';
        plane.set('icons', icons);
    }, 100);
}


///////All Maps Loctions

var centerPosAll = { lat: 14.5994, lng: -28.6731 };
var posAll1 = { lat: 33.980796, lng: -80.962908 };
var pinColorAll = '98012e';
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


    

