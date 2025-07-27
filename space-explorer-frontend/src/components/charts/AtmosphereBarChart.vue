<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">Atmosphere Composition</h3>
    <Bar :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

const props = defineProps({
  enrichedPlanets: Array
})

const chartData = computed(() => {
  const counts = {}
  props.enrichedPlanets.forEach(p => {
    const atm = p.predictions?.atmosphere
    if (atm) counts[atm] = (counts[atm] || 0) + 1
  })

  return {
    labels: Object.keys(counts),
    datasets: [{
      label: 'Planet Count',
      data: Object.values(counts),
      backgroundColor: '#60a5fa'
    }]
  }
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { display: false },
    title: {
      display: false
    }
  },
  scales: {
    x: {
      ticks: { color: '#cbd5e1' },
      title: { display: true, text: 'Atmosphere Type', color: '#cbd5e1' }
    },
    y: {
      beginAtZero: true,
      ticks: { color: '#cbd5e1' },
      title: { display: true, text: 'Count', color: '#cbd5e1' }
    }
  }
}
</script>
