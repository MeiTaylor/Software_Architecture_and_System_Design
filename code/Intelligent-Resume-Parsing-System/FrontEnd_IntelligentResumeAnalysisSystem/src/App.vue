<script setup>
  import { useStore } from 'vuex'
  const store = useStore()
  store.commit('getLocalStorage')
</script>

<template>
  <router-view></router-view>
</template>

<style scoped>
  #app {
    max-width: 1920px;
    width: 100%;
    margin: 0;
    padding: 0;
  }
</style>