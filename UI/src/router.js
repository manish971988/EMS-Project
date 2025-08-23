import { createRouter, createWebHistory } from 'vue-router'
import AddEmployee from './views/AddEmployee.vue'

const routes = [
  { path: '/add-employee', component: AddEmployee },
  // Add other routes here
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
