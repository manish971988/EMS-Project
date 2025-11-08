import { createRouter, createWebHistory } from 'vue-router';
import EmployeeList from './views/EmployeeList.vue';
import EditEmployee from './views/EditEmployee.vue';
import AddEmployee from './views/AddEmployee.vue';
import UserList from './views/UserList.vue';
import RoleList from './views/RoleList.vue';

const routes = [
  { path: '/', redirect: '/employees' }, // Default route
  { path: '/employees', component: EmployeeList },
  { path: '/employees/:id/edit', component: EditEmployee },
  { path: '/add-employee', component: AddEmployee },
  { path: '/users', component: UserList },
  { path: '/roles', component: RoleList }
];
const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;