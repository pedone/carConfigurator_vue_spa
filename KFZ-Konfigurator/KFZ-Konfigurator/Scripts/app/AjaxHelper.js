/**
 * @param {string} url
 * @param {string|Object} data
 * @param {string} antiForgeryToken
 * @returns {jqXHR}
 */
function saveViewModel(url, data, antiForgeryToken) {
    var i;
    /** @type {Object} */
    var dataObject = {};

    if (typeof data === 'object') {
        for (i in data) {
            if (data.hasOwnProperty(i)) {
                dataObject[i] = data[i];
            }
        }
    } else {
        dataObject.data = data;
    }
    if (antiForgeryToken) {
        dataObject.__RequestVerificationToken = antiForgeryToken;
    }
    return $.ajax({
        type: "Post",
        url: url,
        data: dataObject,
        contentType: "application/x-www-form-urlencoded"
    });
}
/**
 * @param {Document} document
 * @returns {string}
 */
function getAntiForgeryToken(document) {
    return document.find("[name='__RequestVerificationToken']").val();
}