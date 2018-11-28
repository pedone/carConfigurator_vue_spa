class ConfigurationOverviewViewModel {
    /**
     * @param {string} configurationUrl
     */
    constructor(configurationUrl) {
        /** @type {string} */
        this.configurationLinkUrl = ko.observable(configurationUrl || "");
        /** @type {string} */
        this.configurationName = ko.observable("");
    }
}