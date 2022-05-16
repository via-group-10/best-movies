<script>
    export let movie;
    export let movie_index;

    const colors = ['black', 'white', 'blue', 'lightblue', 'green', 'grey']

    function bg_color() {
        const c = movie_index % 6;
        return colors[c];
    }

    let color = bg_color();
</script>

<article class="movie" style="--color: {color}">
    {#if movie }
    <h5> { movie.title } </h5>
    {/if}
</article>

<style>
    .movie {
        width: 25%;
        height: 10rem;
        background-color: var(--color);
        position: relative;
    }

    .movie h5 {
        position: absolute;
        top: 15%;
        bottom: 15%;
        left: 10%;
        right: 10%;
    }

</style>