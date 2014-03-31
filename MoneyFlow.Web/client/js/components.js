/* MoneyFlow v.0.0.1 */

angular.module('mf.components', []);
angular.module('mf.components')

    //
    // Lookup

    .directive('input', function($compile) {
        return {
            require: 'ngModel',
            restrict: 'E',
            scope: {
                option: '=ngModel'
            },
            controller: function() {
                var self = this;

                self.select = function(option) {
                    self.option = option;
                };
            },
            controllerAs: 'vm',
            link: function(scope, element, attrs) {
                if (attrs['type'] !== 'lookup') return;

                var ul = $('<ul></ul>'),
                    li = $('<li ng-repeat="option in vm.options"></li>'),

                    a = $('<a href="javascript:;">{{ option.name }}</a>').attr({
                            'ng-class': '{ active: vm.option == option }',
                            'ng-click': 'vm.select(option)'
                        });

                ul.append(li.append(a));

                // Компилируем шаблон
                var tmpl = $compile(ul)(scope);

                element.wrap('<div class="lookup"></div>').after(tmpl);
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

                scope.$watchCollection('options', function(options) {
                    LookupCtrl.options = options;
                });
            }
        };
    });