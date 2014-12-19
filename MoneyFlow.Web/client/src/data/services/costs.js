angular.module('mf.data')

    //
    // Costs

    .factory('costs', function($resource) {
        return $resource('/api/costs', { id: '@id' }, {
            periods: {
                method: 'GET',
                isArray: true,
                url: '/api/costs/periods'
            },
            query: {
                method: 'GET',
                isArray: true,
                transformResponse: function(data) {

                    return (function(costs) {
                        costs.forEach(function(c) {
                            c.date = new Date(c.date);
                        });

                        return costs;

                    })(angular.fromJson(data));
                }
            }
        });
    });