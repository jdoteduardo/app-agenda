<template>
  <div>
    <nav>
      <ul>
        <li>
          Agenda
        </li>
      </ul>
      <button @click="logout" class="logout-button">Deslogar</button>
    </nav>
    <button type="button" class="btn btn-primary mt-2" @click="adicionarAgenda">Adicionar</button>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Nome</th>
          <th>Email</th>
          <th>Telefone</th>
          <th>Opções</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="agenda in agendas" :key="agenda.id">
          <td>{{ agenda.id }}</td>
          <td>{{ agenda.nome }}</td>
          <td>{{ agenda.email }}</td>
          <td>{{ agenda.telefone }}</td>
          <td>
            <button type="button" class="btn btn-link" @click="editarAgenda(agenda)">Editar</button>
            <button type="button" class="btn btn-link" @click="excluirAgenda(agenda.id)">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>

    <ModalAdicionar v-if="modalAdicionarVisible" @close="fecharModalAdicionar"/>
    <ModalEditar v-if="modalEditarVisible" :agenda="agendaSelecionada" @close="fecharModalEditar" />
    <ModalExcluir v-if="modalExcluirVisible" :agenda-id="agendaExcluirId" @close="modalExcluirVisible = false" @excluida="agendaExcluida" />
  </div>
</template>

<script>
import ModalAdicionar from './ModalAdicionar.vue';
import ModalEditar from './ModalEditar.vue';
import ModalExcluir from './ModalExcluir.vue';

export default {
  name: 'AppListar',
  components: {
    ModalEditar,
    ModalAdicionar,
    ModalExcluir
  },
  data() {
    return {
      caminhoApi: "https://localhost:7128/api/agendas/",
      agendas: [],
      modalEditarVisible: false,
      modalAdicionarVisible: false,
      modalExcluirVisible: false,
      agendaSelecionada: null,
      agendaExcluirId: null
    };
  },
  methods: {
    logout() {
      sessionStorage.clear();
      this.$router.push('/');
    },
    fetchAgendas() {
      const token = sessionStorage.getItem('token');
      fetch(this.caminhoApi, {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      })
      .then(response => response.json())
      .then(data => {
        this.agendas = data;
      })
      .catch(error => {
        console.error("Erro ao buscar as agendas:", error);
      });
    },
    editarAgenda(agenda) {
      this.agendaSelecionada = agenda;
      this.modalEditarVisible = true;
    },
    fecharModalEditar() {
      this.modalEditarVisible = false;
      this.agendaSelecionada = null;
      this.fetchAgendas();
    },
    adicionarAgenda() {
      this.modalAdicionarVisible = true;
    },
    fecharModalAdicionar() {
      this.modalAdicionarVisible = false;
      this.fetchAgendas();
    },
    excluirAgenda(id) {
      this.agendaExcluirId = id;
      this.modalExcluirVisible = true;
    },
    fecharModalExcluir() {
      this.modalExcluirVisible = false;
      this.fetchAgendas();
    },
    agendaExcluida() {
      this.fetchAgendas();
    }
  },
  mounted() {
    this.fetchAgendas();
  }
};
</script>

<style scoped>
nav {
  background-color: #333;
  padding: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  display: flex;
}

li {
  margin-right: 20px;
  color: white;
}

a {
  color: white;
  text-decoration: none;
}

a:hover {
  text-decoration: underline;
}

.logout-button {
  background-color: #ff4d4d;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
}

.logout-button:hover {
  background-color: #ff3333;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
}

th {
  background-color: #f2f2f2;
  text-align: left;
}

tr:nth-child(even) {
  background-color: #f9f9f9;
}

tr:hover {
  background-color: #f1f1f1;
}
</style>
