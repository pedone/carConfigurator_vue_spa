<template>
    <div>
        <div>App</div>
        <router-link :to="$route.path + '/about'">About</router-link>
        <router-view></router-view>
    </div>
</template>

<script>
    export default { }
</script>