<script>
    import { onMount } from "svelte";
import MovieCarouselItem from "./MovieCarouselItem.svelte";

    export let title = '';
    let movies = [];

    onMount(async () => {
        const res = await fetch('http://best-movies-api/api/movies')
        movies = await res.json();
    })
</script>

<h2>{ title }</h2>
<section class="movie-carousel">

        <MovieCarouselItem movie = {movies[0]} ></MovieCarouselItem>
</section>

<style>
    .movie-carousel {
        width: 100%;
        height: 250px;
        position: relative;
        background-color: bisque;
        display: flex;
        flex-flow: row nowrap;
    }
/* 
    .movie-carousel::after {
        content: "";
        position: relative;
        width: inherit;
        height: inherit;
        background-color: rgba(0,0,0, 0.2);
    } */

    h2 {
        padding-left: 1rem;
    }
</style>