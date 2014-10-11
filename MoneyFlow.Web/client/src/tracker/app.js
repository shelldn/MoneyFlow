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
        $rootScope.costs = uow['costs'].query();
    });