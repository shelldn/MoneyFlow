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
                    self.init();
                    $scope.$broadcast('costCreated', c);
                })

                ['finally'](function() {
                    self.isProcessing = false;
                });
        };
    })

    //
    // Period

    .controller('PeriodCtrl', function($scope, costStore) {
        var self = this;

        function appendIfPeriodsMatch(c) {
            var periodsMatch =
                moment(self._period)
                    .isSame(c.period);

            if (periodsMatch)
                self.costs.push(c);
        }

        self.init = function(period) {
            self._period = period;

            self.costs = costStore
                .getByPeriod(period);
        };

        self.isYesterday = function(c) {
            if (!c.next) return true;

            return moment(c.next.date)
                .isAfter(c.date, 'day');
        };

        self.sameDay = function(c) {
            var count = 1;

            while (c.next && moment(c.date).isSame(c.next.date, 'day')) {
                count++;
                c = c.next;
            }

            return count;
        };

        $scope.$on('costCreated', function(e, c) {
            appendIfPeriodsMatch(c);
        });
    });