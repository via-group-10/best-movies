<script>
    import { onMount } from "svelte";
    import { push } from 'svelte-spa-router'
    import Spinner from "../Misc/Spinner.svelte";

    export let movie;
    export let params;
    let dummy_comment = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc in erat sollicitudin eros auctor fringilla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec finibus, tellus nec rutrum. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc in erat sollicitudin eros auctor fringilla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec finibus, tellus nec rutrum';

    let id = params.id;
    const url = "BestMoviesApiUrl";
    const endpoint = `/movies/${id}`;

    onMount(async () => {
        const res = await fetch(url + endpoint);
        movie = await res.json();
    })

    console.log(movie)
</script>


<div class="container pb-2">
    {#if movie}
    <div class="row d-flex justify-content-center">
        <div class="col-md-6">
            <h2 style="margin-bottom:0">{movie.title}</h2>
            <small>duration</small>
        </div>
        <div class="col-md-4 d-md-block d-none">
            <h4 class="mb-0">IMDB Score: {movie.rating.value} <span style="color:yellow">&#9733;</span></h4>
            <h6>({movie.rating.votes}) votes </h6>
        </div>
    </div>
    <div class="row pt-2 d-flex justify-content-center">
        <div class="col-md-10 col-12">
            <div class="movie-picture" />
        </div>
        
    </div>
    <div class="row d-flex justify-content-center p-2">
        <div class="col-md-10 text-left border-bottom">
            <h6>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc in erat sollicitudin eros auctor fringilla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec finibus, tellus nec rutrum viverra, nulla felis vulputate tortor, non placerat nunc lectus id eros. Aliquam id tellus sit amet metus molestie varius. Aenean accumsan gravida nibh, sed faucibus magna. Mauris eleifend dictum tortor ac dictum. Vestibulum maximus molestie porta.</h6>
        </div>
    </div>
    <div class="row d-flex justify-content-center pt-2">
        <div class="col-md-7 col-8 border-bottom">
            <div class="row">
                <div class="col-md-12 d-md-none">
                    <h5>Rating: {movie.rating.value}</h5> 
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    {#if movie.directors.name}
                    <h5>Director: {movie.directors.name}</h5>
                    {:else}
                    <h5>Director: - </h5>
                    {/if}
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 ">
                    <h5>Stars:
                    {#if movie.stars}
                        {#each movie.stars as star}
                            { star.person.name }{ movie.stars[movie.stars.length-1].id === star.id ? '' : ', ' }
                        {/each}
                    {/if}
                    </h5>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-4 border-bottom">
            <h5 class="d-md-none" ><span style="font-weight: bold">{movie.rating.votes}</span> IMDB ratings</h5>
            <h5><span style="font-weight: bold">XY</span> users love this movie</h5>
            <h5><span style="font-weight: bold">XY</span> user comments</h5>
        </div>
    </div>
    <div class="row d-flex justify-content-center pt-2">
        <div class="col-md-10 text-left">
            <div class="card">
                <div class="card-header" on:click={() => push(`/movie/comments/${movie.id}`)}>
                <h5> User comments <span style="font-weight: bold; font-size:1.5rem;">&#8250</span> </h5> 
                </div>
                <div class="card-body">
                <h5 class="card-title"> Latest comment </h5>
                <div class="row">
                    <div class="col-auto">
                        <p class="card-text">Username</p>
                    </div>
                    <div class="col-auto">
                        <small class="card-text">Timestamp</small>
                    </div>
                </div>
                
                <p class="card-text">{dummy_comment}</p>
                </div>
        </div>
        </div>
    </div>
    {:else}
        <Spinner/>
    {/if}
</div>



<style>

    .container{
        padding-top: 2rem;
    }

    .card-header:hover{
        color: blue;
        cursor: pointer;
        transition: color 200ms linear;
    }

    .movie-picture {
        display: block;
        background-color: blanchedalmond;
        aspect-ratio: 16/6;
    }

</style>