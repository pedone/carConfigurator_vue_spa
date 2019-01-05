import Vuex from 'vuex';
import { getConfigurationData } from './Api/data';

export default function () {
    return new Vuex.Store({
        strict: process.env.NODE_ENV !== 'production',
        state: {
            configurationData: {},
            configuration: {
                engineSettingsId: -1
            }
        },
        mutations: {
            setConfiguration(state, value) {
                state.configurationData = value;
            },
            setEngineSettingsId(state, id) {
                state.configuration.engineSettingsId = id;
            }
        },
        actions: {
            initConfigurationData({ commit, state }) {
                if (Object.keys(state.configurationData).length === 0) {
                    getConfigurationData().then(data => commit('setConfiguration', data));
                }
            }
        }
    })
};
