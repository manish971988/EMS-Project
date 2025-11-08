<template>  
  <div class="add-employee-container">
    <div class="form-card">
      <h2><img src="/logo.png" alt="EMS Logo" class="logo" /> Add Employee</h2>
      <div v-if="error" class="error">{{ error }}</div>
      <div v-if="success" class="success">{{ success }}</div>
      <form @submit.prevent="saveEmployee">
        <div class="form-row">
          <label>Name</label>
          <input v-model="employee.name" required />
        </div>
        <div class="form-row">
          <label>Department</label>
          <select v-model="employee.departmentId" required>
            <option disabled value="">Select department</option>
            <option v-for="dept in departments" :key="dept.id" :value="dept.id">{{ dept.name }}</option>
          </select>
        </div>
        <div class="form-row">
          <label>Designation</label>
          <input v-model="employee.designation" required />
        </div>
        <div class="form-row">
          <label>Salary</label>
          <input type="number" v-model="employee.salary" required min="1" />
        </div>
        <div class="form-row">
          <label>Address</label>
          <textarea v-model="employee.address"></textarea>
        </div>
        <div class="form-row">
          <label>Status</label>
          <select v-model="employee.status" required>
            <option value="Active">Active</option>
            <option value="Inactive">Inactive</option>
          </select>
        </div>
        <div class="actions">
          <button type="submit">Save</button>
          <button type="button" @click="cancel">Cancel</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

const router = useRouter();
const departments = ref([]);
const employee = ref({
  name: '',
  departmentId: '',
  designation: '',
  salary: '',
  address: '',
  status: 'Active'
});
const error = ref('');
const success = ref('');

onMounted(async () => {
  try {
    const res = await axios.get('/api/departments');
    departments.value = res.data;
  } catch (e) {
    error.value = 'Failed to load departments.';
  }
});

async function saveEmployee() {
  error.value = '';
  success.value = '';
  if (!employee.value.name || !employee.value.departmentId || !employee.value.designation || !employee.value.salary || !employee.value.status) {
    error.value = 'Please fill all required fields.';
    return;
  }
  if (employee.value.salary <= 0) {
    error.value = 'Salary must be greater than zero.';
    return;
  }
  try {
    await axios.post('/api/employees', employee.value);
    success.value = 'Employee added successfully.';
    setTimeout(() => {
      router.push('/employees');
    }, 1000); // Redirect after 1 second
  } catch (e) {
    error.value = e.response?.data?.message || 'Save failed.';
  }
}

function cancel() {
  router.push('/employees');
}
</script>

<style scoped>
.logo { height: 2em; vertical-align: middle; margin-right: 0.5em; }
.add-employee-container { display: flex; justify-content: center; align-items: center; min-height: 100vh; background: #f4f6fa; }
.form-card { background: #fff; padding: 2rem; border-radius: 12px; box-shadow: 0 4px 24px rgba(0,0,0,0.08); width: 440px; max-width: 95vw; }
h2 { margin-bottom: 1.5rem; color: #2c3e50; text-align: center; font-weight: 700; }
.form-row { display: flex; flex-direction: column; gap: 0.5rem; margin-bottom: 1rem; }
label { font-weight: 500; margin-bottom: 0.25rem; text-align: left; }
input, select, textarea { padding: 0.75rem; border: 1px solid #dbe2ef; border-radius: 6px; font-size: 1rem; }
.actions { display: flex; gap: 1rem; margin-top: 1.5rem; }
button[type='submit'] { background: #0077b6; color: #fff; border: none; padding: 0.75rem 1.5rem; border-radius: 6px; font-weight: 600; cursor: pointer; }
button[type='button'] { background: #f4f6fa; color: #2c3e50; border: 1px solid #dbe2ef; padding: 0.75rem 1.5rem; border-radius: 6px; cursor: pointer; }
.error { color: #e74c3c; margin-top: 1rem; text-align: left; }
.success { color: #27ae60; margin-top: 1rem; text-align: left; }
</style>
