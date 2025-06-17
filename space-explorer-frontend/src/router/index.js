import { createRouter, createWebHistory } from 'vuew-router';
import Dashboard from '../views/Dashboard.vue';
import Login from '../views/Login.vue';

const routes = [
    { path: '/', redirect: '/dashboard' },
    { path: '/dashboard', component: Dashboard },
    { path: '/login', component: Login },
];

export const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!localStorage.getITem('token')
    if(to.path !== '/login' && !isAuthenticated) {
        next('/login')
    } else {
        next()
    }
})