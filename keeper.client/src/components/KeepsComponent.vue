<template>
  <div class="component">
    <div>
      <img class="img-fluid rounded" :src="keep.img" :alt="keep.name" :title="keep.name" data-bs-toggle="modal"
        data-bs-target="#exampleModal">
      <h4>{{ keep.name }}</h4>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div>
            <!-- <img :src="keep.img"> -->
            {{ keep.name }}
          </div>
          <!-- <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div> -->
        </div>
      </div>
    </div>

  </div>
</template>


<script>
import { logger } from "../utils/Logger";


export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped></style>