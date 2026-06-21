<script setup lang="ts">
import AppHeader from '@/components/AppHeader.vue';
import BaseEditView from '@/components/BaseEditView.vue';
import { useMemberStore } from '@/stores/MemberStore';
import { ref } from 'vue';
import { findByConditions } from '@/api/SaleSumApi'
import { SaleSum } from '@/models/SaleSum';

const SUCCESS_CODE = 200;

const memberStore = useMemberStore()

const storeId = ref('');
const startDate = ref('');
const endDate = ref('');
const resuleList = ref<SaleSum[]>([])

const message = ref('');

const logout = () => {

  memberStore.logout()
}

const searchAction = () => {

  message.value = ''
  searchApi()
}


const searchApi = async () => {

  var id = storeId.value ?? ''
  var start = startDate.value ?? ''
  var end = endDate.value ?? ''

  const response = await findByConditions(id, start, end)
    
  console.log(response)
  if (response.code == SUCCESS_CODE) {
    resuleList.value = response.data ?? []
  } else {
    message.value = response.message
  }
}

</script>

<template>

  <AppHeader />
    
  <main class="main_div">

    <div class="member_bg">
      <p class="member_text">{{ memberStore.member?.name }} 你好，</p>
      <RouterLink to="/" class="logout_text" @click="logout">登出</RouterLink> 
    </div>

    <div class="search_div">
      <BaseEditView
        v-model="storeId" 
        title="店號" 
        hint="請輸入店號" 
        type="text" 
        error-text=""/>

      <BaseEditView
        v-model="startDate" 
        title="起始日期" 
        hint="請輸入起始日期" 
        type="date" 
        error-text=""/>

      <BaseEditView
        v-model="endDate" 
        title="結束日期" 
        hint="請輸入結束日期" 
        type="date" 
        error-text=""/>

    </div>

    <button class="submit_button" @click="searchAction">搜尋</button>

      <p class="message_text" v-show="message.length > 0">{{ message }}</p>

      <table class="resule_table">
        <thead>
        <tr class="title_tr">
            
          <th>店號</th>
          <th>銷售日期</th>
          <th>銷售金額</th>
          <th>建立日期</th>
        </tr>
        </thead>

        <tbody>
        <tr v-for="(sum, index) in resuleList" :key="String(sum.id ?? index)">
            <td>{{ sum.store_Id }}</td>
            <td>{{ sum.sale_Time }}</td>
            <td>{{ sum.price }}</td>
            <td>{{ sum.create_Time }}</td>
        </tr>
        </tbody>
      </table>

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