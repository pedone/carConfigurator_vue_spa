'use strict';

const vueGlobals = require('vueGlobals.js');
const Vue = require('vue$');
const orderListViewModel = require('orderListViewModel.js');

const vm = new Vue({
    el: '#app',
    mixins: [orderListViewModel.buildVueMixin(window.modelData.data, window.modelData.pageCount)]
});

//set the data of the confirm dialog, so that the selected item can be removed
$(document).on('show.bs.modal', '#confirmCancelDialog', function (e) {
    let orderId = $(e.relatedTarget).data('id');
    $(this).find('#confirmCancelBtn').attr('data-orderid', orderId);
});