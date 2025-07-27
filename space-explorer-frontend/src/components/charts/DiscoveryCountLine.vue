<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">Planet Discoveries Over the Years</h3>
    <Line :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { Line } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, LineElement, CategoryScale, LinearScale, PointElement)

const props = defineProps({
  enrichedPlanets: Array
})

const chartData = computed(() => {
  const yearCounts = {}

  props.enrichedPlanets.forEach(p => {
    const year = p.discoveryYear
    if (year) {
      yearCounts[year] = (yearCounts[year] || 0) + 1
    }
  })

  const sortedYears = Object.keys(yearCounts).sort((a, b) => a - b)

  return {
    labels: sortedYears,
    datasets: [{
      label: 'Planets Discovered',
      data: sortedYears.map(year => yearCounts[year]),
      borderColor: '#34d399',
      backgroundColor: '#34d399',
      tension: 0.3
    }]
  }
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { position: 'top' }
  },
  scales: {
    x: {
      title: { display: true, text: 'Discovery Year', color: '#e5e7eb' },
      ticks: { color: '#e5e7eb' }
    },
    y: {
      title: { display: true, text: 'Count', color: '#e5e7eb' },
      ticks: { color: '#e5e7eb' }
    }
  }
}
</script>
