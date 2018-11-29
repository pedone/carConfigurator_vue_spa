class ConfigurationItemViewModel {
    /**
     * @param {ConfigurationData} data
     * @param {string} linkTemplate
     */
    constructor(data, linkTemplate) {
        /** @type {number} */
        this.id = data.Id;
        /** @type {number} */
        this.price = data.Price;
        /** @type {string} */
        this.name = data.Name;
        /** @type {string} */
        this.guid = data.Guid;
        /** @type {string} */
        this.linkUrl = linkTemplate.replace("Placeholder", data.Guid);
    }
}

class ConfigurationListViewModel {
    /**
     * @param {Array.<ConfigurationData>} data
     * @param {string} linkTemplate
     */
    constructor(data, linkTemplate) {
        var self = this;

        /** @type {KnockoutObservableArrayStatic} */
        this.configurations = ko.observableArray(_.map(data, (cur) => new ConfigurationItemViewModel(cur, linkTemplate)));

        /**
         * @param {ConfigurationItemViewModel} item
         * @param {Event} event
         * @param {JQueryStatic} document
         */
        this.deleteItem = function (item, event, document) {
            event.preventDefault();

            console.log(event.currentTarget.ownerDocument);
            /** @type {string} */
            let antiForgeryToken = getAntiForgeryToken(document);
            deleteItemAjax(item.id, antiForgeryToken)
                .done(function () {
                    self.configurations.remove(item);
                })
                .fail(function () {
                    //TODO
                    alert('item ' + item.id + ' could not be removed');
                });
        };

        /**
         * @param {number} id
         * @param {string} antiForgeryToken
         * @returns {jqXHR}
         */
        function deleteItemAjax(id, antiForgeryToken) {
            return $.ajax({
                type: 'POST',
                url: '/Configurations/delete',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    id: id
                },
                contentType: 'application/x-www-form-urlencoded'
            });
        }
    }
}

/**
 * @typedef {Object} ConfigurationData
 * @property {number} Id
 * @property {number} Price
 * @property {string} Name
 * @property {string} Guid
 */