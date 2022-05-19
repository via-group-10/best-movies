<script>
    import { onMount } from "svelte";
    import { flip } from "svelte/animate"
    import MovieCarouselItem from "./MovieCarouselItem.svelte";

    export let title = '';
    export let animation_speed = 500;
    let movies = [];
    let movie_item = [];
    const url = "BestMoviesApiUrl";
    const endpoint = "/movies?limit=15";

    onMount(async () => {
        let res = await fetch(url + endpoint);
        if (res.ok)
            movies = await res.json();
    })

    const onRotateLeft = e => {
        const transitionImage = movies[movies.length - 1]
        document.getElementById(transitionImage.id).style.opacity = 0;
        movies = [movies[movies.length - 1],...movies.slice(0, movies.length - 1)]
        setTimeout(()=> (document.getElementById(transitionImage.id).style.opacity = 1), animation_speed/1.4)
    }

    const onRotateRight = e => {
        const transitionImage = movies[0]
        document.getElementById(transitionImage.id).style.opacity = 0;
        movies = [...movies.slice(1, movies.length), movies[0]]
        setTimeout(()=> (document.getElementById(transitionImage.id).style.opacity = 1), animation_speed/1.4)
    }

</script>

<div class="info-container">
    <div class="header">
        <h3>{ title }</h3>
        <div class="progress-bar"></div>
    </div>
</div>

<div class="movie-carousel">
    <button id="left" on:click={onRotateLeft} class="handle left-handle">
        <div class="arrow">
            &#8249;
        </div>
    </button>
    <div class="slider">
            {#each movies as movie, i (movie.id)}
            <div class="item-container" animate:flip={{duration:animation_speed}} id = {movie.id}>
                <MovieCarouselItem movie = {movie} movie_index = {i} bind:this={movie_item[i]}/>
            </div>
            {/each}
    </div>
    <button id="right" on:click={onRotateRight} class="handle right-handle">
        <div class="arrow">
            &#8250;
        </div>
    </button>
</div>


<style>

    :root{
        --slider-padding: 5rem;
        --item-gap: 0.25rem;
        --border-radius: 0.8rem;
    }

   .movie-carousel {
       display: flex;
       justify-content: center;
       overflow: hidden;
   }

   .slider {
       --items-per-screen: 4;
       display: flex;
       width: calc(100% - var(--slider-padding));
       margin: 0 var(--item-gap);
       transition: 400ms ease-in-out;
   }

   .handle{
        background-color: rgba(0,0,0,0.25);
        z-index: 10;
        margin: var(--item-gap) 0;
        border: none;
        border-radius: var(--border-radius);
        font-size: 3rem;
        display:flex;
        justify-content: center;
        color: white;
        transition: background-color 150ms ease-in-out;
    }

    .item-container{
        display:flex;
        flex: 0 0 calc(100% / var(--items-per-screen));
    }

    .arrow {
            transition: transform 150ms ease-in-out;
            margin: auto;
        }

    .handle:hover {
        background-color: rgba(0,0,0,0.5);
    }

    .handle:hover .arrow{
        transform: scale(1.3);
    }

    .left-handle{
        border-top-left-radius: 0;
        border-bottom-left-radius: 0;
    }

    .right-handle{
        border-top-right-radius: 0;
        border-bottom-right-radius: 0;
    }

    .header{
        display: flex;
        justify-content: space-between;
        margin-left: var(--slider-padding);
    }

/* 
    .movie-carousel::after {
        content: "";
        position: relative;
        width: inherit;
        height: inherit;
        background-color: rgba(0,0,0, 0.2);
    } */


    @media only screen and (max-width: 600px){
        .slider{
            --items-per-screen:3
        }
    }

    @media only screen and (min-width: 600px){
        .slider {
            --items-per-screen:4
        }
    }

    @media only screen and (min-width: 768px){
        .slider {
            --items-per-screen:5
        }
    }

    @media only screen and (min-width: 992px){
        .slider {
            --items-per-screen:6
        }
    }
</style>