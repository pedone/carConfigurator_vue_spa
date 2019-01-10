import App from './App';
import Model from './Model/Model';
import constants from './constants';
import EngineSettings from './EngineSettings/EngineSettings';
import Accessories from './Accessories/Accessories';
import Exterior from './Exterior/Exterior';
import ConfigurationOverview from './ConfigurationOverview/ConfigurationOverview';

export default [
    {
        path: '',
        component: App,
        children: [
            {
                name: constants.routes.model,
                path: '',
                component: Model,
                meta: {
                    title: 'Model'
                }
            },
            {
                name: constants.routes.engineSettings,
                path: 'model-:id/engine-settings',
                component: EngineSettings,
                meta: {
                    title: 'Engine Settings'
                }
            },
            {
                name: constants.routes.accessories,
                path: 'model-:id/accessories',
                component: Accessories,
                meta: {
                    title: 'Accessories'
                }
            },
            {
                name: constants.routes.exterior,
                path: 'model-:id/exterior',
                component: Exterior,
                meta: {
                    title: 'Exterior'
                }
            },
            {
                name: constants.routes.configurationOverview,
                path: 'model-:id/overview',
                component: ConfigurationOverview,
                meta: {
                    title: 'ConfigurationOverview'
                }
            }
        ]
    }
];