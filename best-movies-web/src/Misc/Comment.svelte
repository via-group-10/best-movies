<script>
    export let comment;
</script>

<div class="card-body">
  <div class="d-flex flex-start">
    <img
      class="rounded-circle shadow-1-strong me-3"
      src="images/avatar.svg"
      alt="avatar"
      width="60"
      height="60"
    />
    <div>
      <h6 class="fw-bold mb-1">{ comment.username }</h6>
      <div class="d-flex align-items-center mb-3">
        <p class="mb-0">
          { comment.created }
        </p>
      </div>
      <p class="mb-0">
          { comment.text }
      </p>
    </div>
  </div>
</div>
