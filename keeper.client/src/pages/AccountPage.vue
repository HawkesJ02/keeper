<template>
  <div v-if="account" class="container card">
    <form @submit.prevent="edit_account()">
      <div class="mb-3">
        <label for="name" class="form-label">Name</label>
        <input required v-model="editable.name" type="text" class="form-control" id="name" name="name">
      </div>
      <div class="mb-3">
        <label for="picture" class="form-label">Picture</label>
        <input v-model="editable.picture" type="url" class="form-control" id="picture">
      </div>
      <div class="mb-3">
        <label for="coverImg" class="form-label">CoverImg</label>
        <input v-model="editable.coverImg" type="url" class="form-control" id="coverImg">
      </div>
      <button class="btn btn-secondary" type="submit">Save</button>
    </form>



    <div class="row">
      <div class="d-flex card my-3 text-center">
        <img v-if="account.coverImg" :src="account.coverImg" alt="" class="cover-image">
        <img v-else
          src="https://images.unsplash.com/photo-1520034475321-cbe63696469a?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80"
          alt="" class="cover-image">
        <img :src="account.picture" alt="" class="img-fluid profile-picture center">
        <h1>{{ account.name }}</h1>
      </div>
      <section>
        <div>
          <h1>
            Vaults
            <div v-for="v in vaults">
              <VaultsComponent :vault="v" />
            </div>
          </h1>
        </div>
        <div>
          <h1>
            Keeps
          </h1>
          <div v-for="k in keeps">
            <KeepsComponent :keep="k" />
          </div>
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { profilesService } from '../services/ProfilesService.js'
import Pop from "../utils/Pop";
import { watchEffect } from "vue";
import { computed } from "vue";
import { ref } from "vue";
import { onUnmounted } from "vue";
import { vaultsService } from "../services/VaultsService";
import { accountService } from "../services/AccountService";

export default {
  setup() {
    const editable = ref({})
    const route = useRoute();
    async function get_profile_by_id() {
      try {
        const account = AppState.account.id
        const profile_id = account
        await profilesService.get_profile_by_id(profile_id)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function get_keeps_by_profile() {
      try {
        const account = AppState.account.id
        const profile_id = account
        await profilesService.get_keeps_by_profile(profile_id);
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function get_vaults_by_profile() {
      try {
        const account = AppState.account.id
        const profile_id = account
        await profilesService.get_vaults_by_profile(profile_id);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    watchEffect(() => {
      get_profile_by_id();
      get_keeps_by_profile();
      get_vaults_by_profile();
      if (AppState.account.id) {
        editable.value = { ...AppState.account }
      }
    })

    onUnmounted(() => {
      AppState.profile = {},
        AppState.keeps = {},
        AppState.vaults = {}
    })
    return {
      editable,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      async edit_account() {
        try {
          const form_data = editable.value
          const id = AppState.account.id
          await accountService.edit_account(id, form_data)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>

<style scoped>
.cover-image {
  height: 50vh;
  width: 100%;
  object-fit: cover;
  background-attachment: fixed;
  transform: translateY(7vh);
}

.profile-picture {
  height: 15vh;
  width: 15vh;
  object-fit: cover;
  z-index: 1;
  border-radius: 50%;
}

.center {
  display: block;
  margin-left: auto;
  margin-right: auto;
}
</style>
