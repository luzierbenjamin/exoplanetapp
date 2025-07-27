<template>
  <div class="min-h-screen bg-gray-900 text-white px-6 py-8" style="background-image: radial-gradient(#ffffff11 1px, transparent 1px); background-size: 40px 40px;">
    <h2 class="text-3xl font-bold text-slate-100 mb-6">🌌 Enriched Planet Dashboard</h2>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mb-10">
      <PlanetTypeDonut :enrichedPlanets="planets" />
    </div>

    <div v-if="planets.length === 0" class="text-slate-300 text-lg">Loading...</div>

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
import api from '../services/api'
import PlanetTypeDonut from '../components/charts/PlanetTypeDonut.vue'

const planets = ref([])

onMounted(async () => {
  try {
    await api.post('/nasa/sync')
    planets.value = await fetchEnrichedPlanets()
  } catch (error) {
    console.error("Failed to load planets", error)
  }
})
</script>
