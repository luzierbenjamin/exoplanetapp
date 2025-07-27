<template>
  <div class="relative h-48 flex justify-center items-center bg-black rounded-lg overflow-hidden border border-gray-600 shadow">
    <!-- Orbit Animation -->
    <div v-if="method === 'Orbital'" class="w-40 h-40 border border-blue-500 rounded-full animate-spin-slow relative">
      <div class="absolute w-4 h-4 bg-green-400 rounded-full -top-2 left-1/2 transform -translate-x-1/2"></div>
    </div>

    <!-- Transit Animation -->
    <div v-else class="relative w-full h-24 bg-gradient-to-r from-black via-yellow-400 to-black overflow-hidden">
      <div class="absolute left-0 top-0 w-6 h-6 bg-black rounded-full animate-transit"></div>
    </div>

    <!-- Star (color-coded) -->
    <div
      class="absolute w-16 h-16 rounded-full shadow-lg"
      :style="{ backgroundColor: starColor, left: 'calc(50% - 2rem)' }"
    ></div>

    <!-- Info button -->
    <button
      @click="showInfo = !showInfo"
      class="absolute top-2 right-2 text-white bg-gray-700 px-2 py-1 text-xs rounded"
    >
      ℹ️ Info
    </button>

    <div v-if="showInfo" class="absolute bottom-2 right-2 text-sm bg-gray-800 p-2 rounded border border-blue-300 max-w-xs">
      <p class="font-semibold mb-1">Star Type: {{ starType }}</p>
      <p>{{ starFacts[starType] || 'Unknown star classification.' }}</p>
    </div>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue'

const props = defineProps({
  method: String,
  starType: String
})

const showInfo = ref(false)

const starFacts = {
  'G': 'Yellow dwarf stars like our Sun. Good for life.',
  'M': 'Red dwarfs, small and cool, but common.',
  'K': 'Orange dwarfs, potentially habitable.',
  'F': 'Hotter than Sun, shorter lifespans.',
  'A': 'Very hot white-blue stars. Unlikely for life.'
}

const starColor = computed(() => {
  switch (props.starType?.toUpperCase()) {
    case 'M': return '#ff4d4d'
    case 'K': return '#ffae42'
    case 'G': return '#ffd700'
    case 'F': return '#c2e0ff'
    case 'A': return '#a8d5ff'
    default: return '#ffffff'
  }
})
</script>

<style scoped>
@keyframes spin-slow {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
.animate-spin-slow {
  animation: spin-slow 8s linear infinite;
}

@keyframes transit {
  0% { left: 0%; }
  100% { left: 100%; }
}
.animate-transit {
  animation: transit 4s linear infinite;
}
</style>
