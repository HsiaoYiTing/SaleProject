<script setup lang="ts">
import AppHeader from '@/components/AppHeader.vue';
import router from '@/router';
import { useMemberStore } from '@/stores/MemberStore';

const memberStore = useMemberStore()

const logout = () => {

  memberStore.logout()
}

</script>

<template>

  <AppHeader />
    
  <main class="main_div">

    <div class="member_bg">
      <p class="member_text">{{ memberStore.member?.name }} 你好，</p>
      <RouterLink to="/" class="logout_text" @click="logout">登出</RouterLink> 
    </div>

    <div >
    </div>
  </main>
</template>

<style>

.wrapper {
  display: flex;
  align-items: center;
  gap: 12px;
}

.main_div {
  width: 100%;
  min-height: calc(80vh);
}

.member_bg {
  width: 100%;
  display: grid;
  grid-template-columns: 1fr auto;
  background-color: aliceblue;
}

.member_text {
  text-align: left;
  margin-top: 5px;
  margin-bottom: 5px;
  margin-left: 20px;
}

.logout_text {
  
  text-decoration: underline;
  text-align: right;
  margin-top: 5px;
  margin-bottom: 5px;
  margin-right: 20px;
}
</style>