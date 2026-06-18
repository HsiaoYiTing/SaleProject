import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import RegisterView from '../views/RegisterView.vue'
import MainView from '../views/MainView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
     {
      path: '/main',
      name: 'main', 
      component: MainView,
    },
    {
      path: '/register',
      name: 'register', 
      component: RegisterView,
    },
  ],
})

export default router
