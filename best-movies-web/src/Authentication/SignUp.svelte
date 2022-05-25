<script>

    const url = "BestMoviesApiUrl";
    const endpoint = `/Authentication/signup`;
    import { push } from 'svelte-spa-router';
    import Spinner from '../Misc/Spinner.svelte';
    import { Circle2 } from 'svelte-loading-spinners'
    

    //Authentication

    let username;
    let password;
    let response;
    let showError = false;
    let showSuccess = false;

    const user = {
      Username: username,
      Password: password
    }

    function User(username, password) {
      const registeredUser = Object.create(user);
      registeredUser.Username = username;
      registeredUser.Password = password;
      return registeredUser;
    }


    async function submit() {
        const res = await fetch(url + endpoint, {
          method: 'POST',
          headers: {'Content-Type': 'application/json'},
          body: JSON.stringify(User(username, password))
        });
        response = await res.json();
        if(res.ok){
          successRedirect();
          
        }
        else{
          displayError();
        }}

        function sleep(ms) {
          return new Promise(resolve => setTimeout(resolve, ms));
        }

    function displayError()
    {
      showError = !showError;
    }

    async function successRedirect()
    {
      showSuccess = !showSuccess;
      await sleep(1000);
      push(`/signin`);
    }


</script>

{#if !showSuccess}
  <main class="form-signin">
    <form on:submit|preventDefault={() => submit()}>
      <h1 class="h3 mb-3 fw-normal">Please sign up</h1>
      <div class="form-floating">
        <input
          bind:value={username}
          type="username"
          class="form-control"
          id="floatingInput"
          placeholder="Name"
          style="background-repeat: no-repeat; background-attachment: scroll; background-size: 16px 18px; background-position: 98% 50%;"
          autocomplete="off"
          required
        />
        <label for="floatingInput">Username</label>
      </div>
      <div class="d-flex justify-content-center">
        {#if showError}
          <p class="justify-self-center" style="color:red;">
            Username already in use
          </p>
        {/if}
      </div>

      <div class="form-floating">
        <input
          type="password"
          bind:value={password}
          class="form-control"
          id="floatingPassword"
          placeholder="Password"
          style="background-repeat: no-repeat; background-attachment: scroll; background-size: 16px 18px; background-position: 98% 50%;"
          autocomplete="off"
          required
        />
        <label for="floatingPassword">Password</label>
      </div>
      <button class="w-100 btn btn-lg btn-primary" type="submit">Sign up</button
      >
    </form>
  </main>
{:else}
<div class="vh-100 d-flex flex-column justify-content-center align-items-center">
    <div class="success-box text-center mb-2">
      <p style="font-weight:bold;">Sign up successful!</p>
    </div>
    <Circle2 />
</div>
  
{/if}

<style>
  .form-signin {
    width: 100%;
    max-width: 330px;
    padding: 15px;
    margin: auto;
  }

  .success-box {
    width: 100%;
    height: 2rem;
    max-width: 330px;
    background-color: rgba(106, 255, 0, 1);
  }
</style>
