import { ref, onMounted } from 'vue'
import { fetchPlanets } from '../services/api'

export function usePlanets() {
    const planets = ref([])

    const loadPlanets = async () => {
        planets.value = await fetchPlanets()
    }

    onMounted(loadPlanets)

    return { planets }
}