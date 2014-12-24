angular.module('mf.tracker', ['mf.data', 'mf.controls'])

    // Types

    .constant('Cost', function(amt, cat) {
        this.amount = amt;
        this.category = cat;
        this.date = new Date();
    })

    // Init

    .run(function($rootScope, uow) {
        $rootScope.uow = uow;

        // TODO: move periods initialization to TrackerCtrl
        // $rootScope.periods = uow['costs'].periods();
    });