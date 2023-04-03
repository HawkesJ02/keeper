<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <button class="btn specialbutton btn-rounded"> <b>Home</b></button>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link :to="{ name: 'About' }" class="btn text-dark selectable">
            <b>Create</b><i class="mdi mdi-menu-down"></i>
          </router-link> -->
          <button v-if="account.id" data-bs-toggle="modal" data-bs-target="#createkeepModal"
            class="btn text-dark selectable">
            <b>Create</b><i class="mdi mdi-menu-down"></i>
          </button>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>

  <!-- Modal -->
  <div class="modal fade" id="createkeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="exampleModalLabel">Add Your Keep</h2>
          <form @submit.prevent="createKeep()">
            <div class="form-floating">
              <input v-model="editable.name" required type="text" class="form-control" id="name" maxlength="20">
              <label for="floatingInput" class="form-label">Name</label>
            </div>
            <div class="form-floating my-3">
              <input v-model="editable.Description" required type="text" class="form-control" id="bio">
              <label for="floatingInput" class="form-label">Description</label>
            </div>
            <div class="form-floating">
              <input v-model="editable.Img" required type="text" class="form-control" id="coverImg">
              <label for="floatingInput" class="form-label">Url</label>
            </div>
            <button class="btn bg-danger mt-4" type="submit">Create Keep</button>
          </form>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          ...
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue"
import { AppState } from "../AppState"
import { computed } from "vue"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import Login from './Login.vue'
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      account: computed(() => AppState.account),
      async createKeep() {
        try {
          const formData = editable.value;
          const keep = await keepsService.createKeep(formData)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
.specialbutton {
  background-color: #e9d8d6;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
