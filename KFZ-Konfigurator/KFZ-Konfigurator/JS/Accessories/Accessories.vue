<template>
    <div>
        <div>
            <div class="accordion" id="accessoriesAccordion">
                <div class="card" v-for="curCategory in sortedAccessoryCategories">
                    <div class="card-header btn d-flex align-items-start" role="button" v-bind:data-target="'#collapse' + curCategory" data-toggle="collapse">
                        <h5>
                            {{ $t('accessoryCategory.' + curCategory) }} <span class="badge badge-secondary">{{ $store.state.configuration.accessories | countAccessoriesByCategory(curCategory) }}</span>
                        </h5>
                    </div>
                    <div class="collapse" data-parent="#accessoriesAccordion" v-bind:id="'collapse' + curCategory">
                        <div class="card-body list-group list-group-flush pr-0"
                             v-for="item in getAccessoriesByCategory(curCategory)">
                            <div class="list-group-item">
                                <div class="row">
                                    <div class="col-auto align-items-center d-flex">
                                        <input type="checkbox"
                                               v-bind:value="item"
                                               v-model="selectedAccessories" />
                                    </div>
                                    <div class="col" @click="toggleSelectedAccessory(item)">
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
            <router-link v-bind:to="{ name: $store.state.constants.routes.exterior }" class="btn btn-outline-dark">{{ $t('accessoriesView.continueToExteriorButton') }}</router-link>
        </div>
    </div>
</template>

<script>
    import { mapActions, mapMutations } from 'vuex';
    import _ from 'lodash';

    export default {
        created: function () {
            this.initConfigurationData({ carModelId: this.$route.params.id });
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
        },
        methods: {
            ...mapMutations([
                'setSelectedAccessories',
                'toggleSelectedAccessory'
            ]),
            ...mapActions([
                'initConfigurationData'
            ]),
            getAccessoriesByCategory(category) {
                return _.filter(this.$store.state.configurationData.accessories, cur => cur.Category === category);
            }
        }
    }
</script>