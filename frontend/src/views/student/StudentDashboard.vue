<template>
  <div>
    <h1 class="text-2xl font-bold mb-6">Öğrenci Paneli</h1>

    <div class="grid">


      <div class="card">
        <h2>Kişisel Bilgiler</h2>
        <button @click="viewInfo" class="action-btn add">Bilgilerimi Görüntüle</button>
      </div>

    
      <div class="card">
        <h2>Notlarım</h2>
        <button @click="viewGrades" class="action-btn list">Notlarımı Göster</button>
      </div>

     
      <div class="card">
        <h2>Devamsızlık Durumu</h2>
        <button @click="viewAbsences" class="action-btn update">Devamsızlık Butonu</button>
      </div>

    </div>

   
    <div class="mt-6">
      <div v-for="c in courses" :key="c.id" class="p-4 mb-3 bg-white rounded shadow">
        <div class="flex justify-between">
          <div>
            <h3 class="font-semibold">{{ c.title }}</h3>
            <div class="text-sm text-gray-500">Hoca: {{ c.teacher }}</div>
          </div>
          <div class="text-right">
            <div class="font-semibold">{{ c.grade ?? '—' }}</div>
            <div class="text-xs text-gray-500">Devamsızlık: {{ c.absences }}</div>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from "vue";
import api from "@/services/api";

interface Course {
  id: number;
  title: string;
  teacher: string;
  grade?: number;
  absences: number;
}

const courses = ref<Course[]>([]);

async function fetchCourses() {
  const token = localStorage.getItem("token");
  const res = await api.get("/courses", {
    headers: { Authorization: `Bearer ${token}` },
  });
  courses.value = res.data.map((c: any) => ({
    id: c.id,
    title: c.title,
    teacher: c.teacherId, 
    grade: c.grade ?? null,
    absences: c.absences ?? 0,
  }));
}

onMounted(() => {
  fetchCourses();
});

function viewInfo() { alert("Bilgiler gösterilecek"); }
function viewGrades() { alert("Notlar gösterilecek"); }
function viewAbsences() { alert("Devamsızlık gösterilecek"); }
</script>

