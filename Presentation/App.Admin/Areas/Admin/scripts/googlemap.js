$(function () {
    var map;
    var geocoder;
    var lat, lng;
    var glat = document.getElementById("Lat").value;
    var glag = document.getElementById("Lag").value;
    document.getElementById("Lat").value = glat;
    document.getElementById("Lag").value = glag;
    lat = glat;
    lng = glag;

    function initialize() {
        var myLatlng = new google.maps.LatLng(10.777662144173254, 106.67312622070312);
        if (lat != "" && lng != "")
            myLatlng = new google.maps.LatLng(lat, lng);
        var mapOptions = {
            zoom: 16,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);


        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            // icon: image,
            draggable: false,
            animation: google.maps.Animation.DROP
        });

        var location = map.getCenter();
        document.getElementById("Lat").value = location.lat();
        document.getElementById("Lag").value = location.lng();
        geocoder = new google.maps.Geocoder();
        var infowindow = new google.maps.InfoWindow();
        google.maps.event.addListener(marker, "dragstart", function () {
            infowindow.close();
        });

        google.maps.event.addListener(marker, "dragend", function (event) {
            document.getElementById("Lat").value = event.latLng.lat();
            document.getElementById("Lag").value = event.latLng.lng();
        });


    }

    function showAddress() {
        if (lat == "" && lng == "") {
            map.overlayMapTypes.setAt(0, null);
            var myLatlng = new google.maps.LatLng(10.777662144173254, 106.67312622070312);
            var mapOptions = {
                zoom: 16,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                // icon: image,
                draggable: false,
                animation: google.maps.Animation.DROP
            });

            var location = map.getCenter();
            document.getElementById("Lat").value = location.lat();
            document.getElementById("Lag").value = location.lng();

            var infowindow = new google.maps.InfoWindow();
            google.maps.event.addListener(marker, "dragstart", function () {
                infowindow.close();
            });

            google.maps.event.addListener(marker, "dragend", function (event) {
                document.getElementById("Lat").value = event.latLng.lat();
                document.getElementById("Lag").value = event.latLng.lng();
            });
        } else {
            // var strAdress = $("#GAddress").val();
            var infowindow = new google.maps.InfoWindow({
                content: strAdress,
                maxWidth: 400
            });
            //var allMapTypes = [MapTypeId.ROADMAP, MapTypeId.SATELLITE, MapTypeId.HYBRID, MapTypeId.TERRAIN];
            var myLatlng = new google.maps.LatLng(lat, lng);
            var mapOptions = {
                zoom: 16,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            // var image = '<%=ResolveUrl("~/AdminCP/images/icons/hotel_marker.png")%>';

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                // icon: image,
                draggable: false,
                animation: google.maps.Animation.DROP
            });

            google.maps.event.addListener(marker, "click", function () {
                infowindow.open(map, marker);
            });
        }

    }
     
    $("#btnSearchAddress").click(function () {
        searcMap();
    });
    $("#findAddress").keypress(function(event) {
        if (event.keyCode==13) {
            searcMap();
        }
    })

    function  searcMap() {
        var address = document.getElementById("findAddress").value;
        var latlng;
        geocoder.geocode({ 'address': address }, function (results, status) {
            var infowindow = new google.maps.InfoWindow({ maxWidth: 400 });
            if (status == google.maps.GeocoderStatus.OK) {
                latlng = results[0].geometry.location;
                if (latlng == null) {
                    alert("Không tìm thấy: " + address);

                } else {
                    document.getElementById("Lat").value = latlng.lat();
                    document.getElementById("Lag").value = latlng.lng();
                    map.setCenter(results[0].geometry.location);
                    marker = new google.maps.Marker({
                        map: map,
                        setMap: map,
                        position: results[0].geometry.location,
                        draggable: true,
                        animation: google.maps.Animation.DROP
                    });
                    // marker.setMap(Map);
                    var infowindow = new google.maps.InfoWindow({ content: "Tìm thấy: " + address, maxWidth: 500 });
                    infowindow.open(map, marker);

                    google.maps.event.addListener(marker, "dragstart", function () {
                        infowindow.close();
                    });
                    google.maps.event.addListener(marker, "dragend", function (event) {
                        document.getElementById("Lat").value = event.latLng.lat();
                        document.getElementById("Lag").value = event.latLng.lng();
                    });
                }
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    }

    google.maps.event.addDomListener(window, "load", initialize);
    google.maps.event.addDomListener(window, "load", showAddress);
});

function LocationFinishedClose() {
    document.getElementById("Lat").value = document.getElementById("Lat").value;
    document.getElementById("Lag").value = document.getElementById("Lag").value;
    window.opener.document.getElementById("Lat").value = document.getElementById("Lat").value;
    window.opener.document.getElementById("Lag").value = document.getElementById("Lag").value;
    window.close();
}