<!-- PlanetDetail.vue -->
<template>
  <div class="min-h-screen bg-gray-900 text-white p-6">
    <div v-if="loading" class="text-gray-400">Loading planet...</div>
    <div v-else-if="planet" class="max-w-4xl mx-auto bg-gray-800 p-6 rounded-xl shadow space-y-6">
      <h2 class="text-3xl font-bold">{{ planet.name }}</h2>

      <ObservationDisplay :method="planet.discoveryMethod" :starType="planet.starType" />

      <div class="text-gray-300 space-y-2">
        <p><strong>Discovery Year:</strong> {{ planet.discoveryYear }}</p>
        <p><strong>Mass:</strong> {{ planet.mass }}</p>
        <p><strong>Radius:</strong> {{ planet.radius }}</p>
        <p><strong>Orbital Period:</strong> {{ planet.orbitalPeriod }}</p>
        <p><strong>Distance:</strong> {{ planet.distance }}</p>
      </div>

      <div class="text-gray-200 border-t border-gray-700 pt-4 space-y-1">
        <p><strong>🌍 Planet Type:</strong> {{ planet.predictions.planetType }}</p>
        <p><strong>☁️ Atmosphere:</strong> {{ planet.predictions.atmosphere }}</p>
        <p><strong>💧 Water Likelihood:</strong> {{ planet.predictions.waterLikelihood }}</p>
        <p><strong>🧬 BioScore:</strong> {{ planet.predictions.bioScore }}</p>
      </div>
    </div>

    <div v-else class="text-red-400">Planet not found.</div>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import api from '../services/api'
import ObservationDisplay from '../components/ObservationDisplay.vue'

const route = useRoute()
const planet = ref(null)
const loading = ref(true)

onMounted(async () => {
  try {
    const res = await api.get(`/planets/${route.params.id}`)
    planet.value = res.data
  } catch (err) {
    console.error('Error loading planet:', err)
  } finally {
    loading.value = false
  }
})
</script>
