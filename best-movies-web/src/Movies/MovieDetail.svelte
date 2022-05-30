<script>
  import { onMount } from "svelte";
  import Spinner from "../Misc/Spinner.svelte";
  import MovieComments from "./MovieComments.svelte";
  import { getMovie, addToFavorites } from "../api";
  import { Badge, Icon, Toast, ToastBody, ToastHeader } from "sveltestrap";

  export let params;

  let movie;

  let id = params.id;
  let toastMessage;
  let isToasterOpen = false;

  onMount(async () => {
    const res = await getMovie(id);
    if (res.ok) {
      let response = await res.json();
      movie = response;
    }
  });

  function handleMessage(event) {
    if (event.detail.event === "reload") {
      getMovie(id)
        .then((r) => r.json())
        .then((b) => (movie = b));
    }
  }

  async function makeThisMyFavorite() {
    const res = await addToFavorites(movie.id);
    const data = await res.json();

    triggerToast(data.message);
  }

  function triggerToast(msg) {
    isToasterOpen = true;
    toastMessage = msg;
  }
</script>

<div class="container-fluid container-lg pt-3">
  {#if movie}
    <div class="row justify-content-center">
      <div class="col-md-6">
        <h2 style="margin-bottom:0">{movie.title}</h2>
      </div>
      <div class="col-md-4 d-md-block d-none">
        <h4 class="mb-0">
          IMDB Score:
          <Badge color="warning">
          {#if movie.rating}
            {movie.rating.value}
          {/if}
          <i class="bi bi-star"/>
        </Badge>
          </h4>
        <h6>
          {#if movie.rating}
            ({movie.rating.votes})
          {/if}
          votes
        </h6>
      </div>
    </div>
    <div class="row mt-2 justify-content-center">
      <div class="col-12 col-md-10 mb-2">
        <div class="movie-picture d-flex justify-content-center">
          {#if movie.imageUrl}
          <img class="img-fluid" src="{movie.imageUrl}" alt="Movie Image"/>
          {:else}
          <img class="img-fluid" src="images/img-unavailable.png" alt="Movie Image"/>
          {/if}
          </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-10 text-left border-bottom">
        <p style="font-size:1.2rem">
          {movie.synopsis}
        </p>
      </div>
    </div>
    <div class="row mt-2 justify-content-center">
      <div class="col-md-7 col-8 border-bottom">
        <div class="row d-md-none mb-2">
          <div class="col-md-12 ">
            <h5>
              Rating:
              {#if movie.rating != null}
                {movie.rating.value}
              {/if}
            </h5>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-md-12">
            <h5>Director:</h5>
            {#if movie.directors}
              {#each movie.directors as director}
                {director.person.name}{movie.directors[
                  movie.directors.length - 1
                ].id === director.id
                  ? ""
                  : ", "}
              {/each}
            {:else}
              -
            {/if}
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-md-12 ">
            <h5>Stars:</h5>
            {#if movie.stars}
              {#each movie.stars as star}
                {star.person.name}{movie.stars[movie.stars.length - 1].id ===
                star.id
                  ? ""
                  : ", "}
              {/each}
            {/if}
          </div>
        </div>
      </div>
      <div class="col-md-3 col-4 border-bottom">
        <div class="row d-md-none mb-2">
          <div class="col-12">
            <h5>
              <span style="font-weight: bold">
                {#if movie.rating}
                  ({movie.rating.votes} )
                {/if}
              </span>
              IMDB ratings
            </h5>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-12">
            <button
              on:click={makeThisMyFavorite}
              type="button"
              class="btn btn-outline-warning w-100"
            >
              <i class="bi bi-star" /> Make this my favorite
            </button>
            <div class="toast-container position-fixed bottom-0 end-0 p-3">
            <Toast
              autohide
              body
              header="Make this my favorite"
              isOpen = {isToasterOpen}
              on:close={() => (isToasterOpen = false)}>

              {toastMessage}
            </Toast>
          </div>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-12">
              <span style="font-weight: bold">
                {movie.favoredByUsers.length}
              </span>
              { movie.favoredByUsers.length === 1 ? 'user' : 'users' } made this movie their favorite
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-12">
              <span style="font-weight: bold">
                { movie.comments.length }
              </span>
              user { movie.comments.length === 1 ? 'comment' : 'comments' }
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-2">
      <div class="col-md-10">
        <MovieComments
          on:message={handleMessage}
          movieId={movie.id}
          comments={movie.comments}
        />
      </div>
    </div>
  {:else}
    <Spinner />
  {/if}
</div>

<style>
  .movie-picture {
        display: block;
        aspect-ratio: 16/9;
    }
</style>
