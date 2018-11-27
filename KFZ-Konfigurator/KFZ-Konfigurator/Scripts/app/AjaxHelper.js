/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */
function saveViewModel(url, id, antiForgeryToken, additionalData) {
    let i;
    /** @type {Object} */
    let dataObject = {};

    if (id) {
        dataObject.id = id;
    }
    if (additionalData) {
        for (i in additionalData) {
            if (additionalData.hasOwnProperty(i)) {
                dataObject[i] = additionalData[i];
            }
        }
    }
    if (antiForgeryToken) {
        dataObject.__RequestVerificationToken = antiForgeryToken;
    }
    return $.ajax({
        type: 'Post',
        url: url,
        data: dataObject,
        contentType: 'application/x-www-form-urlencoded'
    });
}
/**
 * @param {Document} document
 * @returns {string}
 */
function getAntiForgeryToken(document) {
    return document.find('[name='__RequestVerificationToken']').val();
}