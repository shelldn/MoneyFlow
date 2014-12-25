angular.module('mf.tracker')

    //
    // Tracker

    .controller('TrackerCtrl', function(Cost, $scope, uow) {
        var self = this;

        //
        // Save cost

        function createAsync(c) {
            return uow['costs'].save(c)
                .$promise;
        }

        (self.init = function() {
            self.amt = null;
            self.cat = {};

        })();

        self.commit = function() {
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
        };
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