<script setup lang="ts">

defineProps<{
  message: string
  show: boolean
}>()

</script>

<template>
  <Transition name="toast">
    <div v-if="show" class="toast">
      {{ message }}
    </div>
  </Transition>
</template>

<style scoped>

.toast {
  position: fixed;

  left: 50%;
  bottom: 30px;

  transform: translateX(-50%);

  padding: 12px 20px;

  background: rgba(0, 0, 0, 0.8);
  color: white;

  border-radius: 8px;

  z-index: 9999;
}

.toast-enter-active,
.toast-leave-active {
  transition: all .3s;
}

.toast-enter-from,
.toast-leave-to {
  opacity: 0;
  transform: translate(-50%, 20px);
}

</style>