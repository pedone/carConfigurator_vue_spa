'use strict';

const vueGlobals = require('vueGlobals.js');
const Vue = require('vue$');
const configurationViewModel = require('configurationViewModel.js');
const configurationOverviewViewModel = require('configurationOverviewViewModel.js');

let options = {
    placeOrderSuccess: function (targetUrl) {
        window.location.href = targetUrl;
    },
    placeOrderFailure: function () {
        $('#orderFailedAlert').attr('hidden', false);
    }
};

const vm = new Vue({
    el: '#app',
    mixins: [
        configurationViewModel.buildVueMixin(window.modelData),
        configurationOverviewViewModel.buildVueMixin(options)
    ]
});