'use strict';

const vueGlobals = require('vueGlobals.js');
const Vue = require('vue$');
const configurationViewModel = require('configurationViewModel.js');

const vm = new Vue({
    el: '#app',
    mixins: [configurationViewModel.buildVueMixin(window.modelData)]
});

$(window).on('beforeunload', () => vm.saveChanges());