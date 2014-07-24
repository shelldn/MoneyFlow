angular.module('mf.data')

    //
    // Categories

    .factory('cats', function($resource) {
        return $resource('/api/categories', { id: '@id' });
    });