import setup from './setup';
import { getOrderListRoutes } from './routes';

setup(getOrderListRoutes(window.vueRouterUrl.orderList));