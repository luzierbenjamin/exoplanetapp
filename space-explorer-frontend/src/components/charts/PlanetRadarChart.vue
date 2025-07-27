<template>
  <div class="bg-gray-800 p-4 rounded shadow mt-6" v-if="selectedPlanet">
    <h3 class="text-white font-semibold mb-2">📡 Radar: {{ selectedPlanet.name }}</h3>
    <Radar :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { Radar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  RadialLinearScale,
  PointElement,
  LineElement,
  Filler
} from 'chart.js'
import { ref, computed } from 'vue'

ChartJS.register(Title, Tooltip, Legend, RadialLinearScale, PointElement, LineElement, Filler)

const props = defineProps({
  enrichedPlanets: Array
})

const selectedPlanet = ref(null)

// Select the first valid one for now (you can make it dynamic later)
selectedPlanet.value = props.enrichedPlanets.find(p =>
  p.mass && p.radius && p.orbitalPeriod && p.predictions?.waterLikelihood !== null
)

const chartData = computed(() => {
  if (!selectedPlanet.value) return {}

  const p = selectedPlanet.value

  // Normalize for radar chart [0–1]
  const normalize = (val, min, max) => (val - min) / (max - min)

  return {
    labels: ['Mass', 'Radius', 'Orbital Period', 'Water Likelihood', 'BioScore'],
    datasets: [{
      label: p.name,
      backgroundColor: 'rgba(96, 165, 250, 0.5)',
      borderColor: '#60a5fa',
      data: [
        normalize(p.mass, 0, 10),
        normalize(p.radius, 0, 10),
        normalize(p.orbitalPeriod, 0, 1000),
        p.predictions.waterLikelihood,
        p.predictions.bioScore
      ]
    }]
  }
})

const chartOptions = {
  responsive: true,
  scales: {
    r: {
      min: 0,
      max: 1,
      pointLabels: { color: '#e5e7eb' },
      angleLines: { color: '#4b5563' },
      grid: { color: '#4b5563' },
      ticks: { display: false }
    }
  },
  plugins: {
    legend: { labels: { color: '#e5e7eb' } }
  }
}
</script>
