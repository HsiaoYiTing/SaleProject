<script setup lang="ts">


const text = defineModel<string | number | null>()
const emit = defineEmits(['onBlur'])

defineProps<{
    title: string,
    hint: string,
    type?: string,
    errorText: string
}>()

const onBlur = () => {
    emit('onBlur')
}

</script>

<template>
  <div>
    <p class="title">{{ title }}</p>
    <input 
        @blur="onBlur"
        class="editView" 
        v-model="text" 
        :placeholder="hint"
        :type="type"
    />
    <p class="errorTxt" v-show="errorText.length > 0">{{errorText}}</p>

  </div>

</template>

<style scoped>

.title {
    padding-top: 5px;
    font-size: 1rem;
    font-weight: bold;
}

.editView {
    
    width: calc(100% - 10px);
    height: 40px;
    font-size: 1rem;
    margin-top: 5px;
}

.errorTxt {
    font-size: 0.8rem;
    font-weight: 400;
    color: red;
}

</style>
