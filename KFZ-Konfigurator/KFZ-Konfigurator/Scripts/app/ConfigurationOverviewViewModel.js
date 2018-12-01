class ConfigurationOverviewViewModel {
    /**
     * @param {string} configurationUrl
     * @param {HandlerOptions} options
     */
    constructor(configurationUrl, options) {
        self = this;
        /** @type {string} */
        this.configurationLink = ko.observable(configurationUrl || "");
        /** @type {string} */
        this.configurationDescription = ko.observable("");
        /** @type {HandlerOptions} */
        this.options = options;
    }

    placeOrder() {
        $.ajax({
            type: 'POST',
            url: '/ConfigurationOverview/PlaceOrder',
            data: { __RequestVerificationToken: self.options.antiForgeryToken, description: self.configurationDescription() },
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'text'
        }).done(
            function (data) {
                self.configurationLink(data);
                if (self.options.placeOrderSuccess) {
                    self.options.placeOrderSuccess(data);
                }
            })
            .fail(function (error) {
                console.log('failed to place order: ' + error.responseText + ' (' + error.statusText + ')');
                if (self.options.placeOrderFailure) {
                    self.options.placeOrderFailure();
                }
            });
    }
}

/**
 * @typedef {Object} HandlerOptions
 * @property {string} antiForgeryToken
 * @property {Function|null} placeOrderSuccess
 * @property {Function|null} placeOrderFailure
 */