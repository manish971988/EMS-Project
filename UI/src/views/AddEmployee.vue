<template>
  <div class="add-employee-container">
    <div class="form-card">
      <h2>Add Employee</h2>
      <form @submit.prevent="submitForm">
        <div class="form-row">
          <div class="form-field">
            <label for="name">Name <span class="required">*</span></label>
            <input id="name" v-model="form.name" placeholder="Name" required />
          </div>
          <div class="form-field">
            <label for="department">Department <span class="required">*</span></label>
            <input id="department" v-model="form.department" placeholder="Department" required />
          </div>
        </div>
        <div class="form-row">
          <div class="form-field">
            <label for="designation">Designation <span class="required">*</span></label>
            <input id="designation" v-model="form.designation" placeholder="Designation" required />
          </div>
          <div class="form-field">
            <label for="salary">Salary <span class="required">*</span></label>
            <input id="salary" v-model="form.salary" type="number" placeholder="Salary" required />
          </div>
        </div>
        <div class="form-row">
          <div class="form-field">
            <label for="address">Address <span class="required">*</span></label>
            <input id="address" v-model="form.address" placeholder="Address" required />
          </div>
          <div class="form-field">
            <label for="status">Status <span class="required">*</span></label>
            <select id="status" v-model="form.status" required>
              <option value="" disabled>Status</option>
              <option value="Active">Active</option>
              <option value="Inactive">Inactive</option>
              <option value="Deleted">Deleted</option>
            </select>
          </div>
        </div>
        <div class="actions">
          <button type="submit" :disabled="!isValid">Add Employee</button>
          <button type="button" @click="cancel">Cancel</button>
        </div>
      </form>
      <div v-if="message" class="feedback">{{ message }}</div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const form = ref({
  name: '',
  department: '',
  designation: '',
  salary: '',
  address: '',
  status: ''
})
const message = ref('')
const isValid = computed(() =>
  form.value.name && form.value.department && form.value.designation &&
  form.value.salary && form.value.address && form.value.status
)

function submitForm() {
  if (!isValid.value) return
  // Replace with your API endpoint
  fetch('/api/employees', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      name: form.value.name,
      department: form.value.department,
      designation: form.value.designation,
      salary: form.value.salary,
      address: form.value.address,
      status: form.value.status
    })
  })
    .then(res => {
      if (res.ok) {
        message.value = 'Employee added successfully!'
        setTimeout(() => router.push('/employees'), 1500)
      } else {
        message.value = 'Error adding employee.'
      }
    })
    .catch(() => {
      message.value = 'Network error.'
    })
}

function cancel() {
  router.push('/employees')
}
</script>

<style scoped>
.add-employee-container {
  display: flex; justify-content: center; align-items: center; min-height: 100vh; background: #f4f6fa;
}
.form-card {
  background: #fff; padding: 2rem; border-radius: 12px; box-shadow: 0 4px 24px rgba(0,0,0,0.08);
  width: 400px; max-width: 90vw;
}
h2 { margin-bottom: 1.5rem; color: #2c3e50; text-align: center; }
.form-row {
  display: flex; gap: 1rem; margin-bottom: 1rem;
}
.form-field {
  flex: 1;
  display: flex;
  flex-direction: column;
}
label {
  font-size: 0.95rem;
  color: #2c3e50;
  margin-bottom: 0.25rem;
  font-weight: 500;
  text-align: left;
}
.required {
  color: #e74c3c;
  font-size: 1rem;
  margin-left: 2px;
}
input, select {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #dbe2ef;
  border-radius: 6px;
  font-size: 1rem;
  transition: border-color 0.2s;
  box-sizing: border-box;
}
input:focus, select:focus { border-color: #0077b6; outline: none; }
.actions { display: flex; gap: 1rem; margin-top: 1.5rem; }
button[type='submit'] {
  background: #0077b6; color: #fff; border: none; padding: 0.75rem 1.5rem; border-radius: 6px;
  font-weight: 600; cursor: pointer; transition: background 0.2s;
}
button[type='submit']:disabled { background: #b0c4de; cursor: not-allowed; }
button[type='button'] {
  background: #f4f6fa; color: #2c3e50; border: 1px solid #dbe2ef; padding: 0.75rem 1.5rem; border-radius: 6px;
  cursor: pointer;
}
.feedback { margin-top: 1rem; color: #27ae60; font-weight: 500; text-align: center; }
</style>
