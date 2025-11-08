<template>
  <div class="user-list-container">
    <div class="header">
      <h2>User List</h2>
    </div>
    <table class="user-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Email</th>
          <th>Role</th>
          <th>Created At</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.id }}</td>
          <td>{{ user.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.role?.name || 'N/A' }}</td>
          <td>{{ formatDate(user.createdAt) }}</td>
        </tr>
      </tbody>
    </table>
    <div v-if="users.length === 0" class="no-data">No users found.</div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const users = ref([]);

onMounted(async () => {
  try {
    const res = await axios.get('/api/users');
    users.value = res.data;
  } catch (error) {
    console.error('Error fetching users:', error);
  }
});

function formatDate(dateString) {
  if (!dateString) return 'N/A';
  const date = new Date(dateString);
  return date.toLocaleDateString();
}
</script>

<style scoped>
.user-list-container {
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
.user-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}
.user-table th, .user-table td {
  border: 1px solid #dbe2ef;
  padding: 0.75rem;
  text-align: left;
}
.user-table th {
  background: #f4f6fa;
  font-weight: 600;
}
.no-data {
  text-align: center;
  padding: 2rem;
  color: #666;
}
@media (max-width: 700px) {
  .user-list-container { padding: 1rem; }
  .user-table th, .user-table td { padding: 0.5rem; font-size: 0.95rem; }
}
</style>
