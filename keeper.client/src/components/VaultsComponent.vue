<template>
  <div v-if="vault.isPrivate == false || vault.isPrivate == true && vault.creatorId == account.id" class="component">
    <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
      <div>
        <img class="img-fluid" :src="vault?.imgUrl" :alt="vault?.name" :title="vault?.name">
      </div>
      <h4>{{ vault.name }}</h4>
    </router-link>
  </div>
  <i v-if="vault.isPrivate == true && vault.creatorId == account.id" class="mdi mdi-lock"></i>
</template>


<script>
import { RouterLink } from "vue-router";
import { computed } from "vue";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { RouterLink }
}
</script>
<style lang="scss" scoped>
.profile-picture {
  height: 6vh;
  width: 6vh;
  border-radius: 50%;
  box-shadow: 2px 2px 4px black;
}
</style>