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
            self.cost = new Cost();
        })();

        self.commit = function() {
            self.isProcessing = true;

            createAsync(self.cost)

                .then(function(c) {
                    self.init();
                    $scope.costs.push(c);
                })

                ['finally'](function() {
                    self.isProcessing = false;
                });
        };
    });