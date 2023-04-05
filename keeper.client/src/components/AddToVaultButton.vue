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
    vault: { type: Object, required: true }
  },

  setup() {
    return {
      vaults: computed(() => AppState.myVaults),
      activekeep: computed(() => AppState.activekeep),
      async addKeepToVault(id) {
        const keepData = this.activekeep
        keepData.vaultId = id
        keepData.keepId = this.activekeep.id
        await keepsService.addKeepToVault(keepData)
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>