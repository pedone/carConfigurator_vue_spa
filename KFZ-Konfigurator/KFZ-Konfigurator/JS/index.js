import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App';
import About from './About';

Vue.config.productionTip = false;
Vue.use(VueRouter);

const routes = [
    {
        path: '/configuration/models',
        component: App,
        children: [
            {
                path: 'about',
                component: About
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