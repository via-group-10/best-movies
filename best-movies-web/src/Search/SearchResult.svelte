<script>
    import { onMount } from "svelte";
    import MovieListItem from "../Movies/MovieListItem.svelte";
    import Spinner from "../Misc/Spinner.svelte";
    import { getMovies } from "../api";
    import { inview } from "svelte-inview/dist/index";
    import { Alert, Icon } from "sveltestrap";
    import { push } from "svelte-spa-router";
    import { User } from '../store';

    export let params = {};
    let filter = params.filter;
    let movies;
    let hasMoreResults = true;

    onMount(async () => {
        fetchData(0);
    });

    const fetchData = (offset) => {
        getMovies({
            title: filter,
            offset: offset,
            limit: 5,
        })
            .then((res) => {
                if (res.ok) {
                    res.json().then((resBody) => {
                        if (resBody.length > 0) {
                            movies = !movies
                                ? resBody
                                : [...movies, ...resBody];
                        } else {
                            if  (!movies) {
                                movies = [];
                            }
                            hasMoreResults = false;
                        }
                    });
                }
            })
            .catch((e) => {
                User.signout();
                push("/signin");
            });
    };

    const handleChange = (e) => {
        if (e.detail.inView && hasMoreResults) {
            fetchData(movies.length);
        }
    };
</script>

<div class="container pb-4">
    {#if movies}
        {#each movies as movie}
            <MovieListItem {movie} />
        {/each}

        <div use:inview={{}} on:change={handleChange} />
    {:else}
        <Spinner />
    {/if}

    {#if !hasMoreResults}
        <Alert color="info" class="text-center my-5">
            <h4 class="alert-heading text-capitalize">
                <Icon name="info-circle-fill" /> No results
            </h4>
        </Alert>
    {/if}
</div>

<style>
</style>
