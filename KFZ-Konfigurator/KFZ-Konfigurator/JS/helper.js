﻿'use strict';

/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */
export function saveViewModel(url, id, antiForgeryToken, additionalData) {
    let i;
    /** @type {Object} */
    let dataObject = {
        __RequestVerificationToken: antiForgeryToken
    };

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
    return $.ajax({
        type: 'POST',
        url: url,
        data: dataObject,
        contentType: 'application/x-www-form-urlencoded'
    });
};

/**
 * @param {Document} document
 * @returns {string}
 */
export function getAntiForgeryToken(document) {
    return document.find('[name="__RequestVerificationToken"]').val();
};

export function formatCurrency(amount) {
    return amount.toLocaleString() + ' EUR';
};