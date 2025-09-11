<template>
  <div>
    <h2 class="text-xl font-semibold mb-4">Öğretmenler</h2>

    <div class="form-row">
      <input v-model="newTeacher.name" type="text" placeholder="İsim" class="input-field"/>
      <input v-model="newTeacher.subject" type="text" placeholder="Branş" class="input-field"/>
      <button @click="saveTeacher" class="save-btn">Kaydet</button>
    </div>

    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>İsim</th>
          <th>Branş</th>
          <th>İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="t in teachers" :key="t.id" class="border-t">
          <td>{{ t.id }}</td>
          <td>{{ t.name }}</td>
          <td>{{ t.subject }}</td>
          <td>
            <button @click="edit(t)" class="text-yellow-600">Düzenle</button>
            <button @click="remove(t.id)" class="text-red-600">Sil</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

interface Teacher {
  id: number;
  name: string;
  subject: string;
}

const teachers = ref<Teacher[]>([
  { id: 1, name: 'Ahmet Kaya', subject: 'Matematik' },
  { id: 2, name: 'Ayşe Yıldız', subject: 'Fizik' }
]);

const newTeacher = ref<Teacher>({ id: 0, name: '', subject: '' });

function saveTeacher() {
  if (!newTeacher.value.name || !newTeacher.value.subject) return alert('Tüm alanları doldurun');
  const id = teachers.value.length ? teachers.value[teachers.value.length - 1].id + 1 : 1;
  teachers.value.push({ ...newTeacher.value, id });
  newTeacher.value.name = '';
  newTeacher.value.subject = '';
}

function edit(t: Teacher) {
  alert('Düzenle: ' + t.name);
}

function remove(id: number) {
  if (!confirm('Silinsin mi?')) return;
  teachers.value = teachers.value.filter(x => x.id !== id);
}
</script>
