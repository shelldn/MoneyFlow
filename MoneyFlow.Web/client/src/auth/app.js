angular.module('mf.auth', ['LocalStorageModule'])

    .config(function(localStorageServiceProvider) {
        localStorageServiceProvider.setPrefix('mf');
    });