angular.module('mf.components')

    //
    // Options

    .directive('mfOptions', function(understand) {
        return {
            require: 'mfLookup',
            link: function(scope, element, attrs, LookupCtrl) {
                var names = understand(attrs['mfOptions']);

                LookupCtrl.item = names.item;
                LookupCtrl.prop = names.prop;
                LookupCtrl.repo = scope.$eval(names.set);
            }
        };
    });