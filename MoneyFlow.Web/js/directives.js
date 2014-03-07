angular.module('mfApp')

	.directive('mfCategory', function() {
		return {
			require: 'ngModel',
			link: function(scope, element, attrs, ModelCtrl) {
				var parser = function(input) {
					return { description: input };
				};

				ModelCtrl.$parsers.push(parser);
			}
		};
	});