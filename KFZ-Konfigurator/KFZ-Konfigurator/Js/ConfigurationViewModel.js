"use strict";

class ViewModel {
    /** @param {ViewModelData} data */
    constructor(data) {
        /** type {number} */
        this.id = data.Id;
        /** type {number} */
        this.price = data.Price;
        /** type {boolean} */
        this.isSelected = ko.observable(data.IsSelected);
    }
}

class ConfigurationViewModel {
    /** @param {ConfigurationData} data */
    constructor(data) {
        self = this;

        /** @type {Object.<ViewModel>} */
        this.engineSettingsById = toViewModelDictionary(data.engineSettings);

        /** @type {Object.<ViewModel>} */
        this.accessoriesById = toViewModelDictionary(data.accessories);

        /** @type {number} */
        this.enginePrice = ko.computed(calculatePriceFactory(self.engineSettingsById));

        /** @type {number} */
        this.accessoriesPrice = ko.computed(calculatePriceFactory(self.accessoriesById));

        /** @type {number} */
        this.fullPrice = ko.computed(function () {
            return self.enginePrice() + self.accessoriesPrice();
        });

        /**
         * @param {Array.<ViewModel>} items
         * @returns {Object.<ViewModel>}
         */
        function toViewModelDictionary(items) {
            /** @type {Object.<ViewModel>} */
            var result = {};

            _.each(items, (cur) => { result[cur.Id] = new ViewModel(cur) });
            return result;
        }

        /** 
         *  @param {Object.<ViewModel>} items
         *  @returns {function() : number}
         */
        function calculatePriceFactory(items) {
            return function () {
                var i;
                /** @type {number} */
                var result = 0;

                var values = _.values(items);
                for (i = 0; i < values.length; i += 1) {
                    if (values[i].isSelected()) {
                        result += values[i].price;
                    }
                }
                return result;
            }
        }

        /** @returns {ViewModel} */
        this.getSelectedEngine = function () {
            /** @type {Array.<ViewModel>} */
            var engineSettings = _.values(self.engineSettingsById);

            return _.find(engineSettings, (cur) => { return cur.isSelected(); });
        };

        /** @param {number} settingsId */
        this.settingsSelectedClick = function (settingsId) {
            //deselect all settings, because deselection doesn't work with binding
            _.each(self.engineSettingsById, (cur) => { cur.isSelected(false) });
            self.engineSettingsById[settingsId].isSelected(true);
        };
    }
}

/**
 * @typedef {Object} ViewModelData
 * @property {number} Id
 * @property {number} Price
 * @property {boolean} IsSelected
 */

/**
 * @typedef {Object} ConfigurationData
 * @property {Array.<ViewModel>} engineSettings
 * @property {Array.<ViewModel>} accessories
 */
