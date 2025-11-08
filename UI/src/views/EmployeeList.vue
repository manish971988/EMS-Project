<!-- filepath: c:\EMS\UI\src\views\EmployeeList.vue -->
<template>
  <div class="employee-list-container">
    <div class="header">
      <h2>Employee List</h2>
      <router-link to="/add-employee">
        <button class="add-btn">Add Employee</button>
      </router-link>
    </div>
    <div class="search-filter">
      <input v-model="search" placeholder="Search by name, department, or designation" />
      <select v-model="departmentFilter">
        <option value="">All Departments</option>
        <option v-for="dept in departments" :key="dept.id" :value="dept.name">{{ dept.name }}</option>
      </select>
    </div>
    <table class="employee-table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Department</th>
          <th>Designation</th>
          <th>Salary</th>
          <th>Status</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="emp in filteredEmployees" :key="emp.id">
          <td>{{ emp.name }}</td>
          <td>{{ emp.department?.name || 'N/A' }}</td>
          <td>{{ emp.designation }}</td>
          <td>{{ emp.salary }}</td>
          <td>
         <span :class="emp.status === 'Active' || emp.status === 0 ? 'status-active' : 'status-inactive'">
    {{ emp.status === 'Active' || emp.status === 0 ? 'Active' : emp.status === 'Inactive' || emp.status === 1 ? 'Inactive' : emp.status }}
  </span>
          </td>
          <td>
            <router-link :to="`/employees/${emp.id}/edit`">
              <button class="edit-btn">Edit</button>
            </router-link>
            <button v-if="isAdmin" @click="confirmDelete(emp.id)" class="delete-btn">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="showConfirm" class="modal">
      <div class="modal-content">
        <p>Are you sure you want to delete this employee?</p>
        <button @click="deleteEmployee">Yes</button>
        <button @click="showConfirm = false">Cancel</button>
      </div>
    </div>
    <div v-if="flashMessage" class="flash">{{ flashMessage }}</div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import axios from 'axios';

const employees = ref([]);
const departments = ref([]);
const search = ref('');
const departmentFilter = ref('');
const showConfirm = ref(false);
const deleteId = ref(null);
const flashMessage = ref('');
const isAdmin = ref(true); // Replace with your actual auth logic
const currentUserId = ref(1); // Replace with your actual user id logic

onMounted(async () => {
  try {
    const [empRes, deptRes] = await Promise.all([
      axios.get('/api/employees?status=active'),
      axios.get('/api/departments')
    ]);
    employees.value = empRes.data;
    departments.value = deptRes.data;
  } catch (e) {
    flashMessage.value = 'Failed to load data.';
  }
});

const filteredEmployees = computed(() => {
  let list = employees.value;
  if (search.value) {
    const s = search.value.toLowerCase();
    list = list.filter(emp =>
      emp.name.toLowerCase().includes(s) ||
      emp.department?.name.toLowerCase().includes(s) ||
      emp.designation.toLowerCase().includes(s)
    );
  }
  if (departmentFilter.value) {
    list = list.filter(emp => emp.department?.name === departmentFilter.value);
  }
  return list;
});

function confirmDelete(id) {
  deleteId.value = id;
  showConfirm.value = true;
}

async function deleteEmployee() {
  showConfirm.value = false;
  try {
    await axios.put(`/api/employees/${deleteId.value}/delete`, {
      updated_by: currentUserId.value
    });
    employees.value = employees.value.filter(emp => emp.id !== deleteId.value);
    flashMessage.value = 'Employee deleted successfully.';
  } catch (e) {
    flashMessage.value = e.response?.data?.message || 'Employee not found or already deleted.';
  }
}
</script>

<style scoped>
.employee-list-container {
  max-width: 1100px;
  margin: 2rem auto;
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 4px 24px rgba(0,0,0,0.08);
  padding: 2rem;
}
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}
.add-btn {
  background: #27ae60;
  color: #fff;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: 6px;
  font-weight: 600;
  cursor: pointer;
}
.search-filter {
  display: flex;
  gap: 1rem;
  margin-bottom: 1rem;
}
.search-filter input, .search-filter select {
  padding: 0.5rem;
  border: 1px solid #dbe2ef;
  border-radius: 6px;
  font-size: 1rem;
}
.employee-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}
.employee-table th, .employee-table td {
  border: 1px solid #dbe2ef;
  padding: 0.75rem;
  text-align: left;
}
.employee-table th {
  background: #f4f6fa;
}
.edit-btn {
  background: #0077b6;
  color: #fff;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  cursor: pointer;
  margin-right: 0.5rem;
}
.edit-btn:hover {
  background: #005f8a;
}
.delete-btn {
  background: #e74c3c;
  color: #fff;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  cursor: pointer;
}
.delete-btn:hover {
  background: #c0392b;
}
.status-active {
  color: #27ae60;
  font-weight: 600;
}
.status-inactive {
  color: #e74c3c;
  font-weight: 600;
}
.modal {
  position: fixed; top: 0; left: 0; width: 100vw; height: 100vh;
  background: rgba(0,0,0,0.3); display: flex; align-items: center; justify-content: center;
}
.modal-content {
  background: #fff; padding: 2rem; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.2);
  display: flex; flex-direction: column; gap: 1rem;
}
.flash {
  color: #27ae60; margin-top: 1rem;
}
@media (max-width: 700px) {
  .employee-list-container { padding: 1rem; }
  .employee-table th, .employee-table td { padding: 0.5rem; font-size: 0.95rem; }
  .header { flex-direction: column; gap: 1rem; }
  .search-filter { flex-direction: column; gap: 0.5rem; }
}
</style>