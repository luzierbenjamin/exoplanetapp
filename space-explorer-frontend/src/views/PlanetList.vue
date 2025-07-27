<template>
  <div class="min-h-screen bg-gray-900 text-white px-6 py-8">
    <h2 class="text-3xl font-bold mb-6">🪐 All Planets</h2>
    <div v-if="planets.length === 0" class="text-slate-300">Loading...</div>
    <div v-else class="space-y-6">
      <div
        v-for="planet in planets"
        :key="planet.id"
        class="bg-gray-800/90 p-6 rounded-2xl shadow-xl border border-gray-700"
      >
        <div class="font-bold text-xl text-slate-100">{{ planet.name }}</div>
        <div class="text-slate-300 mt-1">
          Discovery Year: {{ planet.discoveryYear }} |
          Mass: {{ planet.mass }} |
          Radius: {{ planet.radius }} |
          Orbital Period: {{ planet.orbitalPeriod }}
        </div>
        <div class="mt-4 text-sm text-slate-200">
          <p><strong>🌍 Planet Type:</strong> {{ planet.predictions.planetType }}</p>
          <p><strong>☁️ Atmosphere:</strong> {{ planet.predictions.atmosphere }}</p>
          <p><strong>💧 Water Likelihood:</strong> {{ planet.predictions.waterLikelihood }}</p>
          <p><strong>🧬 BioScore:</strong> {{ planet.predictions.bioScore }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import { fetchEnrichedPlanets } from '../services/api'

const planets = ref([])

onMounted(async () => {
  planets.value = await fetchEnrichedPlanets()
})
</script>
