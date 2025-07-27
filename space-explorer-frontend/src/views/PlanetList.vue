<template>
  <div class="flex gap-6">
    <div class="flex gap-4 mb-4">
        <button @click="exportToCSV" class="bg-green-600 hover:bg-green-700 px-3 py-1 rounded text-white">Export CSV</button>
        <button @click="exportToPDF" class="bg-blue-600 hover:bg-blue-700 px-3 py-1 rounded text-white">Export PDF</button>
    </div>

    <SidebarFilters @filter="applyFilters" />
    <div id="planet-list-container" class="space-y-6">
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
  </div>
</template>

<script setup>
import Papa from 'papaparse'
import jsPDF from 'jspdf'
import html2canvas from 'html2canvas'
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

const exportToCSV = () => {
  const rows = filteredPlanets.value.map(p => ({
    Name: p.name,
    Mass: p.mass,
    Radius: p.radius,
    OrbitalPeriod: p.orbitalPeriod,
    DiscoveryYear: p.discoveryYear,
    PlanetType: p.predictions.planetType,
    Atmosphere: p.predictions.atmosphere,
    WaterLikelihood: p.predictions.waterLikelihood,
    BioScore: p.predictions.bioScore
  }));

  const csv = Papa.unparse(rows);
  const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
  const link = document.createElement("a");
  link.href = URL.createObjectURL(blob);
  link.download = "filtered_planets.csv";
  link.click();
}

const exportToPDF = async () => {
  const container = document.getElementById('planet-list-container')
  const canvas = await html2canvas(container)
  const imgData = canvas.toDataURL('image/png')
  const pdf = new jsPDF({ orientation: 'portrait', unit: 'pt', format: 'a4' })
  const width = 500
  const height = canvas.height * (width / canvas.width)
  pdf.addImage(imgData, 'PNG', 40, 40, width, height)
  pdf.save('filtered_planets.pdf')
}
</script>