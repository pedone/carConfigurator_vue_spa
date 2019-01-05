export function getCarModelList() {
    return fetch('/Data/CarModelList', {
        method: 'get'
    }).then(function (response) {
        return response.json();
    });
};

export function getConfigurationData() {
    return fetch('/Data/ConfigurationData?carModelId=5', {
        method: 'get',
    }).then(function (response) {
        return response.json();
    });
}