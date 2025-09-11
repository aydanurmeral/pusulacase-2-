<template>
  <div>
    <h2 class="text-xl font-semibold mb-4">Devamsızlık & Notlar - {{ course.title }}</h2>

    <table class="w-full bg-white rounded shadow">
      <thead class="bg-gray-50"><tr><th>Öğrenci</th><th>Not</th><th>Devamsız</th><th>Kaydet</th></tr></thead>
      <tbody>
        <tr v-for="s in students" :key="s.id" class="border-t">
          <td class="p-2">{{ s.name }}</td>
          <td class="p-2"><input v-model="s.grade" class="border px-2 py-1 rounded w-20" /></td>
          <td class="p-2"><input type="checkbox" v-model="s.absent" /></td>
          <td class="p-2"><button @click="save(s)" class="py-1 px-3 border rounded">Kaydet</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from 'vue';
const course = { id: 1, title: 'Matematik 9A' };
const students = ref([
  { id: 1, name: 'Mehmet', grade: 80, absent: false },
  { id: 2, name: 'Elif', grade: 90, absent: false }
]);

function save(s) {
  
  alert(`Kaydedildi: ${s.name} => not ${s.grade}, devamsız ${s.absent}`);
}
</script>
