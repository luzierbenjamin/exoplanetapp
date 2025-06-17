<template>
    <div v-if="planet" class="text-white bg-gray-700 p-4 rounded">
        <strong>{{  planet.name }}</strong> - {{  planet.discoveryMethod  }} ({{ planet.discoveryYear }})
    </div>
</template>

<script setup>
defineProps({
    planet: Object
})
</script>