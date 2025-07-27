<template>
  <div v-if="planet" class="bg-gray-800 p-4 rounded shadow border border-blue-400">
    <h3 class="text-blue-300 font-semibold mb-2">🆕 Most Recently Discovered</h3>
    <div class="font-bold text-lg">{{ planet.name }}</div>
    <div class="text-sm">
      Discovery Year: {{ planet.discoveryYear }} |
      Mass: {{ planet.mass }} |
      Radius: {{ planet.radius }} |
      Orbital Period: {{ planet.orbitalPeriod }}
    </div>
    <div class="mt-2 text-sm">
      <strong>Planet Type:</strong> {{ planet.predictions.planetType }} <br />
      <strong>Atmosphere:</strong> {{ planet.predictions.atmosphere }} <br />
      <strong>Water Likelihood:</strong> {{ planet.predictions.waterLikelihood }} <br />
      <strong>BioScore:</strong> {{ planet.predictions.bioScore }}
    </div>
  </div>
</template>

<script setup>
defineProps({
  planet: Object
})
</script>
