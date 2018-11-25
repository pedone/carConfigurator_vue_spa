/**
 * @param {string} url
 * @param {ViewModel|Array.<ViewModel>} viewModel
 * @param {string} antiForgeryToken
 * @returns {jqXHR}
 */
function saveViewModel(url, viewModel, antiForgeryToken) {
    //TODO handle viewModel list
    /** @type {Object} */
    var data = {
        id: viewModel.id
    };
    if (antiForgeryToken) {
        data.__RequestVerificationToken = antiForgeryToken;
    }
    return $.ajax({
        type: "Post",
        url: url,
        data: data,
        contentType: "application/x-www-form-urlencoded"
    });
}