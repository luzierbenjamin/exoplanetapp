<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">Planet Type Distribution</h3>
    <Doughnut v-if="chartData" :data="chartData" :options="chartOptions" />
    <p v-else class="text-gray-400">No data to display</p>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { Doughnut } from 'vue-chartjs'
import { Chart as ChartJS, Title, Tooltip, Legend, ArcElement } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement)

const props = defineProps({
  enrichedPlanets: {
    type: Array,
    required: true
  }
})

// ✅ Reactive computed counts
const chartData = computed(() => {
  const counts = {
    Rocky: 0,
    "Gas Giant": 0,
    Ice: 0
  }

  props.enrichedPlanets.forEach(p => {
    const type = p.predictions?.planetType
    if (counts[type] !== undefined) counts[type]++
  })

  const values = Object.values(counts)
  const hasData = values.some(count => count > 0)

  return hasData ? {
    labels: Object.keys(counts),
    datasets: [{
      data: values,
      backgroundColor: ['#34d399', '#60a5fa', '#f87171']
    }]
  } : null
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { position: 'bottom' }
  }
}
</script>
