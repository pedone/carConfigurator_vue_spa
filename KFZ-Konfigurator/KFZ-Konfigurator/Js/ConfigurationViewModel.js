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
        this.engineSettingsById = {};
        _.each(data.engineSettings, (cur) => { self.engineSettingsById[cur.Id] = new ViewModel(cur) });

        this.enginePrice = ko.computed(function () {
            /** @type {number} */
            var result = 0;

            _.each(_.values(self.engineSettingsById), (cur) => {
                if (cur.isSelected()) {
                    result += cur.price;
                }
            });
            return result;
        });

        /** @param {number} settingsId */
        this.selectSettings = function (settingsId) {
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
 */
