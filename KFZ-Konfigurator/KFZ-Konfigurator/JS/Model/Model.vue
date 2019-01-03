<template>
    <div>
        <div class="row container">
            <h2 class="col"><!--@LocalizationManager.Localize("ModelView_Header")-->Choose your model</h2>
            <div class="btn-group btn-group-toggle align-self-start">
                <button type="button" class="btn btn-light" v-bind:class="{ active: isModelFilterEmpty }" @@click="filterModels">
                    <!--@LocalizationManager.Localize("ModelView_Filter_All")-->
                    All
                </button>
                <button v-for="item in seriesList"
                        type="button"
                        class="btn btn-light"
                        @@click="filterModels(item)"
                        v-bind:class="{ active: curModelFilter === item }">
                    {{ item }}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
    import * as DataApi from '../Api/data.js';
    import { map as _map, uniqBy as _uniqBy } from 'lodash';

    export default {
        data: function () {
            return {
                curModelFilter: '',
                modelList: {},
                seriesList: {}
            };
        },
        created: function () {
            /** @param {{Series: string}} data */
            this.buildSeriesList = (data) => _uniqBy(_map(data, cur => cur.Series), cur => cur);
        },
        mounted: function () {
            DataApi.getCarModelList().then(data => {
                this.modelList = data;
                this.seriesList = this.buildSeriesList(data);
            });
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
    }
</script>