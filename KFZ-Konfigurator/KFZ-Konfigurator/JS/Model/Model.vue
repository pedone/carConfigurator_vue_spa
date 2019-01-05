<template>
    <div>
        <div class="row container">
            <h2 class="col">{{$t('modelView.header')}}</h2>
            <div class="btn-group btn-group-toggle align-self-start">
                <button type="button" class="btn btn-light" v-bind:class="{ active: isModelFilterEmpty }" @click="filterModels('')">
                    {{ $t('modelView.filterAll') }}
                </button>
                <button v-for="item in seriesList"
                        type="button"
                        class="btn btn-light"
                        @click="filterModels(item)"
                        v-bind:class="{ active: curModelFilter === item }">
                    {{ item }}
                </button>
            </div>
        </div>
        <div class="row container">
            <a v-for="item in modelList"
               class="card mb-2 col-md-4 rounded-0 car-model-link"
               v-show="curModelFilter === item.Series || isModelFilterEmpty"
               @click="selectCarModel(item.Id)">
                <div class="card-body">
                    <h5 class="card-title">{{item.Series}} {{item.BodyType}} {{item.Year}}</h5>
                    <h6 class="card-subtitle">
                        {{$t('general.from')}} {{ $n(item.BaseSettings.Price, 'currency') }}
                    </h6>
                    <div class="card-text mt-3">
                        <div>
                            {{ $t('technicalData.consumption') }}: {{ item.BaseSettings.Consumption }} l/100 km
                        </div>
                        <div>
                            {{ $t('technicalData.emissions') }}: {{ item.BaseSettings.Emission }} g/km
                        </div>
                    </div>
                </div>
            </a>
        </div>
    </div>
</template>

<script>
    import * as DataApi from '../Api/data.js';
    import { map as _map, uniqBy as _uniqBy } from 'lodash';
    import { saveViewModel } from '../../Scripts/app/helper.js';

    export default {
        data: function () {
            return {
                curModelFilter: '',
                modelList: {},
                seriesList: {}
            };
        },
        inject: ['antiForgeryToken'],
        created: function () {
            /** @param {{Series: string}} data */
            this._buildSeriesList = (data) => _uniqBy(_map(data, cur => cur.Series), cur => cur);
            DataApi.getCarModelList().then(data => {
                this.modelList = data;
                this.seriesList = this._buildSeriesList(data);
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
            selectCarModel: function (id) {
                saveViewModel('/Model/SetCarModel', id, this.antiForgeryToken)
                    .done(() => {
                        this.$router.push({ name: 'engine-settings', params: { id: id } });
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