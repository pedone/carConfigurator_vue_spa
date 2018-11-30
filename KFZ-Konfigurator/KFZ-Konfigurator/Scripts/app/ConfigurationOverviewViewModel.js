class ConfigurationOverviewViewModel {
    /**
     * @param {string} configurationUrl
     * @param {HandlerOptions} handlerOptions
     */
    constructor(configurationUrl, handlerOptions) {
        self = this;
        /** @type {string} */
        this.configurationLinkUrl = ko.observable(configurationUrl || "");
        /** @type {string} */
        this.configurationName = ko.observable("");
        /** @type {HandlerOptions} */
        this.options = handlerOptions;
    }

    generateConfigurationLink() {
        $.ajax({
            type: 'POST',
            url: '/ConfigurationOverview/GenerateConfigurationLink',
            data: { __RequestVerificationToken: self.options.antiForgeryToken, configurationName: self.configurationName() },
            contentType: 'application/x-www-form-urlencoded',
        }).done(function (data) {
            self.configurationLinkUrl(data);
        }).fail(function (error) {
            console.log('failed to generate configuration link: ' + error.responseText + ' (' + error.statusText + ')');
        });
    }

    placeOrder() {
        $.ajax({
            type: 'POST',
            url: '/ConfigurationOverview/PlaceOrder',
            data: { __RequestVerificationToken: self.options.antiForgeryToken, configurationName: self.configurationName() },
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'text'
        }).done(function (data) {
            self.configurationLinkUrl(data);
            if (self.options.placeOrderSuccess) {
                self.options.placeOrderSuccess();
            }
        })
        .fail(
            function (error) {
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