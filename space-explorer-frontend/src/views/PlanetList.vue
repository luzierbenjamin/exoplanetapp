<template>
  <div class="flex gap-6">
    <SidebarFilters @filter="applyFilters" />

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 w-full">
      <div
        v-for="planet in filteredPlanets"
        :key="planet.id"
        class="bg-gray-800 p-4 rounded-xl shadow border border-gray-700"
      >
        <div class="font-bold text-lg">{{ planet.name }}</div>
        <div class="text-sm text-gray-300">
          Year: {{ planet.discoveryYear }} | Mass: {{ planet.mass }} | Radius: {{ planet.radius }}
        </div>
        <div class="mt-2 text-sm">
          <strong>Type:</strong> {{ planet.predictions.planetType }}<br />
          <strong>Water:</strong> {{ planet.predictions.waterLikelihood }}<br />
          <strong>BioScore:</strong> {{ planet.predictions.bioScore }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import SidebarFilters from '../components/SidebarFilters.vue'
import { fetchEnrichedPlanets } from '../services/api'

const planets = ref([])
const filters = ref(null)

const applyFilters = (filters) => {
  filteredPlanets.value = planets.value.filter(p => {
    const pred = p.predictions
    const nameMatch = !filters.searchTerm || p.name.toLowerCase().includes(filters.searchTerm)

    const typeMatch = !filters.planetTypes.length || filters.planetTypes.includes(pred.planetType)
    const atmMatch = !filters.atmospheres.length || filters.atmospheres.includes(pred.atmosphere)

    const inRange = (val, min, max) => (min === null || val >= min) && (max === null || val <= max)

    return nameMatch &&
      typeMatch &&
      atmMatch &&
      inRange(p.mass, filters.ranges.mass.min, filters.ranges.mass.max) &&
      inRange(p.orbitalPeriod, filters.ranges.orbitalPeriod.min, filters.ranges.orbitalPeriod.max) &&
      inRange(pred.waterLikelihood, filters.ranges.waterLikelihood.min, filters.ranges.waterLikelihood.max) &&
      inRange(pred.bioScore, filters.ranges.bioScore.min, filters.ranges.bioScore.max)
  })
}

const filteredPlanets = computed(() => {
  if (!filters.value) return planets.value

  return planets.value.filter(p =>
    p.predictions.bioScore >= filters.value.bioScore &&
    p.predictions.waterLikelihood >= filters.value.waterLikelihood &&
    p.mass >= filters.value.mass &&
    p.radius >= filters.value.radius &&
    p.orbitalPeriod >= filters.value.orbitalPeriod
  )
})

onMounted(async () => {
  planets.value = await fetchEnrichedPlanets()
})
</script>