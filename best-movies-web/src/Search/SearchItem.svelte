<script>
    export let movie;
    import { push } from 'svelte-spa-router'
    let synopsis =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc in erat sollicitudin eros auctor fringilla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec finibus, tellus nec rutrum viverra, nulla felis vulputate tortor, non placerat nunc lectus id eros. Aliquam id tellus sit amet metus molestie varius. Aenean accumsan gravida nibh, sed faucibus magna. Mauris eleifend dictum tortor ac dictum. Vestibulum maximus molestie porta.";

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
    <div class="col-12 col-lg-5" on:click={() => push(`/movie/${movie.id}`)}>
        <!--TODO ADD movie picture-->
        <div class="movie-picture" />
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
                    <p>
                        Synopsis: <br />
                        {synopsis}
                    </p>
                {/if}
            </div>
        </div>
    </div>
</div>

<style>
    .movie-picture {
        display: block;
        background-color: blanchedalmond;
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
