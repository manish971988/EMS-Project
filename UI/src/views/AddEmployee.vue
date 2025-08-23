<template>
  <div class="add-employee-container">
    <div class="form-card">
      <h2><img src="/logo.png" alt="EMS Logo" class="logo" /> Add Employee</h2>
      <form @submit.prevent="submitForm" aria-label="Add Employee Form">
        <div class="form-row">
          <div class="form-field">
            <label for="name" class="field-label">Name <span class="required">*</span></label>
            <input id="name" v-model="form.name" placeholder="Name" required />
            <div v-if="errors.name" class="error">{{ errors.name }}</div>
          </div>
          <div class="form-field">
            <label for="department" class="field-label">Department <span class="required">*</span></label>
            <select id="department" v-model="form.department" required>
              <option value="" disabled>Select Department</option>
              <option v-for="dept in departments" :key="dept" :value="dept">{{ dept }}</option>
            </select>
            <div v-if="errors.department" class="error">{{ errors.department }}</div>
          </div>
        </div>
        <div class="form-row">
          <div class="form-field">
            <label for="designation" class="field-label">Designation <span class="required">*</span></label>
            <input id="designation" v-model="form.designation" placeholder="Designation" required />
            <div v-if="errors.designation" class="error">{{ errors.designation }}</div>
          </div>
          <div class="form-field">
            <label for="salary" class="field-label">Salary <span class="required">*</span></label>
            <input id="salary" v-model="form.salary" type="number" placeholder="Salary" required />
            <div v-if="errors.salary" class="error">{{ errors.salary }}</div>
          </div>
        </div>
        <div class="form-row">
          <div class="form-field">
            <label for="address" class="field-label">Address <span class="required">*</span></label>
            <input id="address" v-model="form.address" placeholder="Address" required />
            <div v-if="errors.address" class="error">{{ errors.address }}</div>
          </div>
          <div class="form-field">
            <label for="status" class="field-label">Status <span class="required">*</span></label>
            <select id="status" v-model="form.status" required>
              <option value="" disabled>Status</option>
              <option v-for="stat in statuses" :key="stat" :value="stat">{{ stat }}</option>
            </select>
            <div v-if="errors.status" class="error">{{ errors.status }}</div>
          </div>
        </div>
        <div class="actions">
          <button type="submit" :disabled="loading || !isValid">{{ loading ? 'Adding...' : 'Add Employee' }}</button>
          <button type="button" @click="cancel">Cancel</button>
        </div>
      </form>
      <div v-if="toast" class="toast" role="alert">{{ toast }}</div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
const departments = ['HR','Finance','Engineering','Sales','Marketing','Support','Operations'];
const statuses = ['Active','Inactive','Deleted'];
const form = ref({
  name: '',
  department: '',
  designation: '',
  salary: '',
  address: '',
  status: ''
});
const errors = ref({});
const loading = ref(false);
const toast = ref('');
const isValid = computed(() => {
  errors.value = {};
  if (!form.value.name) errors.value.name = 'Name is required.';
  if (!form.value.department) errors.value.department = 'Department is required.';
  if (!form.value.designation) errors.value.designation = 'Designation is required.';
  if (!form.value.salary) errors.value.salary = 'Salary is required.';
  if (!form.value.address) errors.value.address = 'Address is required.';
  if (!form.value.status) errors.value.status = 'Status is required.';
  return Object.keys(errors.value).length === 0;
});
function submitForm() {
  if (!isValid.value) return;
  loading.value = true;
  fetch('/api/employees', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(form.value)
  })
    .then(res => res.ok ? res.json() : Promise.reject(res))
    .then(() => {
      toast.value = 'Employee added successfully!';
      setTimeout(() => toast.value = '', 3000);
      Object.keys(form.value).forEach(k => form.value[k] = '');
    })
    .catch(() => {
      toast.value = 'Failed to add employee.';
      setTimeout(() => toast.value = '', 3000);
    })
    .finally(() => loading.value = false);
}
function cancel() {
  Object.keys(form.value).forEach(k => form.value[k] = '');
}
</script>

<style scoped>
.logo { height: 2em; vertical-align: middle; margin-right: 0.5em; }
.add-employee-container { display: flex; justify-content: center; align-items: center; min-height: 100vh; background: #f4f6fa; }
.form-card { background: #fff; padding: 2rem; border-radius: 12px; box-shadow: 0 4px 24px rgba(0,0,0,0.08); width: 440px; max-width: 95vw; }
h2 { margin-bottom: 1.5rem; color: #2c3e50; text-align: center; font-weight: 700; }
.form-row { display: flex; gap: 1rem; margin-bottom: 1rem; }
.form-field { flex: 1; display: flex; flex-direction: column; }
.field-label { font-size: 0.95rem; color: #2c3e50; margin-bottom: 0.25rem; font-weight: 500; text-align: left; }
.required { color: #e74c3c; font-size: 1rem; margin-left: 2px; }
input, select { width: 100%; padding: 0.75rem; border: 1px solid #dbe2ef; border-radius: 6px; font-size: 1rem; transition: border-color 0.2s; box-sizing: border-box; }
input:focus, select:focus { border-color: #0077b6; outline: none; }
.error { color: #e74c3c; font-size: 0.85rem; margin-top: 2px; text-align: left; }
.actions { display: flex; gap: 1rem; margin-top: 1.5rem; }
button[type='submit'] { background: #0077b6; color: #fff; border: none; padding: 0.75rem 1.5rem; border-radius: 6px; font-weight: 600; cursor: pointer; transition: background 0.2s; }
button[type='submit']:disabled { background: #b0c4de; cursor: not-allowed; }
button[type='button'] { background: #f4f6fa; color: #2c3e50; border: 1px solid #dbe2ef; padding: 0.75rem 1.5rem; border-radius: 6px; cursor: pointer; }
.toast { margin-top: 1rem; color: #27ae60; font-weight: 500; text-align: center; background: #eafaf1; border-radius: 6px; padding: 0.5rem; }
</style>
