<template>
  <div class="p-6 text-white">
    <h2 class="text-2xl font-bold mb-4">🌌 Enriched Planet Dashboard</h2>
    <div v-if="planets.length === 0">Loading...</div>
    <div v-else class="space-y-4">
      <div v-for="planet in planets" :key="planet.id" class="bg-gray-800 p-4 rounded shadow">
        <div class="font-bold text-lg">{{ planet.name }}</div>
        <div class="text-sm">
          Discovery Year: {{ planet.discoveryYear }} |
          Mass: {{ planet.mass }} |
          Radius: {{ planet.radius }} |
          Orbital Period: {{ planet.orbitPeriod }}
        </div>
        <div class="mt-2 text-sm">
          <strong>Planet Type:</strong> {{ planet.predictions.planetType }} <br />
          <strong>Atmosphere:</strong> {{ planet.predictions.atmosphere }} <br />
          <strong>Water Likelihood:</strong> {{ planet.predictions.waterLikelihood }} <br />
          <strong>BioScore:</strong> {{ planet.predictions.bioScore }}
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
