<script>
  import Comment from "../Misc/Comment.svelte";

  export let comments = [
    {
      username: "antonjanto",
      created: "25.5.2022",
      text: "random lorem ipsum shit",
    },
    {
      username: "antonjanto",
      created: "25.5.2022",
      text: "random lorem ipsum shit",
    },
  ];
</script>

<div class="text-dark">
  {#if comments}
    <h4 class="mb-0">Recent comments</h4>
    <p class="fw-light mb-4 pb-2">Latest Comments section by users</p>
    {#each comments as comment}
      <Comment {comment} />
      <hr class="my-0" />
    {/each}
  {:else}
    <h4 class="mb-0">No recent comments</h4>
  {/if}
</div>
