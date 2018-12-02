'use strict';

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
    /**
     * @param {Array.<OrderData>} data
     * @param {number} pageCount
     */
    constructor(data, pageCount) {
        let self = this;
        /** @type {KnockoutObservableArrayStatic} */
        this.orders = ko.observableArray(_.map(data, (cur) => new OrderItemViewModel(cur)));
        /** @type {Array.<number>} */
        this.pages = [];
        _.times(pageCount, (index) => this.pages.push(index + 1));
        /** @type {number} */
        this.currentPageIndex = 0;

        /**
         * Notes: this method has to be placed within the constructor, because self.orders is not returning the
         * KnockoutObservableArrayStatic object outside the constructor, but the function that knockout is generating.
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

        /** @param {number} number */
        this.loadPage = function (number) {
            /** @type {number} */
            let targetIndex = number - 1;

            if (targetIndex === this.currentPageIndex) {
                return;
            }

            $.ajax({
                type: 'GET',
                url: '/OrderList/LoadPage',
                data: { pageIndex: targetIndex },
            }).done(function (data) {
                self.orders(_.map(data, (cur) => new OrderItemViewModel(cur)));
            }).fail(function (error) {
                console.log('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
            });
        };
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