<script setup>
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <nav>
    <router-link to="/employees">Employees</router-link>
    <router-link to="/users">Users</router-link>
    <router-link to="/roles">Roles</router-link>
  </nav>
  <router-view />
</template>

<style scoped>
nav {
  background: #2c3e50;
  padding: 1rem 2rem;
  display: flex;
  gap: 2rem;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
nav a {
  color: #fff;
  text-decoration: none;
  font-weight: 500;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  transition: background 0.3s;
}
nav a:hover {
  background: rgba(255,255,255,0.1);
}
nav a.router-link-active {
  background: #0077b6;
}
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
