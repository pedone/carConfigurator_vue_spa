'use strict';

class ModelSelectionViewModel {
    constructor() {
        /**
         * koObservable
         * @type {string}
         */
        this.curModelFilter = ko.observable('');
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