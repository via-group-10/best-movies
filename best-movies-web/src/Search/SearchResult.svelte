<script>
    import { onMount } from "svelte";
    import MovieListItem from "../Movies/MovieListItem.svelte";
    import Spinner from "../Misc/Spinner.svelte";
    import { getMovies } from "../api";
    import { inview } from "svelte-inview/dist/index";
    import { Alert, Icon } from 'sveltestrap';

    export let params = {};
    let filter = params.filter;
    let movies;
    let hasMoreResults = true;

    onMount(async () => {
        fetchData(0)
    })

    const fetchData = async (offset) => {
        const res = await getMovies({
            title: filter,
            offset: offset,
            limit: 5
        });
        let resBody = await res.json();
        if (res.ok && resBody.length > 0)
        {
            movies = !movies ? resBody: [...movies, ...resBody];
        }
        else{
            hasMoreResults = false;
        }
    }

    const handleChange = (e) => {

        if(e.detail.inView && hasMoreResults)
        {
            fetchData(movies.length);
        }
    }

</script>

<div class="container pb-4">

    {#if movies}

        {#each movies as movie}
            <MovieListItem movie={movie}></MovieListItem>
        {/each}

        <div use:inview={{}} on:change={handleChange} />

    {:else}
        <Spinner />
    {/if}

    
    

    {#if !hasMoreResults}
        <Alert color = "info" class="text-center my-5">
            <h4 class="alert-heading text-capitalize"> <Icon name="info-circle-fill"/> Has no more results</h4>
        </Alert>
    {/if}

</div>




<style>

</style>
