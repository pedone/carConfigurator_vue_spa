const helper = require('helper.js');
const Vue = require('vue$');

Vue.filter('formatCurrency', function (value) {
    if (value !== undefined) {
        return helper.formatCurrency(value);
    }
});