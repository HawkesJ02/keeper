<template>
  <div class="container py-3">
    <section class="bricky">
      <div v-for="k in keeps">
        <KeepsComponent :keep="k" />

      </div>
    </section>
  </div>
</template>

<script>
import { watchEffect } from "vue";
import { AppState } from "../AppState";
import { computed } from "vue";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    watchEffect(() => {
      getKeeps()
    })
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.bricky {
  columns: 300px;
  column-gap: .5em;

  &>div {
    margin-top: .5em;
    display: inline-block;
  }
}
</style>
