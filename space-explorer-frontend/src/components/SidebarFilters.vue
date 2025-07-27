<template>
  <div class="bg-gray-800 p-4 rounded text-white w-full sm:w-64 space-y-4">
    <h3 class="text-lg font-bold">🔍 Filters</h3>

    <!-- 🔎 Search -->
    <div>
      <input
        v-model="searchTerm"
        type="text"
        placeholder="Search by name..."
        class="w-full bg-gray-700 p-2 rounded placeholder-gray-400 text-white"
      />
    </div>

    <!-- 🌍 Planet Type -->
    <div>
      <label class="block font-semibold mb-1">Planet Type</label>
      <div v-for="type in planetTypes" :key="type" class="flex items-center space-x-2">
        <input type="checkbox" :value="type" v-model="selectedPlanetTypes" />
        <span>{{ type }}</span>
      </div>
    </div>

    <!-- ☁️ Atmosphere -->
    <div>
      <label class="block font-semibold mb-1">Atmosphere</label>
      <div v-for="type in atmosphereTypes" :key="type" class="flex items-center space-x-2">
        <input type="checkbox" :value="type" v-model="selectedAtmospheres" />
        <span>{{ type }}</span>
      </div>
    </div>

    <!-- 📏 Numeric Ranges -->
    <div v-for="range in numericRanges" :key="range.key">
      <label class="block font-semibold mb-1">{{ range.label }}</label>
      <div class="flex space-x-2 text-sm">
        <input type="number" v-model.number="range.model.min" class="bg-gray-700 p-1 rounded w-20" :placeholder="range.minLabel" />
        <span>to</span>
        <input type="number" v-model.number="range.model.max" class="bg-gray-700 p-1 rounded w-20" :placeholder="range.maxLabel" />
      </div>
    </div>

    <!-- ✅ Apply -->
    <button @click="emitFilters" class="mt-2 bg-blue-600 hover:bg-blue-700 text-white py-1 px-3 rounded w-full">
      Apply Filters
    </button>
  </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue'

const emit = defineEmits(['update:filters'])

const searchTerm = ref('')
const selectedPlanetTypes = ref([])
const selectedAtmospheres = ref([])

const rangeModels = {
  mass: ref({ min: null, max: null }),
  orbitalPeriod: ref({ min: null, max: null }),
  waterLikelihood: ref({ min: null, max: null }),
  bioScore: ref({ min: null, max: null }),
}

const planetTypes = ['Rocky', 'Gas Giant', 'Ice', 'Super Earth']
const atmosphereTypes = ['Nitrogen-Oxygen', 'Hydrogen-Helium', 'Carbon Dioxide', 'No Atmosphere']

const numericRanges = computed(() => [
  { key: 'mass', label: '🪨 Mass', model: rangeModels.mass.value, minLabel: 'Min', maxLabel: 'Max' },
  { key: 'orbitalPeriod', label: '🌀 Orbital Period', model: rangeModels.orbitalPeriod.value, minLabel: 'Min', maxLabel: 'Max' },
  { key: 'waterLikelihood', label: '💧 Water Likelihood', model: rangeModels.waterLikelihood.value, minLabel: '0.0', maxLabel: '1.0' },
  { key: 'bioScore', label: '🧬 BioScore', model: rangeModels.bioScore.value, minLabel: '0.0', maxLabel: '1.0' },
])

const emitFilters = () => {
  emit('update:filters', {
    searchTerm: searchTerm.value.trim().toLowerCase(),
    planetTypes: selectedPlanetTypes.value,
    atmospheres: selectedAtmospheres.value,
    ranges: {
      mass: rangeModels.mass.value,
      orbitalPeriod: rangeModels.orbitalPeriod.value,
      waterLikelihood: rangeModels.waterLikelihood.value,
      bioScore: rangeModels.bioScore.value,
    }
  })
}
</script>
