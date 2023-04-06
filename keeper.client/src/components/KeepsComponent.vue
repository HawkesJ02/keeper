<template>
  <div class="bricky">
    <div>
      <section class="text-holder">
        <img @click="get_keep_by_id(keep.id)" class="img-fluid rounded" :src="keep.img" :alt="keep.name"
          :title="keep.name" data-bs-toggle="modal" data-bs-target="#exampleModal">
        <p class="bottom-left fs-5"><b>{{ keep.name }}</b></p>
        <div class="bottom-right">
          <router-link :to="{ name: 'Profile', params: { creatorId: keep.creatorId } }">
            <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="profile-picture">
          </router-link>
        </div>
      </section>
      <div v-if="keep.vaultKeepId">
        <button @click="remove_keep_from_vault(keep.vaultKeepId)" class="btn btn-danger"
          title="Delete Keep From Vault">GET OUTTA
          HERE KEEP</button>
      </div>
      <div>
        <!-- 
        <div v-if="keep.creatorId == account.id">
          <button @click="delete_keep(keep.id);" class="btn btn-danger">
            <i class="mdi mdi-delete" title="Remove Keep">
            </i>
          </button>
        </div> -->
      </div>
    </div>
  </div>

  <!-- Modal -->
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="container">
          <div class="row">
            <div class="col-6 p-0 d-flex">
              <img class="img-modal rounded img-fluid" :src="activekeep?.img" :alt="activekeep?.name"
                :title="activekeep?.name">
            </div>
            <div class="col-6">
              <section class="d-flex flex-row d-flex justify-content-center">
                <div class="px-2"><i class="mdi mdi-eye px-1"></i>{{ activekeep?.views }}</div>
                <div class="px-2"> <i class="mdi mdi-lock"></i>{{ activekeep?.kept }}</div>
                <!-- <div v-if="activekeep?.creatorId == account.id">
                  <div><i class="mdi mdi-delete bg-danger rounded" title="delete keep"></i></div>

                </div> -->
              </section>
              <section>
                <h1 class="text-center">{{ activekeep?.name }}</h1>
                <p>{{ activekeep?.description }}</p>
              </section>

              <section v-if="activekeep">
                <!-- <router-link :to="{ name: 'Profile', params: { creatorId: activekeep?.creatorId } }">
                  <img :src="activekeep?.creator.picture" :alt="activekeep?.creator.name" class="profile-picture">
                </router-link> -->
              </section>
              <div>
                <div v-if="account?.name != null" class="dropdown">
                  <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                    aria-expanded="false" @click="getMyClubs">
                    Add Keep to Vault
                  </button>
                  <ul class="dropdown-menu">
                    <div v-for="v in myVaults">
                      <AddToVaultButton :vault="v" />
                    </div>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { RouterLink } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import { computed } from "vue";
import Pop from "../utils/Pop";
import { vaultsService } from "../services/VaultsService";


export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      async get_keep_by_id(id) {
        try {
          await keepsService.get_keep_by_id(id)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async delete_keep(id) {
        try {
          await keepsService.delete_keep(id)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },

      async remove_keep_from_vault(VKID) {
        try {
          await vaultsService.remove_keep_from_vault(VKID)
        } catch (error) {
          logger.log(error)
          Pop.error(error)
        }
      },

      activekeep: computed(() => AppState.activekeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults)
    };
  },
  components: { RouterLink }
}
</script>


<style lang="scss" scoped>
.img-modal {
  max-height: 80vh;
  width: auto;
}

.profile-picture {
  height: 4vh;
  width: 4vh;
  border-radius: 50%;
  box-shadow: 2px 2px 4px black;
}

.bricky {
  columns: 300px;
  column-gap: .5em;

  &>div {
    margin-top: .5em;
    display: inline-block;
  }
}

.text-holder {
  position: relative;
  text-align: center;
  color: white;
  text-shadow: 3px 3px 4px black;
}

.bottom-left {
  position: absolute;
  bottom: 0px;
  left: 16px;
}

.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}
</style>