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
            <router-link v-for="item in modelList"
                         class="card mb-2 col-md-4 rounded-0 car-model-link"
                         v-show="curModelFilter === item.Series || isModelFilterEmpty"
                         v-bind:to="{ name: $store.state.constants.routes.engineSettings, params: { id: item.Id }}">
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
            </router-link>
        </div>
    </div>
</template>

<script>
    import { getCarModelList } from '../Api/data.js';
    import { map, uniqBy } from 'lodash';
    import { saveViewModel } from '../helper.js';

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
            this._buildSeriesList = (data) => uniqBy(map(data, cur => cur.Series), cur => cur);
            getCarModelList().then(data => {
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
            }
        }
    }
</script>