'use strict';

import * as helper from './Helper.js';

class ViewModel {
    /** @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data */
    constructor(data) {
        /** @type {number} */
        this.id = data.Id;
        /** @type {number} */
        this.price = data.Price;
        /** @type {boolean} */
        this.isSelected = ko.observable(data.IsSelected);
        /** @type {number|null} */
        this.name = data.Name;
        /** @type {number|null} */
        this.size = data.Size;
        /** @type {number|null} */
        this.category = data.Category;
    }
}

/**
 * @property {Object.<ViewModel>} _engineSettingsById
 * @property {Object.<ViewModel>} _rimsById
 * @property {Object.<ViewModel>} _paintById
 * @property {Object.<ViewModel>} accessoriesById
 * @property {Array.<ViewModel>} selectedAccessories
 */
class ConfigurationViewModel {
    /**
     * @param {ConfigurationData} data
     */
    constructor(data) {
        Object.defineProperties(this, {
            _engineSettingsById: {
                value: this._toViewModelDictionary(data.EngineSettings),
                writable: false,
                enumerable: false
            },
            _rimsById: {
                value: this._toViewModelDictionary(data.Rims),
                writable: false,
                enumerable: false
            },
            _paintById: {
                value: this._toViewModelDictionary(data.Paints),
                writable: false,
                enumerable: false
            },
            accessoriesById: {
                value: this._toViewModelDictionary(data.Accessories),
                writable: false,
                enumerable: true
            }
        });

        /**
         * koObservable
         * @type {string}
         */
        this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.Paints));
        /** @type {string} */
        this._initialPaintId = this.selectedPaintId();

        /**
         * koObservable
         * @type {string}
         */
        this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.Rims));
        /** @type {string} */
        this._initialRimsId = this.selectedRimsId();

        /**
         * koObservable
         * @type {Array.<ViewModel>}
         */
        this.selectedAccessories = ko.observableArray(_.values(this.accessoriesById)).extend({ filterSelected: null });
        /** @type {Array.<number>} */
        this._initialAccessoryIds = this.selectedAccessoryIds;

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedEngineSettings = ko.computed(() => {
            let values = _.values(this._engineSettingsById);
            // only one setting is supposed to be selected
            return _.first(_.filter(values, (cur) => cur.isSelected()));
        });
        /** @type {ViewModel} */
        this._initialEngineSettings = this.selectedEngineSettings();

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedPaint = ko.computed(() => this._paintById[this.selectedPaintId()]);

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedRims = ko.computed(() => this._rimsById[this.selectedRimsId()]);

        /**
         * Calculates the combined price of everything but the engine
         * koComputed
         * @type {number}
         */
        this.extrasPrice = ko.computed(() => {
            /** @type {number} */
            let accessoriesPrice = _.reduce(this.selectedAccessories(), (memo, cur) => memo + cur.price, 0);
            return accessoriesPrice + (this.selectedPaint() && this.selectedPaint().price) + (this.selectedRims() && this.selectedRims().price);
        });

        /**
         * Just the engine price
         * koComputed
         * @type {number}
         */
        this.basePrice = ko.computed(() => {
            return (this.selectedEngineSettings() && this.selectedEngineSettings().price) || 0;
        });

        /**
         * The base price combined with the extras price
         * koComputed
         * @type {number}
         */
        this.fullPrice = ko.computed(() => {
            return this.basePrice() + this.extrasPrice();
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

    /** @returns {Array.<number>} */
    get selectedAccessoryIds() {
        return _.map(this.selectedAccessories(), (cur) => cur.id);
    }

    /**
     * @param {Array.<ViewModel>} accessories
     * @param {string} category
     */
    countCategory(accessories, category) {
        return _.filter(accessories, (cur) => cur.category === category).length;
    }

    /** @param {number} settingsId */
    selectEngineSettings(settingsId) {
        //deselect all other settings, because deselection doesn't work with binding
        _.each(this._engineSettingsById, (cur) => { cur.isSelected(cur.id === settingsId) });
    }

    /** @param {string} id */
    selectPaint(id) {
        this.selectedPaintId(id);
    }

    /** @param {string} id */
    selectAccessory(id) {
        /** @type {boolean} */
        let isSelected = this.accessoriesById[id].isSelected();
        this.accessoriesById[id].isSelected(!isSelected);
    }

    /** @param {string} antiForgeryToken */
    saveChanges(antiForgeryToken) {
        let selectedAccessoryIds = this.selectedAccessoryIds;

        //check for changes
        let accessoriesChanged = (this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, this.selectedAccessoryIds).length > 0);
        let engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings();
        let paintChanged = this._initialPaintId != this.selectedPaintId();
        let rimsChanged = this._initialRimsId != this.selectedRimsId();

        if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
            console.debug('no configuration changes');
            return;
        }

        // package changes
        let changedData = { __RequestVerificationToken: antiForgeryToken };
        if (paintChanged) {
            changedData.paintId = viewModel.selectedPaintId();
        }
        if (rimsChanged) {
            changedData.rimId = viewModel.selectedRimsId();
        }
        if (engineSettingsChanged) {
            changedData.engineSettingsId = viewModel.selectedEngineSettings().id;
        }
        if (accessoriesChanged) {
            changedData.accessoryIds = selectedAccessoryIds;
        }

        console.debug('saving configuration changes');

        //send changes
        $.ajax({
            //make sure the changes are saved before the next page is loaded
            async: false,
            type: 'POST',
            url: '/Configuration/UpdateActiveConfiguration',
            data: changedData,
            contentType: 'application/x-www-form-urlencoded'
        }).fail((error) => {
            console.error('failed to save configuration changes: ' + error.responseText + ' (' + error.statusText + ')');
            console.debug(JSON.stringify(error));
            //alert('something went wrong. see console for details');
        });
    }
}

