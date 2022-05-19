<script>
    import { onMount } from "svelte";
    import SearchItem from "./SearchItem.svelte";

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

<div class="container">

    {#if movies}

        {#each movies as movie, i (movie.id)}
            <SearchItem movie={movie}></SearchItem>
        {/each}

        {:else}
        <h1>waiting...</h1>

    {/if}

</div>




<style>

    .container{
        display:grid;
    }



    

</style>
