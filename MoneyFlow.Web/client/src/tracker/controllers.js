angular.module('mf.tracker')

    //
    // Tracker

    .controller('TrackerCtrl', function($scope, Cost, costStore) {
        var self = this;

        //
        // Save cost

        /*function createAsync(c) {
            return uow['costs'].save(c)
                .$promise;
        }*/

        (self.init = function() {
            self.periods = costStore.getPeriods();
            self.amount = null;
            self.category = {};

            self._isInitialized = true;

        })();

        self.commit = function() {
            self.isProcessing = true;

            var cost = new Cost(
                self.amount,
                self.category,
                new Date()
            );

            costStore.create(cost)

                .then(function(c) {
                    self.isProcessing = false;
                    $scope.$broadcast('costCreated', c);
                });
        };

        /*self.commit = function() {
            self.isProcessing = true;

            createAsync(new Cost(self.amt, self.cat))

                .then(function(c) {
                    $scope['costs']
                        .push(c);

                    self.init();
                })

                ['finally'](function() {
                    self.isProcessing = false;
                });
        };*/
    })

    //
    // Period

    .controller('PeriodCtrl', function(costStore) {
        var self = this;

        self.init = function(period) {
            self.costs = costStore
                .getByPeriod(period);
        };
    });