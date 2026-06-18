<script setup lang="ts">
import AppHeader from '@/components/AppHeader.vue';
import { ref } from 'vue';

</script>

<template>

  <AppHeader />
    
  <main class="login_div">
    <div class="login_container">
      <RouterLink to="/" class="link_text">返回</RouterLink> 
    </div>
  </main>
</template>

<style>

.wrapper {
  display: flex;
  align-items: center;
  gap: 12px;
}

.login_div {
  min-height: calc(80vh);
  display: flex;
  justify-content: center;
  align-items: center;
}

.login_container {
  width: 50%;
  padding: 20px;
  border-radius: 10px;
  background-color: #e1fae1;
}


.login_button {

  width: calc(100% - 10px);
  padding: 15px;
  font-size: 1rem;
  background-color: rgb(11, 94, 67);
  color: white;
  border-width: 0px;
  margin-top: 20px;
}

.link_text{
  margin-top: 20px;
  align-items: center;
  text-decoration: underline;
}

</style>