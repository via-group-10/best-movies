<script>
	import Home from './Home/Home.svelte';
	import Router from 'svelte-spa-router';
	import MovieDetail from './Movies/MovieDetail.svelte';
	import NotFound from './Misc/NotFound.svelte';

	let routes = {
		"/": Home,
		"/movie": MovieDetail,


		"*": NotFound
	}
</script>

<navmenu></navmenu>
<main>
	<Router {routes}/>
</main>
<footer>

</footer>

<style>
	footer {
		height: 50vh;
	}
</style>