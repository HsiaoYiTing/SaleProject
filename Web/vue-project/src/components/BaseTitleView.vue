<script setup lang="ts">

defineProps<{
  text: string
}>()
</script>

<template>
  <div class="greetings">
    <h1 class="green">{{ text }}</h1>
  </div>

</template>

<style scoped>
h1 {
  font-size: 1.8rem;
  position: relative;
  white-space: nowrap;
  font-weight: 700;
}

.greetings h1{
  text-align: center;
}

@media (min-width: 1024px) {
  .greetings h1{
    text-align: center;
  }
}

</style>
