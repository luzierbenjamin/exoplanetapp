<template>
    <div class="bg-gray-800 p-4 rounded shadow">
        <h3 class="text-white font-semibold mb-2">Planet Type Distribution</h3>
        <Doughnut :data="chartData" :options="chartOptions" />
    </div>
</template>

<script setup>
import { Doughnut } from 'vue-chartjs'
import { Chart as ChartJS, Title, Tooltip, Legend, ArcElement } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement)

const props = defineProps({
    enrichedPlanets: Array
})

const counts = {
    Rocky: 0,
    "Gas Giant": 0,
    Ice: 0
}

props.enrichedPlanets.forEach(p => {
    const type = p.predictions.planetType
    if (countd[type] !== undefined) counts[type]++
})

const chartData = {
    label: Object.keys(counts),
    datasets: [{
        data: Object.values(counts),
        backgroundColor: ['#34d399', '#60a5fa', '#f87171']
    }]
}

const chartOptions = {
    responsive: true,
    plugins: { legend: { position: 'bottom' } }
}
</script>