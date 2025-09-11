<template>
  <div>
    <h2 class="text-xl font-semibold mb-4">Öğrenciler</h2>

    <div class="form-row">
      <input v-model="newStudent.name" type="text" placeholder="İsim" class="input-field"/>
      <input v-model="newStudent.grade" type="text" placeholder="Sınıf" class="input-field"/>
      <button @click="saveStudent" class="save-btn">Kaydet</button>
    </div>

    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>İsim</th>
          <th>Sınıf</th>
          <th>İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="s in students" :key="s.id" class="border-t">
          <td>{{ s.id }}</td>
          <td>{{ s.name }}</td>
          <td>{{ s.grade }}</td>
          <td>
            <button @click="edit(s)" class="text-yellow-600">Düzenle</button>
            <button @click="remove(s.id)" class="text-red-600">Sil</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

interface Student {
  id: number;
  name: string;
  grade: string;
}

const students = ref<Student[]>([
  { id: 1, name: 'Mehmet Yılmaz', grade: '9A' },
  { id: 2, name: 'Elif Demir', grade: '10B' }
]);

const newStudent = ref<Student>({ id: 0, name: '', grade: '' });

function saveStudent() {
  if (!newStudent.value.name || !newStudent.value.grade) return alert('Tüm alanları doldurun');
  const id = students.value.length ? students.value[students.value.length - 1].id + 1 : 1;
  students.value.push({ ...newStudent.value, id });
  newStudent.value.name = '';
  newStudent.value.grade = '';
}

function edit(s: Student) {
  alert('Düzenle: ' + s.name);
}

function remove(id: number) {
  if (!confirm('Silinsin mi?')) return;
  students.value = students.value.filter(x => x.id !== id);
}
</script>
