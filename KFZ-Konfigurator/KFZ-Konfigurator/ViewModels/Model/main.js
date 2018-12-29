'use strict';

const Vue = require('vue$');
const helper = require('helper.js');

const vm = new Vue({
    el: '#app',
    data: {
        curModelFilter: ''
    },
    computed: {
        isModelFilterEmpty: function () {
            return this.curModelFilter === '';
        }
    },
    methods: {
        filterModels: function (filter) {
            if (filter !== this.curModelFilter) {
                this.curModelFilter = filter || '';
            }
        },
        selectCarModel: function (id, targetUrl) {
            const antiForgeryToken = helper.getAntiForgeryToken($(document));
            helper.saveViewModel('/Model/SetCarModel', id, antiForgeryToken)
                .done(() => {
                    window.location.href = targetUrl;
                })
                .fail((error) => {
                    console.error('failed to set car model: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                    // TODO randomly triggered, maybe because of antiForgeryValidation
                    //alert('something went wrong. see console for details');
                });
        }
    }
});