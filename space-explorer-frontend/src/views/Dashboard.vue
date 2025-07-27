<template>
  <div class="min-h-screen bg-gray-900 text-white px-6 py-8" style="background-image: radial-gradient(#ffffff11 1px, transparent 1px); background-size: 40px 40px;">
    <h2 class="text-3xl font-bold text-slate-100 mb-6">🌌 Enriched Planet Dashboard</h2>

    <div v-if="planets.length" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-10">
      <PlanetTypeDonut :enrichedPlanets="planets" />
      <MassRadiusScatter :enrichedPlanets="planets" />
      <BioScoreZoneScatter :enrichedPlanets="planets" />
      <AtmosphereBarChart :enrichedPlanets="planets" />
      <BioScoreHistogram :enrichedPlanets="planets" />
      <WaterVsBioScatter :enrichedPlanets="planets" />
      <DiscoveryCountLine :enrichedPlanets="planets" />
      <PlanetRadarChart :enrichedPlanets="planets" />
      <div class="col-span-1 sm:col-span-2 lg:col-span-4">
        <MostRecentPlanetCard :planet="mostRecentPlanet" />
      </div>
    </div>

    <div v-else class="text-slate-300 text-lg">Loading...</div>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue'
import api from '../services/api'
import { fetchEnrichedPlanets } from '../services/api'
import PlanetTypeDonut from '../components/charts/PlanetTypeDonut.vue'
import MassRadiusScatter from '../components/charts/MassRadiusScatter.vue'
import BioScoreZoneScatter from '../components/charts/BioScoreZoneScatter.vue'
import AtmosphereBarChart from '../components/charts/AtmosphereBarChart.vue'
import BioScoreHistogram from '../components/charts/BioScoreHistogram.vue'
import WaterVsBioScatter from '../components/charts/WaterVsBioScatter.vue'
import DiscoveryCountLine from '../components/charts/DiscoveryCountLine.vue'
import PlanetRadarChart from '../components/charts/PlanetRadarChart.vue'
import MostRecentPlanetCard from '../components/charts/MostRecentPlanetCard.vue'

const planets = ref([])
const mostRecentPlanet = ref(null)

onMounted(async () => {
  await api.post('/nasa/sync')
  planets.value = await fetchEnrichedPlanets()
  mostRecentPlanet.value = planets.value.reduce((latest, current) =>
    current.discoveryYear > (latest?.discoveryYear || 0) ? current : latest,
    null
  )
})
</script>
