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
        this.increase_kept(keepData.keepId);

      },
      async increase_kept(id) {
        var id = id
        var Kept = this.activekeep.kept + 1;
        await keepsService.increase_kept({ id, Kept })
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>