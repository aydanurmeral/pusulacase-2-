
import { ref } from 'vue';

const user = ref(JSON.parse(localStorage.getItem('user') || 'null'));

export function useAuth() {
  function setUser(u) {
    user.value = u;
    localStorage.setItem('user', JSON.stringify(u));
  }
  function logout() {
    user.value = null;
    localStorage.removeItem('user');
  }

  async function login(credentials, apiCall) {
   
    if (apiCall) {
      const res = await apiCall();
      setUser(res.user || res.data.user);
      return res;
    } else {
     
      const mock = { id: 1, username: credentials.username, role: credentials.role || 'Student' };
      setUser(mock);
      return { user: mock };
    }
  }

  return { user, setUser, logout, login };
}
