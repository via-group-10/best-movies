<script>
    export let movie;
    import { push } from 'svelte-spa-router'
    let synopsis = movie.synopsis;
    let collapsable = synopsis.length > 250 ? true : false;
    let collapsed = true;
    let collapse_prompt = "show more";
    let displayed_synopsis = synopsis.slice(0, 250) + "...";

    function promptClicked() {
        collapsed = !collapsed;
        changeDisplayedElements();
    }

    function changeDisplayedElements() {
        displayed_synopsis =
            collapsed === true ? synopsis.slice(0, 250) + "..." : synopsis;
        collapse_prompt = collapsed === true ? "show more" : "show less";
    }
</script>

<div class="row mt-5">
    <div class="col-12 col-lg-5 movie-picture" on:click={() => push(`/movie/${movie.id}`)}>
        {#if movie.imageUrl}
        <img class="img-fluid" src="{movie.imageUrl}" alt="Movie Image"/>
        {:else}
        <img class="img-fluid" src="images/img-unavailable.png" alt="Movie Image"/>
        {/if}
    </div>
    <div class="col-12 col-lg-7">
        <div class="row">
            <div class="col-auto" on:click={() => push(`/movie/${movie.id}`)}>
                <h2 class="movie-title">{ movie.title }</h2>
            </div>

            <div class="col-lg" style="padding-top:0.8rem">
                Rating: 
                {#if movie.rating}
                    { movie.rating.value } <small>({ movie.rating.votes } votes)</small>
                {:else}
                    <small>unavailable</small>
                {/if}
            </div>
        </div>
        <div class="row">
            <div class="col-md movie-stars">
                <strong>Starring:</strong>
                {#if movie.stars}
                    {#each movie.stars as star}
                        { star.person.name }{ movie.stars[movie.stars.length-1].id === star.id ? '' : ', ' }
                    {/each}
                {/if}
            </div>
        </div>
        <div class="row">
            <div class="col-md movie-synopsis">
                {#if collapsable}
                    <!--TODO ADD synopsis-->
                    <p style="font-size:1.2rem">
                        Synopsis: <br />
                        {displayed_synopsis}
                        <span
                            class="collapse-prompt"
                            on:click={promptClicked}
                            style="color:blue">{collapse_prompt}</span
                        >
                    </p>
                {:else}
                    <p style="font-size:1.2rem">
                        Synopsis: <br />
                        {movie.synopsis}
                    </p>
                {/if}
            </div>
        </div>
    </div>
</div>

<style>
    .movie-picture {
        display: block;
        max-width: 15rem;
        max-height: 8rem;
        aspect-ratio: 16/9;
    }

    .movie-title:hover{
        color: blue;
        cursor: pointer;
        transition: color 200ms linear;
    }

    .movie-picture:hover{
        opacity: 0.6;
        cursor: pointer;
        transition: opacity 300ms ease-out;
    }

    .collapse-prompt:hover{
        cursor: pointer;
    }

    .movie-stars {
        font-size: 1.2rem;
    }

    .collapse-prompt:hover {
        text-decoration: underline;
    }
</style>
