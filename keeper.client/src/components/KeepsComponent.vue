<template>
  <div class="component">
    <div>
      <img class="img-fluid rounded" :src="keep.img" :alt="keep.name" :title="keep.name">
      <h4>{{ keep.name }}</h4>
    </div>

  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>