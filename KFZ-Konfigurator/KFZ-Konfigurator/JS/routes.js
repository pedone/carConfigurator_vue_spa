import App from './App';
import Configuration from './Configuration';
import Model from './Model/Model';
import constants from './constants';
import EngineSettings from './EngineSettings/EngineSettings';
import Accessories from './Accessories/Accessories';
import Exterior from './Exterior/Exterior';
import ConfigurationOverview from './ConfigurationOverview/ConfigurationOverview';
import OrderList from './OrderList/OrderList';
import OrderOverview from './OrderOverview/OrderOverview';

export default function (baseRoute) {
    return [
        {
            path: baseRoute,
            component: Configuration,
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
            ],
        },
        {
            path: '',
            component: App,
            children: [
                {
                    path: '/orders/view/:guid',
                    name: constants.routes.orderOverview,
                    component: OrderOverview,
                    meta: {
                        orderSuccess: true
                    }
                }
            ]
        }
    ];
};

export function getOrderListRoutes(baseRoute) {
    return [
        {
            path: baseRoute,
            component: App,
            children: [
                {
                    name: constants.routes.orderList,
                    path: '',
                    component: OrderList,
                    meta: {
                        title: 'Order List'
                    }
                },
                {
                    name: constants.routes.orderOverview,
                    path: 'view/:guid',
                    component: OrderOverview,
                    meta: {
                        title: 'Order Overview'
                    }
                }
            ]
        }
    ];
};
