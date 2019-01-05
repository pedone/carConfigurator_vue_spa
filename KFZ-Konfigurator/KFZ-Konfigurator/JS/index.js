import './filters';
import Vue from 'vue';
import VueRouter from 'vue-router';
import VueI18n from 'vue-i18n';
import App from './App';
import Model from './Model/Model';
import EngineSettings from './EngineSettings/EngineSettings';
import { getAntiForgeryToken } from './helper';
import enUS from './Localization/en-US';
import deDE from './Localization/de-DE';

Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(VueI18n);

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

const i18n = new VueI18n({
    locale: 'enUS',
    fallbackLocale: 'enUS',
    messages: {
        enUS: enUS,
        deDE: deDE
    }
});

new Vue({
    //template: "<div><router-view></router-view></div>",
    render: ce => ce('div', ['', ce('router-view')]),
    provide: {
        antiForgeryToken: getAntiForgeryToken($(document))
    },
    router,
    i18n
}).$mount('#app');
