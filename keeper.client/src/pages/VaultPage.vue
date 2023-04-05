<template>
  <div class="component">
    <div>
      {{ vaults.name }}
      <!-- {{ vaults.creator.name }} -->
      <button v-if="account?.id == vaults?.creatorId" @click="delete_vault_by_id();"><i class="mdi mdi-delete"
          title="Delete Vault!"></i></button>
      <i v-if="vaults.isPrivate == true" class="mdi mdi-lock"> PRIVATE VAULT </i>
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
import Pop from "../utils/Pop";
import { watchEffect } from "vue";
import { computed } from "vue";
import { onUnmounted } from "vue";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
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
        router.push({ name: 'Home' })
      }
    }
    async function get_vault_by_id() {
      try {
        const id = route.params.id;
        await vaultsService.get_vault_by_id(id);
      } catch (error) {
        Pop.error(error.message)
        logger.error(error)
        router.push({ name: 'Home' })
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
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),

      async delete_vault_by_id() {
        try {
          const id = route.params.id;
          await vaultsService.delete_vault_by_id(id);
          router.push({ name: 'Home' })
        } catch (error) {
          Pop.error(error.message)
          logger.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>