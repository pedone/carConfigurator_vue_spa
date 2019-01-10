<template>
    <div>
        <h4>{{ $t('configurationDocument.technicalDataHeader') }}</h4>
        <!-- EngineSettings -->
        <table class="table">
            <tbody>
                <tr>
                    <th scope="row">{{ $t('technicalData.transmission') }}</th>
                    <td>
                        {{ $store.state.configuration.engineSettings.Gears }} {{ $t('technicalData.transmissionSuffix') }}
                        <template v-if="$store.state.configuration.engineSettings.WheelDrive === $store.state.constants.wheelDriveKind.all">
                            {{ $t('technicalData.transmissionAllroad') }}
                        </template>
                    </td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.transmission') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.Acceleration }} s</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.topSpeed') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.TopSpeed }} km/h</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.consumption') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.Consumption }} l/100 km</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.performance') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.Engine.Performance }} kW ({{ $store.state.configuration.engineSettings.Engine.PerformancePS }} PS)</td>
                </tr>
                <tr>
                    <th scope="row">CO<sub>2</sub>-{{ $t('technicalData.emissions') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.Emission }} g/km</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.engineSize') }}</th>
                    <td>{{ $store.state.configuration.engineSettings.Engine.Size }} cm<sup>3</sup></td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.fuelType') }}</th>
                    <td>{{ $t('fuelCategory.' + $store.state.configuration.engineSettings.Engine.FuelKind) }}</td>
                </tr>
            </tbody>
        </table>

        <!-- Accessories -->
        <h4 class="mt-3">{{ $t('configurationDocument.accessoriesHeader') }}</h4>
        <table class="table">
            <tr>
                <th>
                    {{ $t('columnName.name') }}
                </th>
                <th>
                    {{ $t('columnName.category') }}
                </th>
                <th>
                    {{ $t('columnName.price') }}
                </th>
            </tr>

            <tr v-for="item in orderedConfigurationAccessories">
                <td>{{ item.Name }}</td>
                <td>{{ $t('accessoryCategory.'+ item.Category) }}</td>
                <td>{{ item.Price }}</td>
            </tr>
        </table>
        <!-- Exterior -->
        <h4 class="mt-3">{{ $t('configurationDocument.exteriorHeader') }}</h4>
        <table class="table">
            <tbody>
                <tr>
                    <th scope="row">{{ $t('configurationDocument.paintsHeader') }}</th>
                    <td>{{ $t('paintCategory.' + $store.state.configuration.paint.Category) + $store.state.configuration.paint.Name }}</td>
                    <td>{{ $store.state.configuration.paint.Price }}</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('configurationDocument.rimsHeader') }}</th>
                    <td>{{ $store.state.configuration.rims.Size + ' ' + $t('general.rimSizeUnit')}}</td>
                    <td>{{ $store.state.configuration.rims.Price }}</td>
                </tr>
            </tbody>
        </table>

        <hr />
        <p>
            <h4>{{ $t('configurationDocument.basePriceHeader') + ': ' + $n(basePrice, 'currency') }}</h4>
            <h4>{{ $t('configurationDocument.extrasPriceHeader') + ': ' + $n(extrasPrice, 'currency') }}</h4>
            <h4>{{ $t('configurationDocument.finalPriceHeader') + ': ' + $n(fullPrice, 'currency') }}</h4>
        </p>
    </div>
</template>

<script>
    import { mapActions, mapGetters } from 'vuex';

    export default {
        created: function () {
            this.loadConfigurationData(this.$route.params.id);
        },
        computed: {
            ...mapGetters([
                'orderedConfigurationAccessories'
            ]),
            basePrice: function () {
                return this.$store.state.configuration.engineSettings.Price;
            },
            extrasPrice: function () {
                /** @type {number} */
                const accessoriesPrice = _.reduce(this.$store.state.configuration.accessories, (memo, cur) => memo + cur.Price, 0);
                return accessoriesPrice + this.$store.state.configuration.paint.Price + this.$store.state.configuration.rims.Price;
            },
            fullPrice: function () {
                return this.basePrice + this.extrasPrice;
            },
        },
        methods: {
            ...mapActions([
                'loadConfigurationData'
            ])
        }
    }
</script>
