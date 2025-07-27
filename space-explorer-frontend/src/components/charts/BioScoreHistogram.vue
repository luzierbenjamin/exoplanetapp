<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">BioScore Distribution</h3>
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

// 🎯 Group BioScores into bins
const chartData = computed(() => {
  const bins = Array(11).fill(0) // Bins: 0–1, 1–2, ..., 9–10

  props.enrichedPlanets.forEach(p => {
    const score = p.predictions?.bioScore
    if (typeof score === 'number') {
      const index = Math.min(Math.floor(score), 10)
      bins[index]++
    }
  })

  return {
    labels: bins.map((_, i) => `${i}–${i + 1}`),
    datasets: [{
      label: 'Planet Count',
      data: bins,
      backgroundColor: '#fbbf24'
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
      ticks: { color: '#e5e7eb' },
      title: { display: true, text: 'BioScore Range', color: '#e5e7eb' }
    },
    y: {
      beginAtZero: true,
      ticks: { color: '#e5e7eb' },
      title: { display: true, text: 'Planet Count', color: '#e5e7eb' }
    }
  }
}
</script>
