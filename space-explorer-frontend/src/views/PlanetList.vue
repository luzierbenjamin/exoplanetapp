<template>
  <div class="min-h-screen bg-gray-900 text-white px-6 py-8">
    <h2 class="text-2xl font-bold mb-4">🔭 Browse Planets</h2>

    <!-- 🔍 Search Input -->
    <input
      v-model="searchQuery"
      type="text"
      placeholder="Search planets by name..."
      class="w-full mb-6 p-3 rounded bg-gray-800 text-white border border-gray-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
    />

    <!-- 🔁 Filtered Results -->
    <div v-if="filteredPlanets.length === 0" class="text-slate-400">No matching planets found.</div>
    <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      <div
        v-for="planet in filteredPlanets"
        :key="planet.id"
        class="bg-gray-800 p-4 rounded shadow hover:shadow-lg hover:ring-1 hover:ring-blue-400 cursor-pointer"
        @click="goToPlanet(planet)"
      >
        <div class="font-bold text-lg">{{ planet.name }}</div>
        <div class="text-sm text-gray-300">
          Discovered: {{ planet.discoveryYear }}<br />
          Mass: {{ planet.mass }}, Radius: {{ planet.radius }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { fetchEnrichedPlanets } from '../services/api'

const router = useRouter()
const searchQuery = ref('')
const planets = ref([])

const filteredPlanets = computed(() =>
  planets.value.filter(p =>
    p.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
)

const goToPlanet = (planet) => {
  router.push(`/planet/${planet.id}`)
}

onMounted(async () => {
  planets.value = await fetchEnrichedPlanets()
})
</script>
