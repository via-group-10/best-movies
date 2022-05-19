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

<article class="movie-item" style="--color: {color}">
    {#if movie }
    <h5> { movie.title } </h5>
    {/if}
</article>

<style>

    .movie-item {
       display: flex;
       flex: 1 1 100%;
       padding: var(--item-gap);
       background-color: var(--color);
       background-clip: content-box;
       border-radius: var(--border-radius);
       text-align: center;
       align-items: center;
       justify-content: center;
   }

</style>