angular.module('mf.data')

    //
    // Costs

    .factory('costs', function($resource) {
        return $resource('/api/costs', { id: '@id' });
    });