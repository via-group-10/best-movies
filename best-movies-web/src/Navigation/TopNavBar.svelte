<script lang="ts">
  import { onDestroy } from "svelte";
  import { push } from "svelte-spa-router";
  import {
    Collapse,
    Navbar,
    NavbarToggler,
    Nav,
    NavItem,
    NavLink,
    NavbarBrand,
  } from "sveltestrap";
  import { User } from "../store";

  let user;
  const unUser = User.subscribe((v) => (user = v));
  onDestroy(unUser);

  $: isSignedIn = !!user;

  let isOpen = false;

  function handleUpdate(event) {
    isOpen = event.detail.isOpen;
  }
</script>

<Navbar color="light" light expand="md">
  <NavbarBrand href="/" class="px-5">&#x1F37F;</NavbarBrand>
  <NavbarToggler on:click={() => (isOpen = !isOpen)} />
  <Collapse {isOpen} navbar expand="md" on:update={handleUpdate}>
    <Nav navbar>
      {#if isSignedIn}
        <NavItem>
          <NavLink on:click={() => push("/myfavorites")}>My favorites</NavLink>
        </NavItem>
      {/if}
    </Nav>
    <Nav class="ms-auto" navbar>
      {#if isSignedIn}
        <NavItem>
          <NavLink href="#" on:click={() => User.signout()}>Sign out</NavLink>
        </NavItem>
      {:else}
        <NavItem>
          <NavLink on:click={() => push("/signin")}>Sign in</NavLink>
        </NavItem>
        <NavItem>
          <NavLink on:click={() => push("/signup")}>Sign up</NavLink>
        </NavItem>
      {/if}
    </Nav>
  </Collapse>
</Navbar>

<style>
</style>
