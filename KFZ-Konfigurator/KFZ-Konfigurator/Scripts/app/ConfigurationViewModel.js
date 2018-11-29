'use strict';

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

/**
 * @property {Object.<ViewModel>} _engineSettingsById
 * @property {Object.<ViewModel>} _rimsById
 * @property {Object.<ViewModel>} _paintById
 * @property {Object.<ViewModel>} accessoriesById
 * @property {number} _computedEnginePrice
 * @property {number} _computedAccessoriesPrice
 * @property {number} _computedPaintPrice
 * @property {number} _computedRimsPrice
 * @property {number} _setEnginePrice
 * @property {number} _setPaintPrice
 * @property {number} _setRimsPrice
 * @property {number} _setAccessoriesPrice
 */
class ConfigurationViewModel {
    /**
     * @param {ConfigurationData} data
     */
    constructor(data) {
        var self = this;

        /**
         * observable
         * @type {string}
         */
        this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.paints));

        /**
         * observable
         * @type {string}
         */
        this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.rims));

        Object.defineProperties(this, {
            _engineSettingsById: {
                value: this._toViewModelDictionary(data.engineSettings),
                writable: false,
                enumerable: false
            },
            _rimsById: {
                value: this._toViewModelDictionary(data.rims),
                writable: false,
                enumerable: false
            },
            _paintById: {
                value: this._toViewModelDictionary(data.paints),
                writable: false,
                enumerable: false
            },
            accessoriesById: {
                value: this._toViewModelDictionary(data.accessories),
                writable: false,
                enumerable: true
            }
        });

        Object.defineProperties(this, {
            _computedEnginePrice: {
                value: ko.computed(this._calculatePriceFactory(this._engineSettingsById)),
                writable: false,
                enumerable: false
            },
            _computedAccessoriesPrice: {
                value: ko.computed(this._calculatePriceFactory(this.accessoriesById)),
                writable: false,
                enumerable: false
            },
            _computedPaintPrice: {
                value: ko.computed(this._calculatePriceBySelectedIdFactory(this._paintById, () => this.selectedPaintId())),
                writable: false,
                enumerable: false
            },
            _computedRimsPrice: {
                value: ko.computed(this._calculatePriceBySelectedIdFactory(this._rimsById, () => this.selectedRimsId())),
                writable: false,
                enumerable: false
            },
            _setEnginePrice: {
                value: (data.selectedEngineSetting && data.selectedEngineSetting.Price) || 0,
                writable: false,
                enumerable: false
            },
            _setPaintPrice: {
                value: (data.selectedPaint && data.selectedPaint.Price) || 0,
                writable: false,
                enumerable: false
            },
            _setRimsPrice: {
                value: (data.selectedRims && data.selectedRims.Price) || 0,
                writable: false,
                enumerable: false
            },
            _setAccessoriesPrice: {
                value: this._calculatePrice(data.selectedAccessories),
                writable: false,
                enumerable: false
            }
        });

        /** @type {boolean} */
        this.isAccessoryLimitReached = ko.computed(function () {
            /** @type {number} */
            const accessoryLimit = 5;
            return _.filter(_.values(self.accessoriesById), (cur) => cur.isSelected()).length >= accessoryLimit;
        });

        /**
         * Calculates the combined price of everything but the engine
         * @type {number}
         */
        this.extrasPrice = ko.computed(function () {
            /** @type {number} */
            let accessoriesPrice = self._setAccessoriesPrice || self._computedAccessoriesPrice();
            /** @type {number} */
            let paintPrice = self._setPaintPrice || self._computedPaintPrice();
            /** @type {number} */
            let rimsPrice = self._setRimsPrice || self._computedRimsPrice();

            return accessoriesPrice + paintPrice + rimsPrice;
        });

        /** @type {number} */
        this.basePrice = ko.computed(function () {
            return self._setEnginePrice || self._computedEnginePrice();
        });

        /** @type {number} */
        this.fullPrice = ko.computed(function () {
            return self.basePrice() + self.extrasPrice();
        });
    }

    /**
     * @param {Array.<ViewModelData>} items
     * @returns {string}
     * @private
     */
    _getInitialSelectedId(items) {
        if (!items || items.length === 0) {
            return -1;
        }

        /** @type {ViewModelData} */
        let selectedItem = _.find(items, (cur) => cur.IsSelected) || items[0];
        return selectedItem.Id.toString();
    }

    /**
     * @param {Object.<ViewModel>} items
     * @param {Function(): number} selectedId
     * @returns {Function(): number}
     * @private
     */
    _calculatePriceBySelectedIdFactory(items, selectedId) {
        return function () {
            /** @type {ViewModel} */
            let selectedItem = items[selectedId()];
            return (selectedItem && selectedItem.price) || 0;
        };
    }
    /**
     * @param {Array.<ViewModelData>} items
     * @returns {number}
     * @private
     */
    _calculatePrice(items) {
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
     * @private
     */
    _toViewModelDictionary(items) {
        /** @type {Object.<ViewModel>} */
        var result = {};

        _.each(items, (cur) => { result[cur.Id] = new ViewModel(cur); });
        return result;
    }

    /** 
     *  @param {Object.<ViewModel>} items
     *  @returns {function() : number}
     */
    _calculatePriceFactory(items) {
        return function () {
            return _.chain(items)
                .filter((cur) => cur.isSelected())
                .map((cur) => cur.price)
                .reduce((memo, cur) => memo + cur, 0);
        }
    }

    /** @returns {number|null} */
    get selectedEngineId() {
        /** @type {Array.<ViewModel>} */
        let engineSettings = _.values(this._engineSettingsById);

        let selectedEngine = _.find(engineSettings, (cur) => { return cur.isSelected(); });
        return selectedEngine && selectedEngine.id;
    }

    /** @returns {Array.<number>} */
    get selectedAccessoryIds() {
        /** @type {Array.<ViewModel>} */
        let accessories = _.values(this.accessoriesById);

        let selectedItems = _.filter(accessories, (cur) => { return cur.isSelected(); });
        return _.map(selectedItems, (cur) => cur.id);
    }

    /** @param {number} settingsId */
    settingsSelectedClick(settingsId) {
        //deselect all settings, because deselection doesn't work with binding
        _.each(this._engineSettingsById, (cur) => { cur.isSelected(false) });
        this._engineSettingsById[settingsId].isSelected(true);
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
 * @property {Array.<ViewModelData>} paints
 * @property {Array.<ViewModelData>} selectedAccessories
 * @property {ViewModelData} selectedEngineSetting
 * @property {ViewModelData} selectedPaint
 * @property {ViewModelData} selectedRims
 */
