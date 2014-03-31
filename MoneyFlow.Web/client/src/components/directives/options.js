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

                scope.$watchCollection('options', function(options) {
                    LookupCtrl.options = options;
                });
            }
        };
    });