<template>
  <div class="bg-gray-800 p-4 rounded shadow">
    <h3 class="text-white font-semibold mb-2">BioScore by Orbital Zone</h3>
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
  LinearScale,
  CategoryScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, PointElement, LinearScale, CategoryScale)

const props = defineProps({
  enrichedPlanets: {
    type: Array,
    required: true
  }
})

const colorMap = {
  Rocky: '#34d399',
  'Gas Giant': '#60a5fa',
  Ice: '#f87171'
}

function getZone(orbitalPeriod) {
  if (!orbitalPeriod) return "Unknown"
  if (orbitalPeriod < 100) return "Inner"
  if (orbitalPeriod < 500) return "Habitable"
  return "Outer"
}

const chartData = computed(() => {
  const points = props.enrichedPlanets
    .filter(p => p.predictions?.bioScore != null && p.orbitalPeriod != null)
    .map(p => {
      const zone = getZone(p.orbitalPeriod)
      const planetType = p.predictions.planetType
      return {
        x: zone,
        y: p.predictions.bioScore,
        label: p.name,
        backgroundColor: colorMap[planetType] || '#a3a3a3',
        radius: Math.min(Math.max((p.predictions.waterLikelihood ?? 1) * 2, 3), 10)
      }
    })

  return {
    datasets: [{
      label: 'Planets',
      data: points,
      parsing: false,
      pointBackgroundColor: points.map(p => p.backgroundColor),
      pointRadius: points.map(p => p.radius)
    }]
  }
})

const chartOptions = {
  responsive: true,
  plugins: {
    legend: { display: false },
    tooltip: {
      callbacks: {
        label: ctx => {
          const { x, y, label } = ctx.raw
          return `${label} - Zone: ${x}, BioScore: ${y}`
        }
      }
    }
  },
  scales: {
    x: {
      type: 'category',
      title: {
        display: true,
        text: 'Orbital Zone',
        color: '#cbd5e1'
      },
      ticks: { color: '#cbd5e1' }
    },
    y: {
      beginAtZero: true,
      max: 1,
      title: {
        display: true,
        text: 'BioScore',
        color: '#cbd5e1'
      },
      ticks: { color: '#cbd5e1' }
    }
  }
}
</script>
