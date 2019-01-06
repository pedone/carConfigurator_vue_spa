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
                /** @type {Array.<number>} */
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
