<script setup lang="ts">
    import BaseEditView from '@/components/BaseEditView.vue';
    import { ref } from 'vue';
    import { summary, findByConditions } from '@/api/SaleSumApi'
    import { SaleSum } from '@/models/SaleSum';

    const SUCCESS_CODE = 200;

    const storeId = ref('');
    const date = ref('');
    const storeIdError = ref('');
    const dateError = ref('');
    const resultList = ref<SaleSum[]>([])

    const message = ref('');

     const summaryAction = () => {

        storeIdValidate()
        dateValidate()

        if (storeId.value.length == 0 || date.value.length == 0) {
            return
        }

        message.value = ''
        summaryApi()
    }

    const summaryApi = async () => {

        var id = storeId.value ?? ''
        var dateStr = date.value ?? ''

        const response = await summary(id, dateStr)
        message.value = response.message

        if (response.code == SUCCESS_CODE) {
            searchApi()
        }
    }

    const searchApi = async () => {

        var id = storeId.value ?? ''
        var dateStr = date.value ?? ''

        const response = await findByConditions(id, dateStr)
            
        console.log(response)
        if (response.code == SUCCESS_CODE) {
            resultList.value = response.data ?? []
        } else {
            message.value = response.message
        }
    }

    const storeIdValidate = () => {
        storeIdError.value = storeId.value.length == 0 ? "店家編號不能為空" : ""
    }

    const dateValidate = () => {
        dateError.value = date.value.length == 0 ? "日期不能為空" : ""
    }


</script>

<template>
    <div class="search_div">
        <BaseEditView
            v-model="storeId" 
            @onBlur="storeIdValidate"
            title="店號" 
            hint="請輸入店號" 
            type="text" 
            :error-text="storeIdError"/>

        <BaseEditView
            v-model="date" 
            @onBlur="dateValidate"
            title="日期" 
            hint="請輸入日期" 
            type="date" 
            :error-text="dateError"/>

        <button class="submit_button" @click="summaryAction">彙整</button>
    </div>

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
            <tr v-for="(sum, index) in resultList" :key="String(sum.id ?? index)">
                <td>{{ sum.store_Id }}</td>
                <td>{{ sum.sale_Time }}</td>
                <td>{{ sum.price }}</td>
                <td>{{ sum.create_Time }}</td>
            </tr>
        </tbody>
    </table>

</template>


<style>


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