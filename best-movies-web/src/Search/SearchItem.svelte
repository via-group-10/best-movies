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

<div class="row" style="margin-bottom:2rem">
    <div class="col-md-4 movie-picture" on:click={() => push(`/movie/${movie.id}`)}>
        <!--TODO ADD movie picture-->
    </div>
    <div class="col-md-8">
        <div class="row">
            <div class="col-auto movie-title" on:click={() => push(`/movie/${movie.id}`)}>{movie.title}</div>

            <div class="col-lg" style="font-size:1.2rem; padding-top:0.8rem">Ratings</div>
        </div>
        <div class="row">
            <div class="col-md movie-stars">
                Starring: <!--TODO ADD stars-->
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
        background-color: blanchedalmond;
        aspect-ratio: 16/9;
        max-width: 400px;
        max-height: 225px;
    }

    .movie-title {
        font-size: 2rem;
    }

    .movie-title:hover{
        color: blue;
        cursor: pointer;
        transition: color 400ms ease ;
    }

    .movie-picture:hover{
        opacity: 0.6;
        cursor: pointer;
        transition: opacity 400ms ease;
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

    @media only screen and (max-width: 768px) {
        .movie-picture {
            max-width: 100%;
        }
    }
</style>
