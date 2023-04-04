<template>
  <div class="component">
    <div>
      <img class="img-fluid rounded" :src="keep.img" :alt="keep.name" :title="keep.name" data-bs-toggle="modal"
        data-bs-target="#exampleModal">
      <h4>{{ keep.name }}</h4>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl">
        <div class="modal-content">
          <div class="container">
            <div class="row">
              <div class="col-6 p-0 m-0">
                <img class="img-fluid rounded" :src="keep.img" :alt="keep.name" :title="keep.name">
              </div>
              <div class="col-6">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad, obcaecati? Cum, amet
                pariatur nobis ad nemo vel alias beatae repellat ex numquam necessitatibus praesentium hic iusto rerum
                nulla in quae fugit corrupti! Ipsum commodi similique praesentium dignissimos recusandae facere cupiditate
                laboriosam maiores. Vel officiis quo perspiciatis harum cupiditate quis velit dolor voluptatibus, est,
                laudantium nostrum ratione quas. Dolore omnis error sit deserunt. Quasi error repellat molestiae
                dignissimos eum consequatur quidem, voluptatem necessitatibus ipsa harum ea, distinctio unde deleniti
                nobis impedit fugit dicta debitis maiores molestias, ut incidunt voluptas repellendus nam! Quod velit
                sequi eius cumque enim hic est assumenda eaque!</div>
            </div>
          </div>
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


<style lang="scss" scoped>.img-modal {
  height: 80vh;
  width: 50vw;
}</style>