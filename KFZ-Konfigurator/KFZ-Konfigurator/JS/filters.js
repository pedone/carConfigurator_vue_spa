import Vue from 'vue';
import { formatCurrency } from './helper.js';

Vue.filter('formatCurrency', function (value) {
    if (value !== undefined) {
        return formatCurrency(value);
    }
});