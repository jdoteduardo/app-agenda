<template>
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <h5 class="modal-title" id="modalEditarLabel">Editar Agenda</h5>
            <button type="button" class="close" @click="$emit('close')">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="submitEdicao">
              <div class="form-group">
                <label for="nome">Nome:</label>
                <input type="text" v-model="agendaEdit.nome" required>
              </div>
              <div class="form-group">
                <label for="email">Email:</label>
                <input type="email" v-model="agendaEdit.email">
              </div>
              <div class="form-group">
                <label for="telefone">Telefone:</label>
                <input type="text" v-model="agendaEdit.telefone" required>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="$emit('close')">Fechar</button>
            <button type="button" class="btn btn-primary" @click="submitEdicao">Salvar</button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ModalEditar',
    props: {
      agenda: Object
    },
    data() {
      return {
        agendaEdit: { ...this.agenda }
      };
    },
    methods: {
      submitEdicao() {
        const token = sessionStorage.getItem('token');
        fetch(`https://localhost:7128/api/agendas/`, {
          method: 'PUT',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.agendaEdit)
        })
        .then(response => {
          if (!response.ok) {
            throw new Error('Erro ao editar a agenda');
          }
          this.$emit('close');
        })
        .catch(error => {
          console.error("Erro ao editar a agenda:", error);
        });
      }
    },
    watch: {
      agenda(newAgenda) {
        this.agendaEdit = { ...newAgenda }; // Atualiza a agendaEdit quando a prop agenda mudar
      }
    }
  };
  </script>
  
  <style scoped>
  .modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  .modal-wrapper {
    width: 400px;
  }
  
  .modal-container {
    background: white;
    padding: 20px;
    border-radius: 5px;
  }
  
  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  .modal-body {
    margin-top: 10px;
  }
  
  .modal-footer {
    display: flex;
    justify-content: flex-end;
    margin-top: 10px;
  }
  
  .form-group {
    display: flex;
    flex-direction: column;
    margin-bottom: 10px;
  }
  
  label {
    margin-bottom: 5px;
  }
  
  input {
    padding: 5px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  </style>
  