window.startWatchingPosition = (dotNetObject) => {
    const watchId = navigator.geolocation.watchPosition((position) => {
        dotNetObject.invokeMethodAsync('OnPositionUpdated', {
            latitude: position.coords.latitude,
            longitude: position.coords.longitude,
            altitude: position.coords.altitude,
            speed: position.coords.speed,
            // add more properties here if you want
        });
    }, (error) => {
        dotNetObject.invokeMethodAsync('OnPositionError', error.code);
    }, {
        enableHighAccuracy: true,
    });

    return watchId;
};

window.stopWatchingPosition = (watchId) => {
    navigator.geolocation.clearWatch(watchId);
};
