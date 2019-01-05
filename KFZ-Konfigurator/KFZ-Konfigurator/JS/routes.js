import App from './App';
import Model from './Model/Model';
import EngineSettings from './EngineSettings/EngineSettings';
import Accessories from './Accessories/Accessories';

export default [
    {
        path: '',
        component: App,
        children: [
            {
                path: '',
                component: Model,
                meta: {
                    title: 'Model'
                }
            },
            {
                name: 'engine-settings',
                path: 'model-:id/engine-settings',
                component: EngineSettings,
                meta: {
                    title: 'Engine Settings'
                }
            },
            {
                name: 'accessories',
                path: 'model-:id/accessories',
                component: Accessories,
                meta: {
                    title: 'Accessories'
                }
            }
        ]
    }
];