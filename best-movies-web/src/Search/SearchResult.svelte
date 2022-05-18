<script>
    import { onMount } from "svelte";

    export let params = {};
    let filter = params.filter;
    let movies;

    const url = "BestMoviesApiUrl";
    const endpoint = `/movies?title=${filter}`;

    onMount(async () => {
        const res = await fetch(url + endpoint);
        movies = await res.json();
    })

</script>

<h1>Passed filter : {params.filter}</h1>

{#if movies}

    {#each movies as movie, i (movie.id)}
    <div class="item-container" id = {movie.id}>
        <h1>{movie.title}</h1>
    </div>
    {/each}

{:else}
<h1>waiting...</h1>

{/if}
