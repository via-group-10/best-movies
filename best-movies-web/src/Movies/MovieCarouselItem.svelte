<script>
    export let movie;

    const colors = ['black', 'white', 'blue', 'lightblue', 'gree', 'grey']

    function bg_color() {
        const c = movie.id % 6;
        return colors[c];
    }
</script>

<article class="movie">
    {#if movie }
    <h5> { movie.title } </h5>
    {/if}
</article>

<style>
    .movie {
        width: 25%;
        background-color: bg_color();
        position: relative;
    }

    .movie h5 {
        position: absolute;
        bottom: 2rem;
        left: 1rem;
        right: 1rem;
    }

</style>