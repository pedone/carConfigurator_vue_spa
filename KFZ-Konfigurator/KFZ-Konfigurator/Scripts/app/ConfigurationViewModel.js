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
        this.selectedEnginePrice = (data.selectedEngineSetting && data.selectedEngineSetting.Price) || 0;

        /** @type {number} */
        this.selectedAccessoriesPrice = calculatePrice(data.selectedAccessories);

        /** @type {number} */
        this.fullPrice = ko.computed(function () {
            /** @type {number} */
            var enginePrice = self.selectedEnginePrice || self.enginePrice();
            /** @type {number} */
            var accessoriesPrice = self.selectedAccessoriesPrice || self.accessoriesPrice();

            return enginePrice + accessoriesPrice;
        });

        /**
         * @param {Array.<ViewModelData>} items
         * @returns {number}
         */
        function calculatePrice(items) {
            if (!items) {
                return 0;
            }

            return _.chain(items)
                .map((cur) => cur.Price)
                .reduce((memo, cur) => memo + cur, 0);
        }

        /**
         * @param {Array.<ViewModelData>} items
         * @returns {Object.<ViewModel>}
         */
        function toViewModelDictionary(items) {
            /** @type {Object.<ViewModel>} */
            var result = {};

            _.each(items, (cur) => { result[cur.Id] = new ViewModel(cur); });
            return result;
        }

        /**
         * @param {Array.<ViewModelData>} items
         * @returns {Array.<ViewModel>}
         */
        function toViewModelArray(items) {
            return _.map(items, (cur) => new ViewModel(cur));
        }

        /** 
         *  @param {Object.<ViewModel>} items
         *  @returns {function() : number}
         */
        function calculatePriceFactory(items) {
            return function () {
                return _.chain(items)
                    .filter((cur) => cur.isSelected())
                    .map((cur) => cur.price)
                    .reduce((memo, cur) => memo + cur, 0);
            }
        }

        /** @returns {number|null} */
        this.getSelectedEngineId = function () {
            /** @type {Array.<ViewModel>} */
            var engineSettings = _.values(self.engineSettingsById);

            var selectedEngine = _.find(engineSettings, (cur) => { return cur.isSelected(); });
            return selectedEngine && selectedEngine.id;
        };

        /** @returns {Array.<number>} */
        this.getSelectedAccessoryIds = function () {
            /** @type {Array.<ViewModel>} */
            var accessories = _.values(self.accessoriesById);

            var selectedItems = _.filter(accessories, (cur) => { return cur.isSelected(); });
            return _.map(selectedItems, (cur) => cur.id);
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
 * @property {Array.<ViewModelData>} engineSettings
 * @property {Array.<ViewModelData>} accessories
 * @property {Array.<ViewModelData>} selectedAccessories
 * @property {ViewModelData} selectedEngineSetting
 */
