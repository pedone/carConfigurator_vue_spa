import { formatCurrency } from './Helper.js';

Vue.filter('formatCurrency', function (value) {
    if (value !== undefined) {
        return formatCurrency(value);
    }
});

export let vueMixins = [];
export let vueInstance = undefined;
export function setVueInstance(vm) {
    vueInstance = vm;
}