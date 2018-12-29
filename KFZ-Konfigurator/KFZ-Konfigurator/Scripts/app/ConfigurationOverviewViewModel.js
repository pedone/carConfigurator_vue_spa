'use strict';

const helper = require('helper.js');

/** @param {HandlerOptions} options */
module.exports.buildVueMixin = function(options) {
    return {
        data: {
            configurationDescription: ''
        },
        created: function () {
            this._antiForgeryToken = helper.getAntiForgeryToken($(document));
        },
        methods: {
            placeOrder: function () {
                $.ajax({
                    type: 'POST',
                    url: '/ConfigurationOverview/PlaceOrder',
                    data: { __RequestVerificationToken: this._antiForgeryToken, description: this.configurationDescription },
                    contentType: 'application/x-www-form-urlencoded',
                    dataType: 'text'
                }).done(
                    function (data) {
                        console.debug('order successfully placed');
                        if (options.placeOrderSuccess) {
                            options.placeOrderSuccess(data);
                        }
                    })
                    .fail(function (error) {
                        console.error('failed to place order: ' + error.responseText + ' (' + error.statusText + ')');
                        console.debug(JSON.stringify(error));
                        if (options.placeOrderFailure) {
                            options.placeOrderFailure();
                        }
                    });
            }
        }
    };
};

/**
 * @typedef {Object} HandlerOptions
 * @property {Function|null} placeOrderSuccess
 * @property {Function|null} placeOrderFailure
 */