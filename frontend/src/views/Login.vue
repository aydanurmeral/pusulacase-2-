<template>
  <div class="auth-page">
    <div class="container">
      <h2>Login</h2>
      <form @submit.prevent="login">
        <input v-model="username" type="text" placeholder="Username" name="username" id="username" />
        <input v-model="password" type="password" placeholder="Password" name="password" id="password" />
<button type="submit" class="login-btn">Login</button>
      </form>

      <p v-if="alertMessage" class="alert">{{ alertMessage }}</p>
      <p v-if="successMessage" class="success">{{ successMessage }}</p>

      <p class="mt-3 text-center">
        Don't have an account? 
        <router-link to="/register" class="underline text-blue-500">Register</router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";
import { useAuth } from '../composables/useAuth';

const username = ref("");
const password = ref("");
const alertMessage = ref("");
const successMessage = ref("");
const router = useRouter();
const { setUser } = useAuth();

const login = async () => {
  alertMessage.value = "";
  successMessage.value = "";

  if (!username.value || !password.value) {
    alertMessage.value = "Please fill in all fields!";
    return;
  }

  try {
   const response = await axios.post("http://localhost:5166/api/Account/login", {
  Username: username.value,
  Password: password.value
});


    const token = response.data.token;
    localStorage.setItem("token", token);


    const payload = JSON.parse(atob(token.split('.')[1]));
    const rolesClaim = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    const role = Array.isArray(rolesClaim) ? rolesClaim[0] : rolesClaim;
    const usernameFromToken = payload['unique_name'] || username.value;

    setUser({ username: usernameFromToken, role });

    successMessage.value = "Login successful! Redirecting...";
    router.push("/dashboard");
  } catch (err: any) {
    alertMessage.value = "Username or password incorrect";
  }
};
</script>

<style scoped>
.alert { color: #ff7675; text-align: center; margin-top: 1rem; font-weight: 500; animation: shake 0.3s ease; }
.success { color: #55efc4; text-align: center; margin-top: 1rem; font-weight: 500; }
</style>
