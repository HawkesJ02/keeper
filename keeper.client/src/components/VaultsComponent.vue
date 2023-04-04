<template>
  <div class="component">
    <router-link :to="{ name: 'Vault', params: { Id: vault.Id } }">
      <div>
        <h4>{{ vault.name }}</h4>
      </div>
    </router-link>
  </div>
</template>


<script>
import { RouterLink } from "vue-router";
import { logger } from "../utils/Logger";
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {}
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