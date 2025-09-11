<template>
  <div class="auth-page">
    <div class="container">
      <h2>Register</h2>
      <form @submit.prevent="register">
        <input v-model="username" type="text" placeholder="Username" />
        <input v-model="fullName" type="text" placeholder="Full Name" />
        <input v-model="password" type="password" placeholder="Password" />
        <input v-model="confirmPassword" type="password" placeholder="Confirm Password" />

        <select v-model="role">
          <option value="" disabled selected>Select the role</option>
          <option value="Student">Student</option>
          <option value="Teacher">Teacher</option>
          <option value="Admin">Admin</option>
        </select>

<button type="submit" class="register-btn">Register</button>
      </form>

      <p v-if="alertMessage" class="alert">{{ alertMessage }}</p>
      <p v-if="successMessage" class="success">{{ successMessage }}</p>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const username = ref("");
const fullName = ref("");
const password = ref("");
const confirmPassword = ref("");
const role = ref("");
const alertMessage = ref("");
const successMessage = ref("");
const router = useRouter();

const register = async () => {
  alertMessage.value = "";
  successMessage.value = "";


  if (!username.value || !fullName.value || !password.value || !confirmPassword.value || !role.value) {
    alertMessage.value = "All fields are required!";
    return;
  }


  const uppercase = /[A-Z]/.test(password.value);
  const lowercase = /[a-z]/.test(password.value);
  const number = /\d/.test(password.value);
  const specialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password.value);

  if (password.value.length < 6) {
    alertMessage.value = "Password must be at least 6 characters!";
    return;
  }
  if (!uppercase || !lowercase || !number || !specialChar) {
    alertMessage.value = "Password must include uppercase, lowercase, number, and special character!";
    return;
  }

  if (password.value !== confirmPassword.value) {
    alertMessage.value = "Passwords do not match!";
    return;
  }

  try {
    await axios.post("http://localhost:5166/api/Account/register", {
      username: username.value,
      fullName: fullName.value,
      password: password.value,
      role: role.value
    });

    successMessage.value = "User registered successfully!";
    setTimeout(() => router.push("/login"), 1500);
  } catch (err: any) {
    alertMessage.value = err.response?.data || "Registration failed!";
  }
};
</script>

<style scoped>
.alert {
  color: #ff7675;
  text-align: center;
  margin-top: 1rem;
  font-weight: 500;
  animation: shake 0.3s ease;
}
.success {
  color: #55efc4;
  text-align: center;
  margin-top: 1rem;
  font-weight: 500;
}
</style>
