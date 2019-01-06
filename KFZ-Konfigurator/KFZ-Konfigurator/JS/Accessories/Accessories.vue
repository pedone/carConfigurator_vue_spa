<template>
    <div>
        <div>
            <div class="accordion" id="accessoriesAccordion">
                <div class="card" v-for="curCategory in sortedAccessoryCategories">
                    <div class="card-header btn d-flex align-items-start" role="button" v-bind:data-target="'#collapse' + curCategory" data-toggle="collapse">
                        <h5>
                            {{ curCategory }} <!--<span class="badge badge-secondary">{{ getAccessoryCount(curCategory) }}</span>-->
                        </h5>
                    </div>
                    <div class="collapse" data-parent="#accessoriesAccordion" v-bind:id="'collapse' + curCategory">
                        <div class="card-body list-group list-group-flush pr-0"
                             v-for="item in getAccessoriesByCategory(curCategory)">
                            <div class="list-group-item">
                                <div class="row">
                                    <div class="col-auto align-items-center d-flex">
                                        <input type="checkbox"
                                               v-bind:value="item.Id"
                                               v-model="selectedAccessories" />
                                    </div>
                                    <!--<div class="col" @click="toggleAccessorySelection(item.Id)">-->
                                    <div class="col">
                                        <h5 class="card-title">{{ item.Name }}</h5>
                                        <h6 class="card-text">{{ $n(item.Price, 'currency') }}</h6>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-end mt-3">
            <router-link :to="{ name: $store.state.constants.routes.exterior }" class="btn btn-outline-dark">{{ $t('accessoriesView.continueToExteriorButton') }}</router-link>
        </div>
    </div>
</template>

<script>
    import { mapActions, mapMutations } from 'vuex';
    import _ from 'lodash';

    export default {
        created: function () {
            this.loadConfigurationData(this.$route.params.id);
        },
        computed: {
            selectedAccessories: {
                get: function () {
                    return this.$store.state.configuration.accessories;
                },
                set: function (newVal) {
                    this.setSelectedAccessories(newVal);
                }
            },
            sortedAccessoryCategories: function () {
                return _.sortBy(this.$store.state.configurationData.accessoryCategories);
            }
            //accessoryCountByCategory: function () {
            //    let result = _.reduce(this.sortedAccessoryCategories, (result, cur) => {
            //        result[cur] = 0;
            //        return result;
            //    }, {});
            //    _.each(this.$store.state.configurationData.accessories, cur => result[cur.Category] += 1);
            //    return result;
            //}
        },
        methods: {
            ...mapMutations([
                'setSelectedAccessories'
            ]),
            ...mapActions([
                'loadConfigurationData'
            ]),
            getAccessoriesByCategory: function (category) {
                return _.filter(this.$store.state.configurationData.accessories, cur => cur.Category === category);
            }
        }
    }
</script>