angular.module('mfApp')

	.factory('uow', function($resource) {
        var 
            months = $resource('/api/months', null, {
                query: {
                    method: 'GET',
                    responseType: 'application/json',
                    isArray: true,
                    transformResponse: function (data) {
                        var response =
                            _.map(angular.fromJson(data),
                                function (p) {
                                    return new Date(p.year, p.month);
                                });

                        return response;
                    }
                }
            }),

            categories = $resource('/api/categories'),
            consumptions = $resource('/api/consumptions', null, {
            	query: {
            		method: 'GET',
            		responseType: 'application/json',
            		isArray: true,
            		transformResponse: function(data) {
            			var consumptions = angular.fromJson(data),
            			response = _.map(consumptions, function(cns) {
            				cns.date = new Date(cns.date);
            				return cns;
            			});

            			return response;
            		}
            	}
            });

        return {
            months: months,
            categories: categories,
            consumptions: consumptions
        };
    })