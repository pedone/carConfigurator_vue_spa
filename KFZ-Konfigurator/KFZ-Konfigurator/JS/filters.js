import Vue from 'vue';
import { filter as _filter } from 'lodash';

Vue.filter('countAccessoriesByCategory',
    /**
     * @param {Array.<AccessoryViewModelData>} data
     * @param {string} category
     */
    function (data, category) {
        return _filter(data, cur => cur.Category === category).length;
    }
);