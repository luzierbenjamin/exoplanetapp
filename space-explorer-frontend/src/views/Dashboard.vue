<template>
    <div class="p-6">
        <h2 class="text-xl font-bold text-white mb-4">Planet List</h2>
        <PlanetCard
            v-for="planet in planets"
            :key="planet"
        />
    </div>
</template>

<script setup>
import { usePlanets } from '../composables/usePlanets'
const { planets } = usePlanets()
</script>