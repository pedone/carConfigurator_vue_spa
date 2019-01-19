import setup from './setup';
import getRoutes from './routes';

setup(getRoutes(window.vueRouterUrl.model));