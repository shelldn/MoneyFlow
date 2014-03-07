angular.module('mfApp')

	.controller('MainCtrl', 
        function ($scope, $http, uow) {
        	$scope.identity = angular.identity;
        	$scope.categories = uow.categories.query();

            $http.get('/api/months')
            	.success(function(data) {
	            	var months = $scope.months = 
	            		_.map(data, function(m) {
		            		return new Date(m);
		            	});

	            	$scope.change(_.max(months));
	            });

        	$scope.$watch('consumptions', function(consumptions) {
        		var sum = 0;

        		_.each(consumptions, function(cns) {
        			sum += cns.amount;
        		});

        		$scope.sum = sum;
        		
        	}, true);

        	$scope.change = function(month) {
        		$scope.month = month;
        		$scope.consumptions = 
        			uow.consumptions.query({
	        			month: month.getMonth() + 1,
	        			year: month.getFullYear()
	        		});
        	};

            $scope.append = function () {
                uow.consumptions.save($scope.cns, 
                	function(cns) {
                		cns.date = new Date(cns.date);
                		$scope.consumptions.push(cns);
                		$scope.categories = uow.categories.query();
                	});
            };
        });