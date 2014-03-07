/* MoneyFlow v.0.0.1 */

angular.module('mf.data', ['ngResource']);
angular.module('mf.data')

    .factory('uow', function($resource) {
        var
            categories = $resource('/api/categories'),
            consumptions = $resource('api/consumptions', null, {
                periods: {
                    method: 'GET',
                    url: '/api/consumptions/periods',
                    isArray: true
                }
            });

        return {
            categories: categories,
            consumptions: consumptions
        };
    });