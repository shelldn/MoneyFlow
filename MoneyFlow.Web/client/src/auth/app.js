angular.module('mf.auth', ['ngResource', 'LocalStorageModule'])

    .config(function($httpProvider, localStorageServiceProvider) {
        $httpProvider.interceptors.push('authInterceptor');
        localStorageServiceProvider.setPrefix('mf');
    });