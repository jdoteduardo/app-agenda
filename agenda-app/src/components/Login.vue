<template>
    <!-- Pills navs -->
    <ul class="nav nav-pills nav-justified mb-3" id="ex1" role="tablist">
      <li class="nav-item" role="presentation">
        <a 
          class="nav-link" 
          :class="{ active: isLoginTab }" 
          id="tab-login" 
          data-mdb-pill-init 
          role="tab"
          aria-selected="true"
          @click="setActiveTab('login')"
        >
          Login
        </a>
      </li>
      <li class="nav-item" role="presentation">
        <a 
          class="nav-link" 
          :class="{ active: !isLoginTab }" 
          id="tab-register" 
          data-mdb-pill-init 
          role="tab"
          aria-selected="false"
          @click="setActiveTab('register')"
        >
          Registrar
        </a>
      </li>
    </ul>
    <!-- Pills navs -->
  
    <!-- Pills content -->
    <div class="tab-content">
      <div class="tab-pane fade show active" id="pills-login" role="tabpanel" aria-labelledby="tab-login">
        <form @submit.prevent="handlerLogin">
          <!-- Login input -->
          <div data-mdb-input-init class="form-outline mb-4">
            <input type="text" id="loginName" class="form-control" v-model="loginForm.login" />
            <label class="form-label" for="loginName">Login</label>
          </div>
  
          <!-- Password input -->
          <div data-mdb-input-init class="form-outline mb-4">
            <input type="password" id="loginPassword" class="form-control" v-model="loginForm.senha" />
            <label class="form-label" for="loginPassword">Senha</label>
          </div>
  
          <!-- Login Button (only visible when Login tab is active) -->
          <button 
            v-if="isLoginTab" 
            type="submit" 
            data-mdb-button-init 
            data-mdb-ripple-init 
            class="btn btn-primary btn-block mb-4"
          >
            Login
          </button>
  
          <!-- Register Button (only visible when Register tab is active) -->
          <button 
            v-if="!isLoginTab" 
            type="submit" 
            data-mdb-button-init 
            data-mdb-ripple-init 
            class="btn btn-primary btn-block mb-4"
          >
            Registrar
          </button>
        </form>
      </div>
    </div>

    <div v-if="exibeErroLogin" class="alert alert-danger" role="alert">
        Login ou senha errados.
    </div>
    <!-- Pills content -->
  </template>
  
  <script>
    import axios from 'axios';
    import { useRouter } from 'vue-router';

    export default {
    name: 'LoginForm',
    data() {
        return {
          caminhoApi: "https://localhost:7128/api/contas/",
          isLoginTab: true,
          loginForm: {
              login: "",
              email: "",
              senha: ""
          },
          exibeErroLogin: false
        };
    },
    setup() {
        const router = useRouter();
        return { router };
    },
    methods: {
        setActiveTab(tab) {
        if (tab === 'login') {
            this.isLoginTab = true;
        } else {
            this.isLoginTab = false;
        }
        },
        async handlerLogin() {
        try {
            const requestBody = {
            login: this.loginForm.login,
            senha: this.loginForm.senha
            };

            const response = await axios.post(`${this.caminhoApi}Login`, requestBody);

            sessionStorage.setItem("token", response.data.token);
            sessionStorage.setItem("refreshToken", response.data.refreshToken);
            this.exibeErroLogin = false;

            this.router.push('/listar');
        } catch (error) {
            this.exibeErroLogin = true;
            console.error("Erro ao fazer login:", error);
        }
        }
    }
    };
    </script>

  
  <style scoped>
  /* Seu estilo aqui */
  </style>  