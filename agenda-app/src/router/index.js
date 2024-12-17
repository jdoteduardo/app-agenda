import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/Login.vue'
import Listar from '@/components/Listar.vue'

function isAuthenticated() {
    return !!sessionStorage.getItem('token');
}

const routes = [
    {
        path: '/',
        name: 'Login',
        component: Login
    },
    {
        path: '/listar',
        name: 'Listar',
        component: Listar,
        beforeEnter: (to, from, next) => {
            if (isAuthenticated()) {
                next();
            } else {
                next('/');
            }
        }
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router
