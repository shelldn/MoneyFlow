var Query = function(q) {
    this.q = q;
};

angular.module('mf.components')

    //
    // Lookup option

    .directive('mfLookupOption', function() {
        return {
            require: '^mfLookup',
            link: function(scope, element, attrs, LookupCtrl, transclude) {
                scope[LookupCtrl.item] = scope['o'];

                transclude(scope, function(clone) {
                    element.empty();
                    element.append(clone);
                });
            }
        };
    })

    //
    // Lookup

    .directive('mfLookup', function() {
        return {
            restrict: 'E',
            require: 'ngModel',
            transclude: true,
            scope: {
                model: '=ngModel'
            },
            controller: function($scope) {
                var self = this;

                self.select = function(o) {
                    $scope.model = o;
                };

                self.isSelected = function(o) {
                    return self.option == o;
                };

                self.lookup = function(q) {
                    self.options = self.repo.query(new Query(q));
                };

                self.nav = function(e) {
                    switch (e.keyCode) {

                        // Enter
                        case 13:
                            self.select(self.option);
                            e.preventDefault();
                            break;

                        // Вниз
                        case 40:
                            self.option = self.options[self.options.indexOf(self.option) + 1] || self.option;
                            e.preventDefault();
                            break;

                        // Вверх
                        case 38:
                            self.option = self.options[self.options.indexOf(self.option) - 1] || self.option;
                            e.preventDefault();
                            break;
                    }
                };

                $scope.$watch('vm.option', function(o) {
                    if (!_.isUndefined(o)) {
                        self.q = o[self.prop];
                    }
                });

                $scope.$watchCollection('vm.options', function(options) {
                    self.option = _.find(options, function(o) {
                        return self.q == o[self.prop];
                    });
                });
            },
            controllerAs: 'vm',
            templateUrl: '/tmpl/lookup'
        };
    });