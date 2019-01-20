import Vuex from 'vuex';
import { getConfigurationData } from './Api/data';
import constants from './constants';
import { orderBy, find } from 'lodash';

export default function () {
    return new Vuex.Store({
        strict: process.env.NODE_ENV !== 'production',
        state: {
            carModelId: -1,
            /** @type {ConfigurationData} */
            configurationData: {},
            configuration: {
                /** @type {ViewModelData} */
                engineSettings: undefined,
                /** @type {Array.<AccessoryViewModelData>} */
                accessories: [],
                /** @type {PaintViewModelData} */
                paint: undefined,
                /** @type {RimViewModelData} */
                rims: undefined
            },
            constants
        },
        getters: {
            orderedDataEngineSettings(state) {
                return orderBy(state.configurationData.engineSettings, cur => cur.Price);
            },
            orderedConfigurationAccessories(state) {
                return orderBy(state.configuration.accessories, cur => cur.Category);
            },
            modelName(state) {
                if (state.configurationData.model) {
                    return state.configurationData.model.series + ' ' + state.configurationData.model.bodyType + ' ' + state.configurationData.model.year;
                }
            }
        },
        mutations: {
            setConfiguration(state, value) {
                state.configurationData = value;
            },
            setEngineSettings(state, value) {
                state.configuration.engineSettings = value;
            },
            setCarModelId(state, id) {
                state.carModelId = id;
            },
            setSelectedAccessories(state, value) {
                state.configuration.accessories = value;
            },
            toggleSelectedAccessory(state, value) {
                const index = state.configuration.accessories.indexOf(value);
                if (index === -1) {
                    state.configuration.accessories.push(value);
                } else {
                    state.configuration.accessories.splice(index, 1);
                }
            },
            selectPaint(state, value) {
                state.configuration.paint = value;
            },
            selectRim(state, value) {
                state.configuration.rims = value;
            }
        },
        actions: {
            initConfigurationData({ commit, state, getters }, { carModelId, onSuccess }) {
                if (carModelId !== state.carModelId) {
                    commit('setCarModelId', carModelId);
                    getConfigurationData(carModelId).then(data => {
                        commit('setConfiguration', data);
                        //set default configuration values
                        commit('setEngineSettings', getters.orderedDataEngineSettings[0]);
                        commit('selectPaint', find(state.configurationData.paints, cur => cur.IsDefault) || state.configurationData.paints[0])
                        commit('selectRim', find(state.configurationData.rims, cur => cur.IsDefault) || state.configurationData.rims[0])
                        if (onSuccess) {
                            onSuccess();
                        }
                    });
                } else if (onSuccess) {
                    onSuccess();
                }
            }
        }
    })
};

//TODO refactor types

/**
 * @typedef {Object} ViewModel
 * @property {number} id
 * @property {number} price
 * @property {boolean} isSelected
 * @property {string} name
 * @property {number|null} size
 * @property {number|null} category
 */

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
 * @property {boolean|undefined} IsDefault
 */

/**
 * @typedef {ViewModelData} PaintViewModelData
 * @property {number} Size
 * @property {boolean|undefined} IsDefault
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
 * @property {Array.<string>} AccessoryCategories
 * @property {Array.<string>} PaintCategories
 */