/**
 * @typedef {Object} ViewModelData
 * @property {number} Id
 * @property {number} Price
 * @property {boolean} IsSelected
 */

/**
 * @typedef {ViewModelData} NameViewModelData
 * @property {string} Name
 */

/**
 * @typedef {ViewModelData} RimViewModelData
 * @property {number} Size
 */

/**
 * @typedef {NameViewModelData} AccessoryViewModelData
 * @property {number} Category
 */

/**
 * @typedef {Object} ConfigurationData
 * @property {Array.<ViewModelData>} EngineSettings
 * @property {Array.<ViewModelData>} Accessories
 * @property {Array.<ViewModelData>} Paints
 * @property {Array.<ViewModelData>} Rims
 */


/**
 * @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data
 * @returns {ViewModel}
 */
function buildViewModel(data) {
    /** @type {ViewModel} */
    return {
        id: data.Id,
        category: data.Category,
        isSelected: data.IsSelected,
        name: data.Name,
        price: data.Price,
        size: data.Size
    };
}

/**
 * @param {Array.<ViewModelData>} items
 * @returns {Object.<ViewModel>}
 * @private
 */
function toViewModelDictionary(items) {
    /** @type {Object.<ViewModel>} */
    var result = {};

    _.each(items, (cur) => { result[cur.Id] = buildViewModel(cur); });
    return result;
}

/**
 * @param {Array.<ViewModelData>} items
 * @returns {number}
 * @private
 */
function getInitialSelectedId(items) {
    if (!items || items.length === 0) {
        return -1;
    }

    /** @type {ViewModelData} */
    let selectedItem = _.find(items, (cur) => cur.IsSelected) || items[0];
    return selectedItem.Id;
}

/**
 * @param {ConfigurationData} data
 * @param {Object} [mixins]
 */
