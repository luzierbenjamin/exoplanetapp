<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">Mass vs Radius</h3>
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
  enrichedPlanets: {
    type: Array,
    required: true
  }
})

const chartData = computed(() => {
  const points = props.enrichedPlanets
    .filter(p => p.mass && p.radius)
    .map(p => ({
      x: p.mass,
      y: p.radius
    }))

  return {
    datasets: [
      {
        label: 'Planets',
        data: points,
        backgroundColor: '#60a5fa'
      }
    ]
  }
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { display: false },
    tooltip: {
      callbacks: {
        label: context => `Mass: ${context.raw.x}, Radius: ${context.raw.y}`
      }
    }
  },
  scales: {
    x: {
      title: {
        display: true,
        text: 'Mass (Earth Masses)'
      },
      ticks: {
        color: '#cbd5e1'
      }
    },
    y: {
      title: {
        display: true,
        text: 'Radius (Earth Radii)'
      },
      ticks: {
        color: '#cbd5e1'
      }
    }
  }
}
</script>
