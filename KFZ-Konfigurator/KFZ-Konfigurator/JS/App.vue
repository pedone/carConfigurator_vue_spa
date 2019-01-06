<template>
    <div>
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
        <router-view></router-view>
    </div>
</template>

<script>
    export default {}
</script>