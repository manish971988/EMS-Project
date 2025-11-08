<template>
  <div class="role-list-container">
    <div class="header">
      <h2>Role List</h2>
    </div>
    <table class="role-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Role Name</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="role in roles" :key="role.id">
          <td>{{ role.id }}</td>
          <td>{{ role.name }}</td>
        </tr>
      </tbody>
    </table>
    <div v-if="roles.length === 0" class="no-data">No roles found.</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const roles = ref([]);

onMounted(async () => {
  try {
    const res = await axios.get('/api/roles');
    roles.value = res.data;
  } catch (error) {
    console.error('Error fetching roles:', error);
  }
});
</script>

<style scoped>
.role-list-container {
  max-width: 800px;
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
.role-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}
.role-table th, .role-table td {
  border: 1px solid #dbe2ef;
  padding: 0.75rem;
  text-align: left;
}
.role-table th {
  background: #f4f6fa;
  font-weight: 600;
}
.no-data {
  text-align: center;
  padding: 2rem;
  color: #666;
}
@media (max-width: 700px) {
  .role-list-container { padding: 1rem; }
  .role-table th, .role-table td { padding: 0.5rem; font-size: 0.95rem; }
}
</style>
