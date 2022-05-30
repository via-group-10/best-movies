<script>
	import MovieCarousel from "../Movies/MovieCarousel.svelte";
	import { sineIn } from 'svelte/easing';
	import Search from 'svelte-search';
	import { link, push } from 'svelte-spa-router';
	
	let filter;

	const swoopRight = () => swoop('right');
	const swoopLeft = () => swoop('left');

	function swoop(side){
		let start = side === 'left' ? -70 : 70 
		return {
			duration: 2500,
			easing: sineIn,
			css: (t) => `transform: translateX(${start + t * -start}%); opacity: ${t};`
		}
	}

</script>

<section class="welcome-section">
		<h1 in:swoopLeft>Welcome to Best Movies &#x1F37F;</h1>
		<h2 in:swoopRight>What are we watching today?</h2>
</section>

<div class="search-container">
	<Search on:submit={() => push(`/search/${filter}`)} bind:value={filter} label={''}/>
</div>


 
<!-- <h1>Movies</h1>
<MovieCarousel title = { 'First 15 movies' } ></MovieCarousel> -->

<style>
	:global([data-svelte-search] input) {
		width: 28rem;
		font-size: 1rem;
		padding: 0.5rem;
		margin: 0.5rem 0;
		border: 1px solid #e0e0e0;
		border-radius: 0.25rem;
	}

	.search-container{
		margin: 5rem 0rem;
		display: flex;
		width: 100%;
		justify-content: center;
	}

	h1 {
		text-align: center;
		color: #ff3e00;
		text-transform: uppercase;
		font-weight: 100;
	}

	h2 {
		text-align: center;
		color: #ff3e00;
		text-transform: uppercase;
		font-weight: 100;
	}

	.welcome-section {
		text-align: center;
		padding: 1em;
		margin: 0 auto;
		overflow-x: hidden;
	}
</style>