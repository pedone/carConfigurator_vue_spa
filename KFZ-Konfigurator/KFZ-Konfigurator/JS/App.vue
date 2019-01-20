<template>
    <div class="container body-content">
        <div class="row align-items-md-start">
            <div class="col pt-2">
                <router-view></router-view>
            </div>
        </div>
        <hr />
        <footer>
            <p>&copy; {{ new Date().getFullYear() + ' - ' + $t('general.productTitle') }}</p>
        </footer>
    </div>
</template>

<script>
    export default {}
</script>