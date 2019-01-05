<template>
    <div>
        <div class="list-group">
            <a v-for="(item, index) in $store.state.configurationData.engineSettings"
               class="list-group-item list-group-item-light list-group-item-action rounded-0 d-flex align-items-lg-center"
               v-bind:class="{ active: index === 0 }"
               data-toggle="list"
               @click="setEngineSettingsId(item.Id)">
                <div class="flex-column w-100 align-items-lg-start">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1">
                            {{ item.Engine.Liter }} {{ $t('engineCategory.' + item.Engine.EngineKind) }} {{ item.Gears }} {{ $t('technicalData.transmissionSuffix') }}
                            <!-- TODO use a constant -->
                            <template v-if="item.WheelDrive === 2">
                                {{ $t('technicalData.transmissionAllroad') }}
                            </template>
                        </h5>
                        <h5 class="mb-1">{{ $n(item.Price, 'currency') }}</h5>
                    </div>
                    <div class="mt-2 row">
                        <span class="col-sm-4">
                            <img src="/Resources/Images/acceleration.svg" class="indicatior-icon" :title="$t('technicalData.transmissionSuffix')" />
                            {{ item.Acceleration }} s
                        </span>
                        <span class="col-sm-4">
                            <img src="/Resources/Images/consumption.svg" class="indicatior-icon" :title="$t('technicalData.consumption')" />
                            {{ item.Consumption }} l/100 km
                        </span>
                        <span class="col-sm-4">
                            <img src="/Resources/Images/performance.svg" class="indicatior-icon" :title="$t('technicalData.performance')" />
                            {{ item.Engine.Performance }} kW ({{ item.Engine.PerformancePS }} PS)
                        </span>
                        <span class="col-sm-4">
                            <img src="/Resources/Images/emissions.svg" class="indicatior-icon" :title="$t('technicalData.emissions')" />
                            {{ item.Emission }} g/km
                        </span>
                        <span class="col-sm-4">
                            <img src="/Resources/Images/topSpeed.svg" class="indicatior-icon" :title="$t('technicalData.topSpeed')" />
                            {{ item.TopSpeed }} km/h
                        </span>
                        <span class="col-sm-4">
                            <img src="/Resources/Images/engineSize.svg" class="indicatior-icon" :title="$t('technicalData.engineSize')" />
                            {{ item.Engine.Size }} cm<sup>3</sup>
                        </span>
                    </div>
                </div>
            </a>
        </div>

        <div class="d-flex justify-content-end mt-3">
            <router-link :to="{ name: 'accessories' }" class="btn btn-outline-dark">{{ $t('engineSettingsView.continueToAccessoriesButton') }}</router-link>
        </div>
    </div>
</template>

<script>
    import { mapActions, mapMutations } from 'vuex';

    export default {
        created: function () {
            this.initConfigurationData();
        },
        methods: {
            ...mapMutations([
                'setEngineSettingsId'
            ]),
            ...mapActions([
                'initConfigurationData'
            ])
        }
    }
</script>