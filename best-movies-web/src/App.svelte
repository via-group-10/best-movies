<script>
	import Home from './Home/Home.svelte'
</script>

<navmenu></navmenu>
<main>
	<section class="welcome-section">
		<h1>Hello! Welcome to Best Movies where you get the movies <u>YOU</u> love!</h1>
		<p>In time as the organization will grow, you will see better results!</p>
	</section>
	<Home>

	</Home>
</main>
<footer>

</footer>

<style>
	.welcome-section {
		text-align: center;
		padding: 1em;
		max-width: 50vw;
		margin: 0 auto;
		height: 90vh;
	}

	h1 {
		color: #ff3e00;
		font-size: 4em;
		font-weight: 100;
	}

	footer {
		height: 50vh;
	}
</style>