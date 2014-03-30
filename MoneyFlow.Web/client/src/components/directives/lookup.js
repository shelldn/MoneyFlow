angular.module('mf.components')

    //
    // Lookup

    .directive('input', function() {
        return {
            restrict: 'E',
            controller: function() {
                var self = this;

                self.addOption = function(opt) {
                    console.log(opt);
                };
            },
            link: function(scope, element, attrs) {
                if (attrs['type'] !== 'lookup') return;
            }
        };
    });