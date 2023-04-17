<template>
  <div v-if="profile" class="container card">
    <!-- TODO fix profile page -->
    <div class="row">
      <div class="d-flex card my-3 text-center">
        <img v-if="profile.coverImg" :src="profile.coverImg" alt="" class="cover-image">
        <img v-else
          src="https://images.unsplash.com/photo-1520034475321-cbe63696469a?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80"
          alt="" class="cover-image">
        <img :src="profile.picture" alt="" class="img-fluid profile-picture center">
        <h1>{{ profile.name }}</h1>
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
import { onUnmounted } from "vue";
import { vaultsService } from "../services/VaultsService";

export default {
  setup() {
    const route = useRoute();
    async function get_profile_by_id() {
      try {
        const profile_id = route.params.creatorId;
        await profilesService.get_profile_by_id(profile_id)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function get_keeps_by_profile() {
      try {
        const profile_id = route.params.creatorId
        await profilesService.get_keeps_by_profile(profile_id);
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }

    async function get_vaults_by_profile() {
      try {
        const profile_id = route.params.creatorId
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
    })

    onUnmounted(() => {
      AppState.profile = {},
        AppState.keeps = {},
        AppState.vaults = {}
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    }
  }
}
</script>


<style lang="scss" scoped>
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

.smol-keeps {
  height: 10vh;
  width: auto;
}
</style>