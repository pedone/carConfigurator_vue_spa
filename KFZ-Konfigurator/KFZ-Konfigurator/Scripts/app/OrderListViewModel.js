'use strict';

/**
 * @param {OrderData} data
 * @returns {OrderItemViewModel}
 */
function buildOrderItemViewModel(data) {
    /** @type {ViewModel} */
    return {
        id: data.Id,
        basePrice: data.BasePrice,
        extrasPrice: data.ExtrasPrice,
        price: data.Price,
        description: data.Description,
        dateTime: data.DateTime,
        model: data.Model,
        guid: data.Guid,
        linkUrl: data.LinkUrl
    };
}

/**
 * @param {Array.<OrderData>} data
 * @param {number} pageCount
 */
export function buildVueMixin(data, pageCount) {
    return {
        data: {
            orders: _.map(data, (cur) => buildOrderItemViewModel(cur)),
            pages: [],
            currentPageIndex: 0
        },
        created: function () {
            let self = this;
            _.times(pageCount, (index) => this.pages.push(index + 1));

            /**
             * @param {number} id
             * @param {string} antiForgeryToken
             * @returns {jqXHR}
             */
            this._deleteItemAjax = function (id, antiForgeryToken) {
                return $.ajax({
                    type: 'POST',
                    url: '/OrderList/delete',
                    data: {
                        __RequestVerificationToken: antiForgeryToken,
                        id: id,
                        pageIndex: self.currentPageIndex
                    },
                    contentType: 'application/x-www-form-urlencoded',
                    dataType: 'json'
                });
            };
        },
        methods: {
            /** @param {number} itemId */
            deleteItem: function (itemId) {
                let self = this;
                var item = _.find(this.orders, (cur) => cur.id == itemId);
                if (!item) {
                    console.error('order ' + itemId + ' not found');
                    return;
                }

                this._deleteItemAjax(itemId, this._antiForgeryToken)
                    .done(
                        /** @param {{NewPageCount: number, NewItem: OrderData }} data */
                        function (data) {
                            console.debug('removing order ' + itemId + ' from view');
                            self.orders.splice(self.orders.indexOf(item), 1);
                            if (data.NewItem) {
                                //insert the item that has moved up to the current page
                                self.orders.push(buildOrderItemViewModel(data.NewItem));
                            }
                            if (self.pages.length !== data.NewPageCount) {
                                console.debug('page count changed from ' + self.pages.length + ' to ' + data.NewPageCount);
                                //update the max page count
                                self.pages.pop();

                                // if the current page is larger than the max pages, gotta load the previous page
                                /** @type {number} */
                                let curPageNumber = self.currentPageIndex + 1;
                                if (curPageNumber > self.pages.length) {
                                    self.loadPage(curPageNumber - 1);
                                }
                            }
                        })
                    .fail(function (error) {
                        console.error('failed to delete order: ' + error.responseText + ' (' + error.statusText + ')');
                        console.debug(JSON.stringify(error));
                        alert('order ' + item.name + ' could not be removed');
                    });
            },
            /** @param {number} number */
            loadPage: function (number) {
                let self = this;
                /** @type {number} */
                let targetIndex = number - 1;
                if (targetIndex === this.currentPageIndex) {
                    return;
                }

                $.ajax({
                    //make sure the data is saved before the next page is loaded
                    async: false,
                    type: 'GET',
                    url: '/OrderList/LoadPage',
                    data: { pageIndex: targetIndex },
                    dataType: 'json'
                }).done(function (data) {
                    console.debug('order list for page index ' + targetIndex + ' successfully retrieved');
                    self.currentPageIndex = targetIndex;
                    self.orders = _.map(data, (cur) => buildOrderItemViewModel(cur));
                }).fail(function (error) {
                    console.error('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                });
            }
        }
    };
};

/**
 * @typedef {Object} OrderData
 * @property {number} Id
 * @property {number} ExtrasPrice
 * @property {number} BasePrice
 * @property {number} Price
 * @property {string} Description
 * @property {string} DateTime
 * @property {string} Model
 * @property {string} Guid
 * @property {string} LinkUrl
 */

/**
 * @typedef {Object} OrderItemViewModel
 * @property {number} id
 * @property {number} basePrice
 * @property {number} extrasPrice
 * @property {number} price
 * @property {string} description
 * @property {string} dateTime
 * @property {string} model
 * @property {string} guid
 * @property {string} linkUrl
 */