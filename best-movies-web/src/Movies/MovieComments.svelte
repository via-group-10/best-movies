<script>
  import Comment from "../Misc/Comment.svelte";
  import { User } from "../store";
  import { replace } from 'svelte-spa-router'
  import {
    Row,
    Col,
    Collapse,
    Button,
    Form,
    Input,
    FormGroup,
    Label,
    CardBody,
  } from "sveltestrap";
  import { postComment } from "../api";
  import { createEventDispatcher, onMount } from 'svelte';

  export let comments = [];
  export let movieId;

  const dispatch = createEventDispatcher();
  function dispatchReload()
  {
      dispatch('message', {
        'event': 'reload'
      });
  }

  let isOpen = false;

  let user;
  User.subscribe((v) => (user = v));

  $: isSignedIn = !!user;

  function handleUpdate(event) {
    isOpen = event.detail.isOpen;
  }

  let commentToPost;

  async function handleSubmit() {
    let res = await postComment(movieId, commentToPost);
    commentToPost = undefined;
    let response = await res.json();
    if (res.ok) {
      dispatchReload()
    }
  }
</script>

<div>
  <Row>
    <Col xs={{ size: "auto", offset: 1 }} sm="6" class="offset-sm-0">
      {#if comments.length !== 0}
        <h4 class="mb-0">Recent comments</h4>
      {:else}
        <h4 class="mb-0">No recent comments</h4>
      {/if}
    </Col>
    <Col xs={{ size: "auto", offset: 1 }} sm="auto" class="ms-sm-auto">
      {#if isSignedIn}
        <h4>
          <a
            on:click|preventDefault={() => (isOpen = !isOpen)}
            class="card-link"
            href="./"
          >
            Post your opinion
          </a>
        </h4>
      {/if}
    </Col>
  </Row>

  {#if isSignedIn}
    <Collapse {isOpen} on:update={handleUpdate}>
      <CardBody class="mt-4">
        <form on:submit|preventDefault={handleSubmit}>
          <div class="d-flex flex-start">
            <img
              class="rounded-circle shadow-1-strong me-3"
              src="images/avatar.svg"
              alt="avatar"
              width="60"
              height="60"
            />
            <div class="w-100">
              <h6 class="fw-bold mb-1">Your name</h6>
              <p class="mb-0">
                <Input
                  type="textarea"
                  placeholder="Write here..."
                  bind:value={commentToPost}
                  required
                />
              </p>
            </div>
          </div>
          <div class="d-flex justify-content-end">
            <Button type="submit" color="primary">Post comment</Button>
          </div>
        </form>
      </CardBody>
      <hr />
    </Collapse>
  {/if}

  {#if comments.length !== 0}
    {#each comments as comment}
      <Comment {comment} />
      <hr class="my-0" />
    {/each}
  {/if}
</div>
