/**
 * @param {string} url
 * @param {string} data
 * @param {string} antiForgeryToken
 * @returns {jqXHR}
 */
function saveViewModel(url, data, antiForgeryToken) {
    //TODO handle viewModel list
    /** @type {Object} */
    var dataObject = {
        data: data
    };
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