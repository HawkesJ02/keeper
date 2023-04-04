<template>
  <div class="component">
    <div>
      <li class="dropdown-item" @click="addKeepToVault(vault.id)">{{
        vault.name }}
      </li>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
export default {
  props: {
    vaults: { type: Object, required: true }
  },

  setup() {
    return {
      vaults: computed(() => AppState.myVaults),
      async addKeepToVault(vaultId) {
        const keep = this.activekeep
        keep.VaultId = vaultId
        await keepsService.addKeepToVault(keep)
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>