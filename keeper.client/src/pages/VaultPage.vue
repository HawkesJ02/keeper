<template>
  <div class="component">
    <div>
      {{ vaults.creator.name }}
    </div>
    <div v-for="k in keeps">
      <KeepsComponent :keep="k" />
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
    async function get_keeps_by_vault() {
      try {
        const vaultId = route.params.id
        await vaultsService.get_keeps_by_vault(vaultId);
      } catch (error) {
        Pop.error(error.message)
        logger.error(error)
      }
    }
    async function get_vault_by_id() {
      try {
        const id = route.params.id
        await vaultsService.get_vault_by_id(id);
      } catch (error) {
        Pop.error(error.message)
        logger.error(error)
      }
    }


    watchEffect(() => {
      get_keeps_by_vault();
      get_vault_by_id();
    })

    onUnmounted(() => {
      AppState.keeps = {}
      AppState.vaults = {}
    })
    return {
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    }
  }
}
</script>


<style lang="scss" scoped></style>