import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import Register from '../views/Register.vue';
import DashboardWrapper from '../views/DashboardWrapper.vue';
import StudentList from '../views/admin/StudentList.vue';
import StudentDetail from '../views/admin/StudentDetail.vue';
import TeacherList from '../views/admin/TeacherList.vue';
import CourseList from '../views/admin/CourseList.vue';
import TeacherDashboard from '../views/teacher/TeacherDashboard.vue';
import CourseDetail from '../views/teacher/CourseDetail.vue';
import StudentDashboard from '../views/student/StudentDashboard.vue';
import MyProfile from '../views/student/MyProfile.vue';
import { useAuth } from '../composables/useAuth';
import 'vue-router';

declare module 'vue-router' {
  interface RouteMeta {
    requiresAuth?: boolean;
    roles?: string[];
  }
}

const routes = [
  { path: '/', redirect: '/login' },
  { path: '/login', component: Login },
  { path: '/register', component: Register },

  { path: '/dashboard', component: DashboardWrapper, meta: { requiresAuth: true } },

  { path: '/students', component: StudentList, meta: { requiresAuth: true, roles: ['Admin','Teacher'] } },
  { path: '/students/:id', component: StudentDetail, meta: { requiresAuth: true, roles: ['Admin','Teacher'] } },

  { path: '/teachers', component: TeacherList, meta: { requiresAuth: true, roles: ['Admin'] } },

  { path: '/courses', component: CourseList, meta: { requiresAuth: true, roles: ['Admin','Teacher'] } },

  { path: '/teacher/courses/:id', component: CourseDetail, meta: { requiresAuth: true, roles: ['Teacher'] } },

  { path: '/me', component: MyProfile, meta: { requiresAuth: true, roles: ['Student'] } },
  { path: '/student/dashboard', component: StudentDashboard, meta: { requiresAuth: true, roles: ['Student'] } },

];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const { user } = useAuth();
  if (to.meta.requiresAuth && !user.value) return next('/login');

  if (to.meta.roles && to.meta.roles.length > 0) {
    if (!user.value) return next('/login');
    if (!to.meta.roles.includes(user.value.role)) {
      return next('/dashboard');
    }
  }
  return next();
});

export default router;
