'use strict';

class ModelSelectionViewModel {
    constructor() {
        /**
         * koObservable
         * @type {string}
         */
        this.curModelFilter = ko.observable('');

        this.test = ko.observable('A1');
        this.test.subscribe(function (newValue) {
            console.log(newValue);
        });
    }

    /**
     * @param {string} filter
     */
    filterModels(filter) {
        if (filter !== this.curModelFilter()) {
            this.curModelFilter(filter);
        }
    }
}