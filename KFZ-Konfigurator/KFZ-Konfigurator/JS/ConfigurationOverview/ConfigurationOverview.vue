<template>
    <div>
        <h2>{{ $t('configurationOverviewView.header') }}</h2>

        <div v-if="isLoading">Loading...</div>
        <configuration-document v-else v-bind:configuration="configuration"></configuration-document>

        <p>
            <button class="btn btn-dark" type="button" data-toggle="collapse" data-target="#orderCollapse">{{ $t('configurationOverviewView.orderButton') }}</button>
        </p>
        <div class="collapse" id="orderCollapse">
            <div class="card card-body">
                <div class="input-group">
                    <input type="text" class="form-control"
                           v-bind:placeholder="$t('configurationOverviewView.orderDescriptionPlaceholder')"
                           v-model="configurationDescription" />
                    <div class="input-group-append">
                        <button class="btn btn-primary" id="placeOrderButton" type="button" data-target="#confirmPlaceOrderDialog" data-toggle="modal">
                            {{ $t('configurationOverviewView.placeOrderButton') }}
                        </button>
                    </div>
                </div>
                <div class="mt-3">
                    <div class="alert alert-danger fade show" role="alert" v-bind:hidden="hasError === false">
                        {{ $t('configurationOverviewView.couldNotPlaceOrderError') }}
                    </div>
                </div>
            </div>
        </div>

        <!-- Confirm Order Modal -->
        <div class="modal fade" id="confirmPlaceOrderDialog" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ $t('configurationOverviewView.orderConfirmTitle') }}</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span>&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        {{ $t('configurationOverviewView.orderConfirmText') }}
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">{{ $t('general.no') }}</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" @click="placeOrder">
                            {{ $t('general.yes') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import ConfigurationDocument from '../ConfigurationDocument/ConfigurationDocument';
    import { map } from 'lodash';
    import { mapActions } from 'vuex';

    export default {
        components: {
            'configuration-document': ConfigurationDocument
        },
        data() {
            return {
                configurationDescription: '',
                hasError: false,
                configuration: null,
                isLoading: false
            };
        },
        created() {
            this.isLoading = true;
            this.initConfigurationData({
                carModelId: this.$route.params.id,
                onSuccess: () => {
                    this.configuration = this.$store.state.configuration;
                    this.isLoading = false;
                }
            });
        },
        inject: ['antiForgeryToken'],
        methods: {
            ...mapActions([
                'initConfigurationData'
            ]),
            placeOrder: function () {
                //extract configuration ids
                const config = this.$store.state.configuration;

                const data = {
                    __RequestVerificationToken: this.antiForgeryToken,
                    description: this.configurationDescription,
                    engineSettings: config.engineSettings.Id,
                    paint: config.paint.Id,
                    rims: config.rims.Id,
                    accessories: map(config.accessories, cur => cur.Id)
                };

                $.ajax({
                    type: 'POST',
                    url: '/ConfigurationOverview/PlaceOrder',
                    data: data,
                    contentType: 'application/x-www-form-urlencoded',
                    dataType: 'text'
                }).done(
                    (data) => {
                        console.debug('order successfully placed');
                        this.$router.push({ name: this.$store.state.constants.routes.orderOverview, params: { guid: data } });
                    })
                    .fail((error) => {
                        console.error('failed to place order: ' + error.responseText + ' (' + error.statusText + ')');
                        console.debug(JSON.stringify(error));
                        this.hasError = true;
                    });
            }
        }
    }
</script>
