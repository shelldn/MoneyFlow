angular.module('mf.tracker', ['mf.data', 'mf.controls'])

    // Types

    .constant('Cost', function() {
        this.amount = null;
        this.category = {
            description: ''
        };
    })

    // Init

    .run(function($rootScope, uow) {
        $rootScope.cats = uow['categories'];
    });