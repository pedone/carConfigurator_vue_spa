export function getCarModelList() {
    return fetch('/Data/CarModelList', {
        method: 'get'
    }).then(function (response) {
        return response.json();
    });
};