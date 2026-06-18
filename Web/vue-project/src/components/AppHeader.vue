<script setup lang="ts">
import BaseTitleView from '@/components/BaseTitleView.vue'
</script>

<template>
  <header class="header">
    <img
        src="@/assets/logo.svg"
      alt="logo"
      width="30"
      height="30"
    />

    <BaseTitleView text="銷售後台" />
  </header>
</template>

<style scoped>
.header {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 10px;
}
</style>