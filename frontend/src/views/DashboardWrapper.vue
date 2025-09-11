<template>
  <div class="min-h-screen flex">
    <aside class="w-64 p-4 bg-gray-50 border-r">
      <div class="mb-6">
        <h3 class="text-lg font-semibold">SchoolApp</h3>
        <p class="text-sm text-gray-500">Welcome, {{ user?.username }}</p>
      </div>
      <div class="mt-6">
<button @click="doLogout" class="logout-btn">Çıkış</button>
      </div>
    </aside>

    <main class="flex-1 p-6">
      <component :is="roleComponent" />
    </main>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue';
import { useAuth } from '../composables/useAuth';
import AdminDashboard from '../views/admin/AdminDashboard.vue';
import TeacherDashboard from '../views/teacher/TeacherDashboard.vue';
import StudentDashboard from './student/StudentDashboard.vue';
import { useRouter } from 'vue-router';

const { user, logout } = useAuth();
const router = useRouter();

const roleComponent = computed(() => {
  if (!user.value) return StudentDashboard;
  if (user.value.role === 'Admin') return AdminDashboard;
  if (user.value.role === 'Teacher') return TeacherDashboard;
  return StudentDashboard;
});

function doLogout() {
  logout();
  router.push('/login');
}
</script>
