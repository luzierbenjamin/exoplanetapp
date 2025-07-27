<template>
  <nav class="bg-gray-800 text-white px-6 py-4 shadow-md">
    <div class="flex justify-between items-center">
      <h1 class="text-xl font-bold">🛰️ Space Explorer</h1>
      <ul class="flex space-x-6">
        <li><router-link to="/" class="hover:text-blue-400">Dashboard</router-link></li>
        <li><router-link to="/planets" class="hover:text-blue-400">Planets</router-link></li>
      </ul>
    </div>
  </nav>
</template>
