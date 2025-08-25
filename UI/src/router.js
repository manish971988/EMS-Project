import { createRouter, createWebHistory } from 'vue-router';
import EmployeeList from './views/EmployeeList.vue';
import EditEmployee from './views/EditEmployee.vue';
import AddEmployee from './views/AddEmployee.vue';

const routes = [
  { path: '/', redirect: '/employees' }, // Default route
  { path: '/employees', component: EmployeeList },
  { path: '/employees/:id/edit', component: EditEmployee },
  { path: '/add-employee', component: AddEmployee }
];
const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;