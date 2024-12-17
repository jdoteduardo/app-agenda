<template>
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <h5 class="modal-title" id="modalExcluirLabel">Excluir Agenda</h5>
            <button type="button" class="close" @click="$emit('close')">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <p>Tem certeza que deseja excluir esta agenda?</p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="$emit('close')">Cancelar</button>
            <button type="button" class="btn btn-danger" @click="submitExcluir">Excluir</button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'ModalExcluir',
    props: {
      agendaId: {
        type: Number,
        required: true
      }
    },
    methods: {
      submitExcluir() {
        const token = sessionStorage.getItem('token');
        fetch(`https://localhost:7128/api/agendas/${this.agendaId}`, {
          method: 'DELETE',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        })
        .then(response => {
          console.log(response);
          this.$emit('excluida', this.agendaId);
          this.$emit('close');
        })
        .catch(error => {
          console.error("Erro ao excluir a agenda:", error);
        });
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
  
  .btn-danger {
    background-color: #dc3545;
    color: white;
  }
  </style>
  