export function buildVue(data, mixins) {
    return new Vue({
        el: '#app',
        mixins: [mixins || {}],
        data: {
            selectedPaintId: getInitialSelectedId(data.Paints),
            selectedRimsId: getInitialSelectedId(data.Rims),
            engineSettingsById: toViewModelDictionary(data.EngineSettings),
            accessoriesById: toViewModelDictionary(data.Accessories),
            accesoryCountByCategory: {}
        },
        created: function () {
            this._getSelectedAccessoryIds = function () {
                return _.map(this.selectedAccessories, (cur) => cur.id);
            };

            this._paintById = toViewModelDictionary(data.Paints);
            this._rimsById = toViewModelDictionary(data.Rims);
            this._initialAccessoryIds = this._getSelectedAccessoryIds();
            this._initialEngineSettings = this.selectedEngineSettings
            this._initialPaintId = this.selectedPaintId;
            this._initialRimsId = this.selectedRimsId;
        },
        computed: {
            /** @type {ViewModel} */
            selectedPaint: function () {
                return this._paintById[this.selectedPaintId];
            },
            /** @type {ViewModel} */
            selectedRims: function () {
                return this._rimsById[this.selectedRimsId];
            },
            /** @type {Array.<ViewModel>} */
            selectedAccessories: function () {
                /** @type {Array.<ViewModel>} */
                let values = _.values(this.accessoriesById);
                return _.filter(values, (cur) => cur.isSelected);
            },
            /**
             * Just the engine price
             * @type {number}
             */
            basePrice: function () {
                return (this.selectedEngineSettings && this.selectedEngineSettings.price) || 0;
            },
            formattedBasePrice: function () {
                return helper.formatCurrency(this.basePrice);
            },
            /**
             * Calculates the combined price of everything but the engine
             * @type {number}
             */
            extrasPrice: function () {
                /** @type {number} */
                let accessoriesPrice = _.reduce(this.selectedAccessories, (memo, cur) => memo + cur.price, 0);
                return accessoriesPrice + (this.selectedPaint && this.selectedPaint.price) + (this.selectedRims && this.selectedRims.price);
            },
            formattedExtrasPrice: function () {
                return helper.formatCurrency(this.extrasPrice);
            },
            /**
             * The base price combined with the extras price
             * @type {number}
             */
            formattedFullPrice: function () {
                return this.basePrice + this.extrasPrice;
            },
            /** @type {ViewModel} */
            selectedEngineSettings: function () {
                let values = _.values(this.engineSettingsById);
                // only one setting is supposed to be selected
                return _.first(_.filter(values, (cur) => cur.isSelected));
            }
        },
        watch: {
            selectedAccessories: {
                immediate: true,
                handler: function (items) {
                    this.accesoryCountByCategory = {};
                    for (let cur of items) {
                        this.accesoryCountByCategory[cur.category] = (this.accesoryCountByCategory[cur.category] || 0) + 1;
                    }
                }
            }
        },
        methods: {
            /** @param {string} id */
            selectPaint(id) {
                this.selectedPaintId = id;
            },
            /** @param {string} id */
            toggleAccessorySelection: function (id) {
                this.accessoriesById[id].isSelected = !this.accessoriesById[id].isSelected;
            },
            /** @param {number} settingsId */
            selectEngineSettings: function (settingsId) {
                //deselect all other settings, because deselection doesn't work with binding
                _.each(this.engineSettingsById, (cur) => { cur.isSelected = (cur.id === settingsId) });
            },
            /** @param {string} antiForgeryToken */
            saveChanges: function (antiForgeryToken) {
                /** @type {Array.<number>} */
                let selectedAccessoryIds = this._getSelectedAccessoryIds();

                //check for changes
                let accessoriesChanged = (this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, selectedAccessoryIds).length > 0);
                let engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings;
                let paintChanged = this._initialPaintId != this.selectedPaintId;
                let rimsChanged = this._initialRimsId != this.selectedRimsId;

                if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
                    console.debug('no configuration changes');
                    return;
                }

                // package changes
                let changedData = { __RequestVerificationToken: antiForgeryToken };
                if (paintChanged) {
                    changedData.paintId = this.selectedPaintId;
                }
                if (rimsChanged) {
                    changedData.rimId = this.selectedRimsId;
                }
                if (engineSettingsChanged) {
                    changedData.engineSettingsId = this.selectedEngineSettings.id;
                }
                if (accessoriesChanged) {
                    changedData.accessoryIds = selectedAccessoryIds;
                }

                console.debug('saving configuration changes');

                //send changes
                $.ajax({
                    //make sure the changes are saved before the next page is loaded
                    async: false,
                    type: 'POST',
                    url: '/Configuration/UpdateActiveConfiguration',
                    data: changedData,
                    contentType: 'application/x-www-form-urlencoded'
                }).fail((error) => {
                    console.error('failed to save configuration changes: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                    //alert('something went wrong. see console for details');
                });
            }
        }
    });
}

/**
 * @typedef {Object} ViewModel
 * @property {number} id
 * @property {number} price
 * @property {boolean} isSelected
 * @property {string} name
 * @property {number|null} size
 * @property {number|null} category
 */
