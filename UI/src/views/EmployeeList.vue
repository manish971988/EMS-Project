<!-- filepath: c:\EMS\UI\src\views\EmployeeList.vue -->
<template>
  <div class="employee-list-container">
    <h2>Employee List</h2>
    <table class="employee-table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Department</th>
          <th>Designation</th>
          <th>Salary</th>
          <th>Status</th>
          <th v-if="isAdmin">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="emp in employees" :key="emp.id">
          <td>{{ emp.name }}</td>
          <td>{{ emp.department }}</td>
          <td>{{ emp.designation }}</td>
          <td>{{ emp.salary }}</td>
          <td>{{ emp.status }}</td>
          <td v-if="isAdmin">
            <router-link :to="`/employees/${emp.id}/edit`">
              <button class="edit-btn">Edit</button>
            </router-link>
            <button @click="confirmDelete(emp.id)" class="delete-btn">Delete</button>
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
import { ref, onMounted } from 'vue';
import axios from 'axios';

const employees = ref([]);
const showConfirm = ref(false);
const deleteId = ref(null);
const flashMessage = ref('');
const isAdmin = ref(true); // Replace with your actual auth logic
const currentUserId = ref(1); // Replace with your actual user id logic

onMounted(async () => {
  const res = await axios.get('/api/employees?status=active');
  employees.value = res.data;
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
.employee-list-container { padding: 2rem; }
.employee-table { width: 100%; border-collapse: collapse; margin-top: 1rem; }
.employee-table th, .employee-table td { border: 1px solid #dbe2ef; padding: 0.75rem; text-align: left; }
.employee-table th { background: #f4f6fa; }
button { background: #0077b6; color: #fff; border: none; padding: 0.5rem 1rem; border-radius: 6px; cursor: pointer; }
button:hover { background: #005f8a; }
.delete-btn {
  background: #e74c3c;
  color: #fff;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  cursor: pointer;
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
</style>