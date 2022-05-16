<script>
	import MovieCarousel from "../Movies/MovieCarousel.svelte";

</script>

<section class="welcome-section">
		<h1>Hello! Welcome to Best Movies where you get the movies <u>YOU</u> love!</h1>
		<p>In time as the organization will grow, you will see better results!</p>
</section>

<h1>Movies</h1>
<MovieCarousel title = { 'First 15 movies' } ></MovieCarousel>

<style>
	h1 {
		text-align: center;
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	.welcome-section {
		text-align: center;
		padding: 1em;
		max-width: 50vw;
		margin: 0 auto;
		height: 90vh;
	}

	footer {
		height: 50vh;
	}
</style>