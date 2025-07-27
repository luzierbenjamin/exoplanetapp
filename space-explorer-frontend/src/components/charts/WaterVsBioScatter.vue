<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">Water Likelihood vs BioScore</h3>
    <Scatter :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { Scatter } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  PointElement,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, PointElement, LinearScale)

const props = defineProps({
  enrichedPlanets: Array
})

const chartData = computed(() => {
  const points = props.enrichedPlanets
    .map(p => {
      const wl = p.predictions?.waterLikelihood
      const bio = p.predictions?.bioScore
      return typeof wl === 'number' && typeof bio === 'number'
        ? { x: wl, y: bio }
        : null
    })
    .filter(Boolean)

  return {
    datasets: [{
      label: 'Planets',
      data: points,
      backgroundColor: '#38bdf8'
    }]
  }
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { display: false },
    title: { display: false }
  },
  scales: {
    x: {
      title: { display: true, text: 'Water Likelihood', color: '#e5e7eb' },
      ticks: { color: '#e5e7eb' },
      min: 0, max: 1
    },
    y: {
      title: { display: true, text: 'BioScore', color: '#e5e7eb' },
      ticks: { color: '#e5e7eb' },
      min: 0, max: 10
    }
  }
}
</script>
