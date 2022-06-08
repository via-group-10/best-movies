<script>
  import MovieListItem from "../Movies/MovieListItem.svelte";
  import Spinner from "../Misc/Spinner.svelte";
  import { getFavoriteMovies } from "../api";

  let moviesPromise;

  function getMoviesPromise() {
    return getFavoriteMovies().then((res) => {
      if (res.ok) {
        return res.json();
      }
      else if (res.status === 401)
      {
        User.signout();
        push("/signin");
      }
      else {
        return res.json().then((r) => {
          if (r.type === "https://tools.ietf.org/html/rfc7231#section-6.5.1") {
            throw new Error(r.errors.id[0]);
          } else {
            throw new Error(r.message);
          }
        });
      }
    });
  }

  moviesPromise = getMoviesPromise();
</script>

<div class="container pb-4">
  <h1>The movies I loved to binge (MILB)</h1>

  {#await moviesPromise}
    <Spinner />
  {:then data}
    {#each data as movie}
      <MovieListItem {movie} />
    {/each}
  {:catch error}
    <p>{error.message}</p>
  {/await}
</div>
