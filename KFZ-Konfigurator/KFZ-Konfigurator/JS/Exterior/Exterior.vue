<template>
    <div>
        <h2>{{ $t('exteriorView.paintsHeader') }}</h2>
        <div class="container">
            <template v-for="curCategory in $store.state.configurationData.paintCategories">
                <div class="row mt-3">
                    <h5>{{ $t('paintCategory.' + curCategory) }} ({{ getPaintPrice(curCategory) }})</h5>
                </div>
                <div class="row">
                    <div v-for="item in getPaintsByCategory(curCategory)"
                         class="paintContainer"
                         @click="selectPaint(item)">
                        <input type="radio" name="paintOptions" v-bind:value="item.Id"
                               v-bind:class="{ checked: $store.state.configuration.paint === item }" />
                        <figure class="figure m-1">
                            <img v-bind:style="{ background: item.Color }" class="border-0" />
                            <figcaption class="figure-caption">{{ item.Name }}</figcaption>
                        </figure>
                    </div>
                </div>
            </template>
        </div>
        <p />
        <h2>{{ $t('exteriorView.rimsHeader') }}</h2>
        <table class="table">
            <tr v-for="item in $store.state.configurationData.rims">
                <th>
                    <input type="radio" name="rimOptions" class="mr-2" v-bind:value="item" v-model="selectedRim" />
                    <label>{{ item.Size }} {{ $t('general.rimSizeUnit') }}</label>
                </th>
                <td>{{ $n(item.Price, 'currency') }}</td>
            </tr>
        </table>

        <div class="d-flex justify-content-end mt-3">
            <router-link v-bind:to="{ name: $store.state.constants.routes.configurationOverview }" class="btn btn-outline-dark">{{ $t('exteriorView.continueToOverviewButton') }}</router-link>
        </div>
    </div>
</template>

<script>
    import { find, filter } from 'lodash';
    import { mapMutations } from 'vuex';

    export default {
        computed: {
            selectedRim: {
                get() {
                    return this.$store.state.configuration.rims;
                },
                set(value) {
                    this.selectRim(value);
                }
            }
        },
        methods: {
            ...mapMutations([
                'selectPaint',
                'selectRim'
            ]),
            getPaintPrice(category) {
                return find(this.$store.state.configurationData.paints, cur => cur.Category === category).Price;
            },
            getPaintsByCategory(category) {
                return filter(this.$store.state.configurationData.paints, cur => cur.Category === category);
            }
        }
    }
</script>
