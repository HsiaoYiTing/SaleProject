<script setup lang="ts">
  import AppHeader from '@/components/AppHeader.vue';
  import { useMemberStore } from '@/stores/MemberStore';
  import { ref } from 'vue';
  import SaleSearchView from './SaleSearchView.vue';  
  import SaleSummaryView from './SaleSummaryView.vue';
  import SaleImportView from './SaleImportView.vue';

  const memberStore = useMemberStore()
  const currentTab = ref<'search' | 'import' | 'summary'>('search')

  const logout = () => {

    memberStore.logout()
  }

  const searchAction = () => {
    currentTab.value = 'search';
  }

  const importAction = () => {
    currentTab.value = 'import';
  }

  const summaryAction = () => {
    currentTab.value = 'summary';
  }
</script>

<template>

  <AppHeader />
    
  <main class="main_div">

    <div class="member_bg">
      <p class="member_text">{{ memberStore.member?.name }} 你好，</p>
      <RouterLink to="/" class="logout_text" @click="logout">登出</RouterLink> 
    </div>

    <div class="function_div">
      <button @click="summaryAction">彙整資料</button>
      <button @click="searchAction">查詢銷售彙整資料</button>
      <button @click="importAction">新增銷售資料</button>
    </div>

    <SaleSearchView v-if="currentTab === 'search'" />
    <SaleImportView v-if="currentTab === 'import'" />
    <SaleSummaryView v-if="currentTab === 'summary'" />

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

.function_div {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  gap: 5px;
}
.function_div 
  button {
    padding: 5px;
    font-size: medium;
}

.button_div {
  width: 100%;
  display: flex;
  gap: 10px;
}

.search_div {
  width: 100%;
  margin-top: 5px;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
}

.member_text {
  text-align: left;
  margin-top: 5px;
  margin-bottom: 5px;
  margin-left: 20px;
}

.export_button {

  width: calc(100% - 10px);
  padding: 15px;
  font-size: 1rem;
  background-color: rgb(96, 119, 111);
  color: white;
  border-width: 0px;
  margin-top: 20px;
}

.logout_text {
  
  text-decoration: underline;
  text-align: right;
  margin-top: 5px;
  margin-bottom: 5px;
  margin-right: 20px;
}

.resule_table {
    width: 100%;
    margin-top: 20px;
    text-align: center;
    border-collapse: collapse;
}


</style>