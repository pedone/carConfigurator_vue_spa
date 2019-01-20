<template>
    <div>
        <h4>{{ $t('configurationDocument.technicalDataHeader') }}</h4>
        <!-- EngineSettings -->
        <table class="table">
            <tbody>
                <tr>
                    <th scope="row">{{ $t('technicalData.transmission') }}</th>
                    <td>
                        {{ configuration.engineSettings.Gears }} {{ $t('technicalData.transmissionSuffix') }}
                        <template v-if="configuration.engineSettings.WheelDrive === $store.state.constants.wheelDriveKind.all">
                            {{ $t('technicalData.transmissionAllroad') }}
                        </template>
                    </td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.transmission') }}</th>
                    <td>{{ configuration.engineSettings.Acceleration }} s</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.topSpeed') }}</th>
                    <td>{{ configuration.engineSettings.TopSpeed }} km/h</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.consumption') }}</th>
                    <td>{{ configuration.engineSettings.Consumption }} l/100 km</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.performance') }}</th>
                    <td>{{ configuration.engineSettings.Engine.Performance }} kW ({{ configuration.engineSettings.Engine.PerformancePS }} PS)</td>
                </tr>
                <tr>
                    <th scope="row">CO<sub>2</sub>-{{ $t('technicalData.emissions') }}</th>
                    <td>{{ configuration.engineSettings.Emission }} g/km</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.engineSize') }}</th>
                    <td>{{ configuration.engineSettings.Engine.Size }} cm<sup>3</sup></td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('technicalData.fuelType') }}</th>
                    <td>{{ $t('fuelCategory.' + configuration.engineSettings.Engine.FuelKind) }}</td>
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
                    <td>{{ $t('paintCategory.' + configuration.paint.Category) + configuration.paint.Name }}</td>
                    <td>{{ configuration.paint.Price }}</td>
                </tr>
                <tr>
                    <th scope="row">{{ $t('configurationDocument.rimsHeader') }}</th>
                    <td>{{ configuration.rims.Size + ' ' + $t('general.rimSizeUnit')}}</td>
                    <td>{{ configuration.rims.Price }}</td>
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
        props: ['configuration'],
        computed: {
            ...mapGetters([
                'orderedConfigurationAccessories',
                'basePrice',
                'extrasPrice',
                'fullPrice',
            ])
        },
        methods: {
            ...mapActions([
                'initConfigurationData'
            ])
        }
    }
</script>
