<script setup lang="ts">
import AppHeader from '@/components/AppHeader.vue';
import BaseEditView from '@/components/BaseEditView.vue';
import ToastView from '@/components/ToastView.vue'
import { ref } from 'vue';
import { login } from '@/api/MemberApi'
import router from '@/router';

const account = ref('');
const password = ref('');
const accountError = ref('')
const passwordError = ref('')

const message = ref('')

const showToast = ref(false)
const toastMsg = ref('')

const clickAction = ()=> {

  if (account.value.length == 0 || password.value.length == 0) {
    message.value = "帳號/密碼不能為空"
    return
  }
  loginApi()
}


const loginApi = async () => {

  const response = await login(account.value, password.value)
    
  console.log(response)
  if (response.code == 200) {
    toastMsg.value = response.message
    showToast.value = true
    setTimeout(() => {
      showToast.value = false
      router.replace("/main")
    }, 2000)
  } else {
    message.value = response.message
  }
}

const accountValidate = () => {

    accountError.value = account.value.length == 0 ? "帳號不能為空" : ""
}

const passwordValidate = () => {

  passwordError.value = password.value.length == 0 ? "密碼不能為空" : ""
}

</script>

<template>

  <AppHeader />
    
  <main class="login_div">
    <div class="login_container">
      <BaseEditView 
        v-model="account" 
        @onBlur="accountValidate" 
        title="帳號" hint="請輸入帳號" 
        type="text" 
        :error-text="accountError"/>
      <BaseEditView
        v-model="password" 
        @onBlur="passwordValidate"
        title="密碼" 
        hint="請輸入密碼" 
        type="password" 
        :error-text="passwordError"/>
      
      <p class="message_text" v-show="message.length > 0">{{ message }}</p>

      <button 
        class="submit_button" 
        @click="clickAction">登入</button>
      <div class="link_container">
        <RouterLink 
          to="/register" 
          class="link_text">註冊會員</RouterLink>
      </div>
      <ToastView 
        :message="toastMsg" 
        :show="showToast"/>
    </div>
  </main>
</template>

<style>

.wrapper {
  display: flex;
  align-items: center;
  gap: 12px;
}

</style>