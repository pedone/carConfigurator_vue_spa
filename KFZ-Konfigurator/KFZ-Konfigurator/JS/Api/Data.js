export function getCarModelList() {
    return fetch('/Data/CarModelList', {
        method: 'get'
    }).then(function (response) {
        return response.json();
    });
};

export function getConfigurationData(carModelId) {
    return fetch('/Data/ConfigurationData?carModelId=' + carModelId, {
        method: 'get',
    }).then(function (response) {
        return response.json();
    });
}