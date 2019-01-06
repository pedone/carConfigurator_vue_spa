export function getCarModelList() {
    return fetch('/Data/CarModelList', {
        method: 'get'
    }).then(function (response) {
        return response.json();
    });
};

//TODO separate get for data that's not dependent on carModelId

export function getConfigurationData(carModelId) {
    return fetch('/Data/ConfigurationData?carModelId=' + carModelId, {
        method: 'get',
    }).then(function (response) {
        return response.json();
    });
}