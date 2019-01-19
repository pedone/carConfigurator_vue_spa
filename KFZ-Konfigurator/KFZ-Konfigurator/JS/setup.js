import './filters';
import Vue from 'vue';
import VueRouter from 'vue-router';
import VueI18n from 'vue-i18n';
import Vuex from 'vuex';
import store from './store';
import { getAntiForgeryToken } from './helper';
import * as enUS from './Localization/en-US';
import * as deDE from './Localization/de-DE';

export default function (routes) {
    Vue.config.productionTip = false;
    Vue.use(VueRouter);
    Vue.use(VueI18n);
    Vue.use(Vuex);

    const router = new VueRouter({
        routes,
        mode: 'history'
    });
    router.beforeEach((to, _from, next) => {
        if (to.meta.title) {
            document.title = to.meta.title;
        }
        next();
    });

    const i18n = new VueI18n({
        locale: 'enUS',
        fallbackLocale: 'enUS',
        messages: {
            enUS: enUS.messages,
            deDE: deDE.messages
        },
        numberFormats: {
            enUS: enUS.numberFormats,
            deDE: deDE.numberFormats
        },
        dateTimeFormats: {
            enUS: enUS.dateTimeFormats,
            deDE: deDE.dateTimeFormats
        }
    });

    new Vue({
        //template: "<div><router-view></router-view></div>",
        render: ce => ce('div', ['', ce('router-view')]),
        provide: {
            antiForgeryToken: getAntiForgeryToken($(document))
        },
        router,
        i18n,
        store
    }).$mount('#app');
};
