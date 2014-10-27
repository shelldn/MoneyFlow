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
            template: function(_, attrs) {
                return '<a href="javascript:;" ng-click="tglExpanded()">' + attrs['mfExpand'] + '</a>' +
                    '<div ng-show="isExpanded" ng-transclude></div>';
            }
        };
    });