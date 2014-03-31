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