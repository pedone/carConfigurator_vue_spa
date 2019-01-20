<template>
    <div>
        <div class="card rounded-0 container">
            <div class="card-body">
                <router-link v-bind:to="{ name: $store.state.constants.routes.model }">
                    <h5 class="card-title">{{ modelName }}</h5>
                </router-link>
                <div class="card-text">
                    <strong v-if="$store.state.configuration.accessories.length > 0">{{ $t('cartOverviewView.extrasHeader') }}:</strong>
                    <div v-for="item in $store.state.configuration.accessories">
                        <div>{{ item.Name }}</div>
                    </div>
                    <div>
                        <strong>{{ $t('cartOverviewView.paintHeader') }}</strong>: {{ $store.state.configuration.paint.Name }}
                    </div>
                    <div>
                        <strong>{{ $t('cartOverviewView.rimsHeader') }}</strong>: {{ $store.state.configuration.rims.Size + ' ' + $t('general.rimSizeUnit') }}
                    </div>
                    <div class="mt-3">
                        <strong>{{ $t('cartOverviewView.basePriceLabel') }}</strong>: {{ $n(basePrice, 'currency') }}
                    </div>
                    <div>
                        <strong>{{ $t('cartOverviewView.extrasPriceLabel') }}</strong>: {{ $n(extrasPrice, 'currency') }}
                    </div>
                    <div>
                        <strong>{{ $t('cartOverviewView.fullPriceLabel') }}</strong>: {{ $n(fullPrice, 'currency') }}
                    </div>
                </div>
            </div>
            <router-link v-if="$route.name !== $store.state.constants.routes.configurationOverview"
                         class="btn btn-info row"
                         v-bind:to="{ name: $store.state.constants.routes.configurationOverview }">{{ $t('cartOverviewView.goToOverview') }}</router-link>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';

    export default {
        computed: {
            ...mapGetters([
                'modelName',
                'basePrice',
                'extrasPrice',
                'fullPrice',
            ])
        }
    }
</script>
