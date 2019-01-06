import Vuex from 'vuex';
import { getConfigurationData } from './Api/data';
import constants from './constants';

export default function () {
    return new Vuex.Store({
        strict: process.env.NODE_ENV !== 'production',
        state: {
            carModelId: -1,
            configurationData: {},
            configuration: {
                /** @type {number} */
                engineSettingsId: -1,
                /** @type {Array.<AccessoryViewModelData>} */
                accessories: []
            },
            constants
        },
        mutations: {
            setConfiguration(state, value) {
                state.configurationData = value;
            },
            setEngineSettingsId(state, id) {
                state.configuration.engineSettingsId = id;
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
            }
        },
        actions: {
            loadConfigurationData({ commit, state }, carModelId) {
                if (carModelId !== state.carModelId) {
                    commit('setCarModelId', carModelId);
                    getConfigurationData(carModelId).then(data => commit('setConfiguration', data));
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
