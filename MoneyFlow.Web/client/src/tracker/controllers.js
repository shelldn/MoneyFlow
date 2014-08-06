angular.module('mf.tracker')

    //
    // Tracker

    .controller('TrackerCtrl', function(Cost, uow) {
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

                .then(function() {
                    self.init();
                })

                ['finally'](function() {
                    self.isProcessing = false;
                });
        };
    });