<template>
  <div>
    <h1 class="text-2xl font-bold mb-6">Teacher Dashboard</h1>

    <div class="grid">

    
      <div class="card">
        <h2 class="card-title">Öğrenci İşlemleri</h2>
        <div class="form-row mb-4">
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
                <button @click="editStudent(s)" class="text-yellow-600">Düzenle</button>
                <button @click="removeStudent(s.id)" class="text-red-600">Sil</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

  
      <div class="card">
        <h2 class="card-title">Derslerim</h2>
        <div class="form-row mb-4">
          <input v-model="newCourse.title" type="text" placeholder="Ders Adı" class="input-field"/>
          <select v-model="newCourse.status" class="input-field">
            <option value="">Durum Seç</option>
            <option value="Aktif">Aktif</option>
            <option value="Pasif">Pasif</option>
          </select>
          <button @click="saveCourse" class="save-btn">Kaydet</button>
        </div>

        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Ders Adı</th>
              <th>Durum</th>
              <th>İşlemler</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="c in courses" :key="c.id" class="border-t">
              <td>{{ c.id }}</td>
              <td>{{ c.title }}</td>
              <td>{{ c.status }}</td>
              <td>
                <router-link :to="`/teacher/courses/${c.id}`" class="text-blue-600">Aç</router-link>
                <router-link :to="`/teacher/courses/${c.id}/attendance`" class="text-blue-600 ml-2">Devamsızlık/Not</router-link>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import api from "@/services/api";

interface Student { id: number; name: string; grade: string; }
interface Course { id: number; title: string; status: string; }

const students = ref<Student[]>([]);
const courses = ref<Course[]>([]);

const newStudent = ref<{ name: string; grade: string }>({ name: "", grade: "" });
const newCourse = ref<Course>({ id: 0, title: "", status: "" });

const token = localStorage.getItem("token");


async function loadStudents() {
  try {
    const res = await api.get("/student", { headers: { Authorization: `Bearer ${token}` } });
    students.value = res.data;
  } catch (e) { console.error(e); }
}

async function loadCourses() {
  try {
    const res = await api.get("/courses", { headers: { Authorization: `Bearer ${token}` } });
    courses.value = res.data;
  } catch (e) { console.error(e); }
}

onMounted(() => {
  loadStudents();
  loadCourses();
});


async function saveStudent() {
  if (!newStudent.value.name || !newStudent.value.grade) return alert("Tüm alanları doldurun");

  try {
    const res = await api.post("/student", newStudent.value, { headers: { Authorization: `Bearer ${token}` } });
    students.value.push(res.data);
    newStudent.value.name = "";
    newStudent.value.grade = "";
  } catch (err) {
    console.error(err);
    alert("Öğrenci kaydedilemedi!");
  }
}


async function removeStudent(id: number) {
  if (!confirm("Öğrenciyi silmek istediğinizden emin misiniz?")) return;

  try {
    await api.delete(`/student/${id}`, { headers: { Authorization: `Bearer ${token}` } });
    students.value = students.value.filter(s => s.id !== id);
  } catch (err) {
    console.error(err);
    alert("Öğrenci silinemedi!");
  }
}


function editStudent(s: Student) {
  const newName = prompt("Yeni isim", s.name);
  const newGrade = prompt("Yeni sınıf", s.grade);
  if (!newName || !newGrade) return;

  api.put(`/student/${s.id}`, { name: newName, grade: newGrade }, { headers: { Authorization: `Bearer ${token}` } })
    .then(() => {
      s.name = newName;
      s.grade = newGrade;
    })
    .catch(err => {
      console.error(err);
      alert("Güncelleme başarısız!");
    });
}
async function saveCourse() {
  if (!newCourse.value.title || !newCourse.value.status)
    return alert("Tüm alanları doldurun");

  try {
    const courseToSave = {
      title: newCourse.value.title,
      status: newCourse.value.status,
      teacherId: 1 
    };

    const res = await api.post("/courses", courseToSave, {
      headers: { Authorization: `Bearer ${token}` }
    });

    courses.value.push(res.data);
    newCourse.value.title = "";
    newCourse.value.status = "";
  } catch (err) {
    console.error(err);
    alert("Ders kaydedilemedi!");
  }
}


</script>