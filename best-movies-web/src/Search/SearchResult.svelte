<script>
    import { onMount } from "svelte";
    import SearchItem from "./SearchItem.svelte";
    import Spinner from "../Misc/Spinner.svelte";
    import { getMovies } from "../api";

    export let params = {};
    let filter = params.filter;
    let movies;

    onMount(async () => {
        const res = await getMovies({
            title: filter
        });
        if (res.ok)
            movies = await res.json();
    })

</script>

<div class="container pb-4">

    {#if movies}

        {#each movies as movie, i (movie.id)}
            <SearchItem movie={movie}></SearchItem>
        {/each}

    {:else}
        <Spinner />
    {/if}

</div>




<style>

</style>
