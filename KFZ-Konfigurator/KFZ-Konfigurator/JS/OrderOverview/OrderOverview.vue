<template>
    <div v-if="isLoading">Loading...</div>
    <div v-else>
        <div v-if="$route.meta.orderSuccess" class="mt-3 alert alert-success alert-dismissible fade show" role="alert">
            <h4>{{ $t('orderOverviewView.orderSuccessfulHeader') }}</h4>
            <p>
                <div></div>
                <div>
                    {{ $t('orderOverviewView.orderSuccessfulDescription_2') }}
                    <a href="#" @click="copyLinkToClipboard">{{ $t('orderOverviewView.copyOrderLinkButton') }}</a>
                    {{ $t('orderOverviewView.orderSuccessfulDescription_3') }}
                    <input type="hidden" v-bind:value="orderData.link" ref="orderLinkContent" />
                </div>
            </p>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>

        <h2 class="mt-3">{{ $t('orderOverviewView.header') + ' - ' + orderData.model }}</h2>
        <p>
            <div>
                {{ $d(orderData.dateTime, 'long') }}
            </div>
            <div>
                {{ orderData.description }}
            </div>
        </p>

        <div class="container mt-3">
            <configuration-document v-bind:configuration="configuration"></configuration-document>
        </div>
    </div>
</template>

<script>
    import ConfigurationDocument from '../ConfigurationDocument/ConfigurationDocument';
    import { getOrderData } from '../Api/data';

    export default {
        data() {
            return {
                configuration: null,
                isLoading: false,
                orderData: {
                    description: '',
                    dateTime: null,
                    model: '',
                    link: ''
                }
            };
        },
        components: {
            'configuration-document': ConfigurationDocument
        },
        created() {
            this.isLoading = true;
            getOrderData(this.$route.params.guid).then(data => {
                this.configuration = {
                    engineSettings: data.Configuration.EngineSettings,
                    accessories: data.Configuration.Accessories,
                    paint: data.Configuration.Paint,
                    rims: data.Configuration.Rims
                };
                this.orderData = {
                    description: data.Description,
                    dateTime: new Date(parseInt(data.DateTime.substr(6))), // parse the format /Date(1547918048320)/
                    model: data.Model,
                    link: this.$router.resolve({ to: this.$store.state.constants.routes.orderOverview }).href
                };
                this.isLoading = false;
            });
        },
        methods: {
            copyLinkToClipboard() {
                const orderLink = this.$refs.orderLinkContent;
                orderLink.setAttribute('type', 'text');
                orderLink.select();
                document.execCommand('copy');
                orderLink.setAttribute('type', 'hidden');
            }
        }
    }
</script>
