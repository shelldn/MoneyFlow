/* MoneyFlow v.0.0.1 */

angular.module('mf.components', []);
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
angular.module('mf.components')

    //
    // Options

    .directive('mfOptions', function() {
        return {
            require: 'input',
            link: function(scope, element, attrs, LookupCtrl) {

                // Функция парсинга выражения опций
                function understand(expression) {
                    var pattern = /^(\w+?)\s+as\s+\1\.(\w+?)\s+in\s+(\w+?)$/,
                        match = expression.match(pattern);

                    if (!match) throw 'mf: invalid options expression';

                    return {
                        item: match[1],
                        prop: match[2],
                        set: match[3]
                    };
                }

                var names = understand(attrs['mfOptions']);

                scope.$watch(names.set, function(opts) {
                    _.each(opts, function(opt) {
                        LookupCtrl.addOption(opt);
                    });
                });
            }
        };
    });