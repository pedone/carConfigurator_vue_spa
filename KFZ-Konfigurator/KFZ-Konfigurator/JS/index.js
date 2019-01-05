import './filters';
import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App';
import Model from './Model/Model';
import EngineSettings from './EngineSettings/EngineSettings';
import { getAntiForgeryToken } from './helper';

Vue.config.productionTip = false;
Vue.use(VueRouter);

const routes = [
    {
        path: '',
        component: App,
        children: [
            {
                path: '',
                component: Model
            },
            {
                name: 'engine-settings',
                path: 'model-:id/engine-settings',
                component: EngineSettings
            }
        ]
    }
];

const router = new VueRouter({
    routes,
    base: window.VueRouterUrl,
    mode: 'history'
});

new Vue({
    //template: "<div><router-view></router-view></div>",
    render: ce => ce('div', ['', ce('router-view')]),
    provide: {
        antiForgeryToken: getAntiForgeryToken($(document))
    },
    router
}).$mount('#app');