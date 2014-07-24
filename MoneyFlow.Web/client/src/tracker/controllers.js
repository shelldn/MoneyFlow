angular.module('mf.tracker')

    //
    // Tracker

    .controller('TrackerCtrl', function(Cost) {
        var self = this;

        (self.init = function() {
            self.cost = new Cost();
        })();

        self.commit = function() {
            console.log(self.cost);
            self.init();
        };
    });