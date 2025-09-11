<script setup lang="ts">
import { ref } from "vue";
import TeacherList from "@/views/admin/TeacherList.vue";
import StudentList from "@/views/admin/StudentList.vue";
import CourseList from "@/views/admin/CourseList.vue";

const currentView = ref(null);

function showTeachers() {
  currentView.value = TeacherList;
}

function showStudents() {
  currentView.value = StudentList;
}

function showCourses() {
  currentView.value = CourseList;
}
</script>

<template>
  <div>
    <h1 class="text-2xl font-bold mb-6">Admin Dashboard</h1>
    
<div class="dashboard-buttons">
  <button @click="showTeachers" class="teachers">Öğretmen İşlemleri</button>
  <button @click="showStudents" class="students">Öğrenci İşlemleri</button>
  <button @click="showCourses" class="courses">Ders İşlemleri</button>
</div>


    <transition name="fade" mode="out-in">
      <component :is="currentView" />
    </transition>
  </div>
</template>

<style>
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s ease;
}
.fade-enter-from, .fade-leave-to {
  opacity: 0;
}
.fade-enter-to, .fade-leave-from {
  opacity: 1;
}
</style>
