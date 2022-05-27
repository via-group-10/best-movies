import { writable } from 'svelte/store';

export const User = function () {
    const { subscribe, set } = writable(window.localStorage.getItem('authToken'));
    return {
        subscribe,

        signout: () => { 
            set(null);
            window.localStorage.removeItem('authToken');
        },
        signin: (token) => { 
            set(token);
            window.localStorage.setItem('authToken', token);
        }
    }
}()