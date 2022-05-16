<script>
    import { onMount } from "svelte";
    import MovieCarouselItem from "./MovieCarouselItem.svelte";

    export let title = '';
    let movies = [];
    let movie_item = [];

    onMount(async () => {
        const res = await fetch('http://localhost:5252/api/movies?limit=15')
        movies = await res.json();
    })
</script>

<h2>{ title }</h2>

<div class="movie_carousel">

    <div class="movie-items">
        <button class="switchLeft"/>
        {#each movies as movie, i}
            <MovieCarouselItem movie = {movie} movie_index = {i} bind:this={movie_item[i]}/>
        {/each}
        <button class="switchRight"/>
    </div>

</div>


<style>
    
    .movie-items {
        width: auto;
        height: 250px;
        position: relative;
        background-color: bisque;
        display: flex;
        align-items: center;
        flex-flow: row wrap;
        overflow: scroll;
        padding: 0rem 2rem;
    }

    .movie_carousel .switchLeft,
    .movie_carousel .switchRight {
        color: white;
        font-weight: bold;
        position: relative;
        height: 100%;
        width: 2rem;
        font-size: 25px;
        text-align: center;
        background-color: gray;
        top:0;
        z-index: 3;
    }

    .movie_carousel .switchLeft {
        position: absolute;
        left: 0px;
    }

    .movie_carousel .switchRight {
        position: absolute;
        right: 0px;
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