<template>
  <div class="max-w-md mx-auto p-6 mt-20 bg-gray-800 rounded-lg text-white shadow-lg">
    <h2 class="text-2xl font-semibold mb-4">🔐 Login</h2>
    <form @submit.prevent="handleLogin">
      <div class="mb-4">
        <label class="block mb-1">Email</label>
        <input type="email" v-model="email" class="w-full p-2 rounded bg-gray-700 text-white" />
      </div>
      <div class="mb-4">
        <label class="block mb-1">Password</label>
        <input type="password" v-model="password" class="w-full p-2 rounded bg-gray-700 text-white" />
      </div>
      <button type="submit" class="bg-blue-500 hover:bg-blue-600 text-white py-2 px-4 rounded">
        Login
      </button>
    </form>
    <div v-if="error" class="mt-4 text-red-400">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { login } from '../services/api'

const router = useRouter()
const email = ref('')
const password = ref('')
const error = ref(null)

const handleLogin = async () => {
  try {
    const token = await login(email.value, password.value)
    localStorage.setItem('token', token)
    router.push('/dashboard')
  } catch (err) {
    error.value = 'Login failed. Check your credentials.'
  }
}
</script>
