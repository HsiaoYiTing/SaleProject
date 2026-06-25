<script setup lang="ts">
    import { ref } from 'vue';
    import { importFile } from '@/api/SaleApi'

    const message = ref('');
    const file = ref<File>()

    const fileChange = (event: Event) => {

        const target = event.target as HTMLInputElement
        if (target.files && target.files.length > 0) {
            file.value = target.files[0]
        }
    }

    const uploadAction = async () => {

        message.value = ""

        if (!file.value) {
            alert("請選擇檔案")
            return
        }
        var result = await importFile(file.value)

        message.value = result;
    }

</script>

<template>

    <div class="file_div">
        <input
            class="file_edit"
            type="file"
            accept=".txt"
            @change="fileChange" />
        <button class="file_button" @click="uploadAction">Import</button>
    </div>

    <p class="message_text">{{ message }}</p>

</template>
<style>
    .file_div {
        width: 100%;
        margin-top: 5px;
        display: flex;
        align-items: center;
    }

    .file_edit {
        font-size: medium;
        padding: 5px;
    }

    .file_button {
        padding: 15px;
        font-size: 1rem;
        background-color: rgb(11, 94, 67);
        color: white;
        border-width: 0px;
        align-items: end;
    }
</style>