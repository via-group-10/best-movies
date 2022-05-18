<script>
    import { onMount } from "svelte";

    export let params = {};
    let filter = params.filter;
    let movies;
//TODO change URL  
    onMount(async () => {
        const res = await fetch(`http://localhost:5252/api/movies?title=${filter}`)
        movies = await res.json();
    })

</script>

<h1>Passed filter : {params.filter}</h1>

{#each movies as movie, i (movie.id)}
<div class="item-container" id = {movie.id}>
    <MovieCarouselItem movie = {movie} movie_index = {i} bind:this={movie_item[i]}/>
</div>
{/each}