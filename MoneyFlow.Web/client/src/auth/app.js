angular.module('mf.auth', ['LocalStorageModule'])

    .config(function($httpProvider, localStorageServiceProvider) {
        $httpProvider.interceptors.push('authInterceptor');
        localStorageServiceProvider.setPrefix('mf');
    });