export function getCarModelList() {
    return fetch('/Data/CarModelList', {
        method: 'get'
    }).then(function (response) {
        return response.json();
    });
};

export function getOrderList() {
    return fetch('/Data/OrderList', {
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

export function getOrderData(guid) {
    return fetch('/Data/LoadOrder?guid=' + guid, {
        method: 'get',
    }).then(function (response) {
        return response.json();
    });
}