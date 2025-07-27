import { createRouter, createWebHistory } from 'vue-router';
import Dashboard from '../views/Dashboard.vue';
import Login from '../views/Login.vue';
import PlanetList from '../views/PlanetList.vue'; // 🔧 You forgot this import

const routes = [
    { path: '/', redirect: '/dashboard' },
    { path: '/dashboard', component: Dashboard },
    { path: '/planets', name: 'Planets', component: PlanetList },
    { path: '/login', component: Login },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!localStorage.getItem('token')
    if (to.path !== '/login' && !isAuthenticated) {
        next('/login')
    } else {
        next()
    }
})

export default router;