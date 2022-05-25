<script>
    import { onMount } from "svelte";
    import SearchItem from "./SearchItem.svelte";
    import { Circle2 } from 'svelte-loading-spinners';

    export let params = {};
    let filter = params.filter;
    let movies;

    const url = "BestMoviesApiUrl";
    const endpoint = `/movies?title=${filter}`;

    onMount(async () => {
        const res = await fetch(url + endpoint, {
            headers: {'Authentication': window.localStorage.getItem('authToken')},
        });
        movies = await res.json();
    })

</script>

<div class="container pb-4">

    {#if movies}

        {#each movies as movie, i (movie.id)}
            <SearchItem movie={movie}></SearchItem>
        {/each}

    {:else}
        <div class="row vh-100">
            <div class="col-12 d-flex justify-content-center align-items-center">
                <Circle2 size="100" color="#FF3E00"/>
            </div>
        </div>
    {/if}

</div>




<style>

</style>
