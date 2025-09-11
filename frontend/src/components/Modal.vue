<template>
  <transition name="fade">
    <div v-if="open" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div class="bg-white rounded-xl shadow-lg p-6 w-96 relative">
        <button @click="$emit('close')" class="absolute top-2 right-2 text-gray-500 hover:text-gray-700">&times;</button>
        <slot></slot>
      </div>
    </div>
  </transition>
</template>

<script setup lang="ts">
defineProps<{ open: boolean }>();
</script>

<style>
.fade-enter-active, .fade-leave-active { transition: opacity 0.2s; }
.fade-enter-from, .fade-leave-to { opacity: 0; }
</style>
