angular.module('mf.controls')

    //
    // Expand

    .directive('mfExpand', function() {
        return {
            transclude: true,
            link: function(scope) {
                scope.tglExpand = function() {
                    scope.isExpanded = !scope.isExpanded;
                };
            },
            templateUrl: function(_, attrs) {
                return "/tmpl/controls/expand?caption=" + attrs['mfExpand'];
            }
        };
    });