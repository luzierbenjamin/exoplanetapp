<template>
  <div class="min-h-screen bg-gray-900 text-white px-6 py-10">
    <h2 class="text-3xl font-bold mb-6 text-blue-300">🔮 Custom Planet Prediction</h2>

    <form @submit.prevent="predict" class="space-y-4 max-w-md">
      <div>
        <label class="block font-semibold mb-1">Mass (Earth Masses)</label>
        <input v-model.number="input.mass" type="number" min="0" step="any" class="w-full rounded p-2 bg-gray-800 text-white border border-gray-600" required />
      </div>
      <div>
        <label class="block font-semibold mb-1">Radius (Earth Radii)</label>
        <input v-model.number="input.radius" type="number" min="0" step="any" class="w-full rounded p-2 bg-gray-800 text-white border border-gray-600" required />
      </div>
      <div>
        <label class="block font-semibold mb-1">Orbital Period (days)</label>
        <input v-model.number="input.orbitalPeriod" type="number" min="0" step="any" class="w-full rounded p-2 bg-gray-800 text-white border border-gray-600" required />
      </div>

      <button type="submit" class="mt-4 px-4 py-2 bg-blue-600 hover:bg-blue-700 rounded text-white">
        Predict
      </button>
    </form>

    <div v-if="result" class="mt-8 bg-gray-800 p-6 rounded shadow border border-blue-400">
      <h3 class="text-xl font-bold mb-2 text-blue-200">🧬 Prediction Result</h3>
      <p><strong>Planet Type:</strong> {{ result.planetType }}</p>
      <p><strong>Atmosphere:</strong> {{ result.atmosphere }}</p>
      <p><strong>Water Likelihood:</strong> {{ result.waterLikelihood.toFixed(2) }}</p>
      <p><strong>BioScore:</strong> {{ result.bioScore.toFixed(2) }}</p>

      <div class="mt-4 text-sm italic text-blue-300">
        {{ interpretResult() }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import api from '../services/api'

const input = ref({
  mass: null,
  radius: null,
  orbitalPeriod: null
})

const result = ref(null)

const predict = async () => {
  try {
    const res = await api.post('/prediction/custom', input.value)
    result.value = res.data
  } catch (error) {
    console.error('Prediction failed', error)
  }
}

const interpretResult = () => {
  if (!result.value) return ''

  const { planetType, atmosphere, waterLikelihood, bioScore } = result.value

  let text = `This is likely a ${planetType} planet with a ${atmosphere} atmosphere. `
  text += `It has a ${waterLikelihood > 0.5 ? 'high' : 'low'} likelihood of having water, `
  text += `and a ${bioScore > 0.5 ? 'strong' : 'weak'} potential for supporting life.`

  return text
}
</script>
