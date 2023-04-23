<template class="">
  <!-- TODO add logo to center and make sticky-top work correctly -->
  <div class="container-fluid my-2">
    <div class="row justify-content-between">
      <div class="d-flex flex-row col-2 align-items-center">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
          <div class="d-flex flex-column align-items-center">
            <button class="btn specialbutton selectable btn-rounded"> <b>Home</b></button>
          </div>
        </router-link>
        <div class="dropdown">
          <button class="btn specialbutton selectable dropdown-toggle mx-3" type="button" id="dropdownMenuButton1"
            data-bs-toggle="dropdown" aria-expanded="false">
            <b>Create</b>
            <i class="mdi mdi-menu-down"></i>
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
            <li><a v-if="account.id" data-bs-toggle="modal" data-bs-target="#createkeepModal"
                class="dropdown-item selectable">Create
                Keep</a></li>
            <li><a v-if="account.id" data-bs-toggle="modal" data-bs-target="#createvaultModal"
                class="dropdown-item selectable" href="#">Create Vault</a></li>

          </ul>
        </div>
      </div>
      <div class="col-1 d-flex justify-content-center">
        <Login />
      </div>
    </div>
  </div>
  <!-- CREATE KEEP MODAL -->
  <div class="modal fade" id="createkeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="exampleModalLabel">Add Your Keep</h2>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
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
            <button class="btn bg-danger mt-4" type="submit" data-bs-dismiss="modal">Create Keep</button>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

  <!-- CREATE VAULT MODAL -->
  <div class="modal fade" id="createvaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h2 class="modal-title" id="exampleModalLabel">Add Your Vault</h2>

          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault()">
            <div class="form-floating">
              <input v-model="editable.name" required type="text" class="form-control" id="name" maxlength="50">
              <label for="floatingInput" class="form-label">Name</label>
            </div>
            <div class="form-floating my-3">
              <input v-model="editable.Description" required type="text" class="form-control" id="bio">
              <label for="floatingInput" class="form-label">Description</label>
            </div>
            <div class="form-floating">
              <input v-model="editable.imgUrl" required type="text" class="form-control" id="coverImg">
              <label for="floatingInput" class="form-label">Url</label>
            </div>
            <div class="form-check">
              <input v-model="editable.IsPrivate" class="form-check-input" type="checkbox" id="isPrivate">
              <label class="form-check-label" for="flexCheckDefault">
                Private?
              </label>
            </div>
            <button class="btn bg-danger mt-4" type="submit" data-bs-dismiss="modal">Create Vault</button>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue"
import { useRoute, useRouter } from 'vue-router';
import { AppState } from "../AppState"
import { computed } from "vue"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import Login from './Login.vue'
export default {
  setup() {
    const editable = ref({})
    const route = useRoute()
    const router = useRouter()
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
      },
      async createVault() {
        try {
          const formData = editable.value;
          if (formData.IsPrivate == null) {
            formData.IsPrivate = false
          }
          logger.log('[FORM DATA]', formData)
          const vault = await vaultsService.createVault(formData)
          const vaultId = vault.id
          editable.value = {}
          router.push(`/vaults/${vaultId}`)
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
  background-color: #fef6f0;
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
