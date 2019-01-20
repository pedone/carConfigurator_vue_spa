<template>
    <div>
        <div class="bg-light">
            <div id="configurationHeaderCarousel" class="container text-overlap-container carousel slide" data-ride="carousel" data-interval="15000">
                <ol class="carousel-indicators dark">
                    <li v-for="(value, _key, index) in headerImages" data-target="#configurationHeaderCarousel" v-bind:data-slide-to="index"
                        v-bind:class="{ active: activeHeaderImage === value }"></li>
                </ol>
                <div class="carousel-inner">
                    <div v-for="value in headerImages" class="carousel-item"
                         v-bind:class="{ active: activeHeaderImage === value }">
                        <img class="img-fluid" v-bind:src="'/Resources/Images/' + value">
                    </div>
                </div>
                <h3 class="overlap-top-left hidden-xs" v-if="$route.name !== $store.state.constants.routes.model">{{ modelName }}</h3>
            </div>
        </div>
        <div class="navbar bg-dark navbar-dark navbar-expand">
            <div class="container"
                 v-bind:class="{ invisible: $route.name === $store.state.constants.routes.model }">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#configurationNavbarToggleContent">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse" id="configurationNavbarToggleContent">
                    <div class="navbar-nav">
                        <router-link class="nav-item nav-link"
                                     v-bind:to="{ name: $store.state.constants.routes.engineSettings }"
                                     v-bind:class="{ active: $route.name === $store.state.constants.routes.engineSettings }">
                            {{ $t('app.engineSettingsButton') }}
                        </router-link>
                        <router-link class="nav-item nav-link"
                                     v-bind:to="{ name: $store.state.constants.routes.accessories }"
                                     v-bind:class="{ active: $route.name === $store.state.constants.routes.accessories }">
                            {{ $t('app.accessoriesButton') }}
                        </router-link>
                        <router-link class="nav-item nav-link"
                                     v-bind:to="{ name: $store.state.constants.routes.exterior }"
                                     v-bind:class="{ active: $route.name === $store.state.constants.routes.exterior }">
                            {{ $t('app.exteriorButton') }}
                        </router-link>
                    </div>
                </div>
            </div>
        </div>

        <div class="container body-content">
            <div class="row align-items-md-start">
                <div class="col pt-2">
                    <router-view></router-view>
                </div>
                <div class="col-md-auto sticky-top sticky-top-offset mt-2">
                    <cart-overview v-if="$route.name !== $store.state.constants.routes.model && isConfigurationLoaded"></cart-overview>
                </div>
            </div>
            <hr />
            <footer>
                <p>&copy; {{ new Date().getFullYear() + ' - ' + $t('general.productTitle') }}</p>
            </footer>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import CartOverview from './CartOverview/CartOverview';

    export default {
        components: {
            'cart-overview': CartOverview
        },
        data() {
            return {
                headerImages: {
                    [this.$store.state.constants.routes.model]: '2018A4AngularFront.gif',
                    [this.$store.state.constants.routes.engineSettings]: '2018A4Engine.gif',
                    [this.$store.state.constants.routes.accessories]: '2018A4Front.gif',
                    [this.$store.state.constants.routes.configurationOverview]: '2018A4AngularRear.gif',
                    [this.$store.state.constants.routes.exterior]: '2018A4Side.gif',
                    'noDefault1': '2018A4Rear.gif',
                }
            };
        },
        computed: {
            ...mapGetters([
                'modelName',
                'isConfigurationLoaded'
            ]),
            activeHeaderImage() {
                return this.headerImages[this.$route.name] || Object.values(this.headerImages)[0];
            }
        }
    }
</script>