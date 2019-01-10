import Vue from 'vue';
import { filter } from 'lodash';

Vue.filter('countAccessoriesByCategory',
    /**
     * @param {Array.<AccessoryViewModelData>} data
     * @param {string} category
     */
    function (data, category) {
        return filter(data, cur => cur.Category === category).length;
    }
);
