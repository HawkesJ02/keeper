<template>
  <div class="component">
    <div>
      <img @click="get_keep_by_id(keep.id)" class="img-fluid rounded" :src="keep.img" :alt="keep.name" :title="keep.name"
        data-bs-toggle="modal" data-bs-target="#exampleModal">
      <h4>{{ keep.name }}</h4>
    </div>
  </div>

  <!-- Modal -->
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="container">
          <div class="row">
            <div class="col-6 p-0">
              <img class="img-modal img-fluid" :src="activekeep.img" :alt="activekeep.name" :title="activekeep.name">
            </div>
            <div class="col-6">
              <section class="d-flex flex-row">
                <div>{{ activekeep.views }}</div>
                <div>SAMPLE</div>
              </section>
              <h1>{{ activekeep.name }}</h1>
              <p>{{ activekeep.description }}</p>
              <section>
                <!-- <router-link :to="{ name: 'Profile', params: { creatorId: activekeep?.creatorId } }">
                  <img :src="activekeep?.creator.picture" :alt="activekeep.creator.name" class="profile-picture">
                </router-link> -->

              </section>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { RouterLink } from "vue-router";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import { computed } from "vue";
import Pop from "../utils/Pop";


export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      async get_keep_by_id(id) {
        try {
          await keepsService.get_keep_by_id(id)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      activekeep: computed(() => AppState.activekeep)
    };
  },
  components: { RouterLink }
}
</script>


<style lang="scss" scoped>
.img-modal {
  height: 80vh;
  width: 50vw;
}

.profile-picture {
  height: 6vh;
  width: 6vh;
  border-radius: 50%;
  box-shadow: 2px 2px 4px black;
}
</style>