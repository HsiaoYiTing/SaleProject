<script setup lang="ts">
import { useMemberStore } from './stores/MemberStore';

const memberStore = useMemberStore()
memberStore.loadMember()

</script>

<template>
  <RouterView />
</template>

<style scoped>
</style>
