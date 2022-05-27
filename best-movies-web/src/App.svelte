<script>
	import Router from 'svelte-spa-router';
	import routes from './routes';
	import TopNavBar from './Navigation/TopNavBar.svelte';
	
</script>

<svelte:head>
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
</svelte:head>

<TopNavBar/>

<main>
	<Router {routes}/>
</main>

<footer>

</footer>
