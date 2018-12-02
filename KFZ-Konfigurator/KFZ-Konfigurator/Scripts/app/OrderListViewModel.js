class OrderItemViewModel {
    /** @param {OrderData} data */
    constructor(data) {
        /** @type {number} */
        this.id = data.Id;
        /** @type {number} */
        this.basePrice = data.BasePrice;
        /** @type {number} */
        this.extrasPrice = data.ExtrasPrice;
        /** @type {number} */
        this.price = data.Price;
        /** @type {string} */
        this.description = data.Description;
        /** @type {string} */
        this.model = data.Model;
        /** @type {string} */
        this.guid = data.Guid;
        /** @type {string} */
        this.linkUrl = data.LinkUrl;
    }
}

class OrderListViewModel {
    /** @param {Array.<OrderData>} data */
    constructor(data) {
        var self = this;
        /** @type {KnockoutObservableArrayStatic} */
        this.orders = ko.observableArray(_.map(data, (cur) => new OrderItemViewModel(cur)));

        /**
         * @param {OrderItemViewModel} item
         * @param {Event} event
         * @param {JQueryStatic} document
         */
        this.deleteItem = function (item, event, document) {
            event.preventDefault();

            /** @type {string} */
            let antiForgeryToken = getAntiForgeryToken(document);
            deleteItemAjax(item.id, antiForgeryToken)
                .done(function () {
                    self.orders.remove(item);
                })
                .fail(function (error) {
                    console.log('failed to delete order: ' + error.responseText + ' (' + error.statusText + ')');
                    alert('order ' + item.name + ' could not be removed');
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
                url: '/OrderList/delete',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    id: id
                },
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'text'
            });
        }
    }
}

/**
 * @typedef {Object} OrderData
 * @property {number} Id
 * @property {number} ExtrasPrice
 * @property {number} BasePrice
 * @property {number} Price
 * @property {string} Description
 * @property {string} Model
 * @property {string} Guid
 * @property {string} LinkUrl
 */