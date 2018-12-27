import { formatCurrency } from './Helper.js';

Vue.filter('formatCurrency', function (value) {
    if (value !== undefined) {
        return formatCurrency(value);
    }
});
