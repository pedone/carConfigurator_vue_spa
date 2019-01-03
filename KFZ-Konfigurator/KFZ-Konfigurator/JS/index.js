import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App';
import Model from './Model/Model';

Vue.config.productionTip = false;
Vue.use(VueRouter);

const routes = [
    {
        path: '/configuration/models',
        component: App,
        children: [
            {
                path: '',
                component: Model
            }
        ]
    }
];


const router = new VueRouter({
    routes,
    mode: 'history'
});

new Vue({
    //template: "<div><router-view></router-view></div>",
    render: ce => ce('div', ['', ce('router-view')]),
    router
}).$mount('#app');