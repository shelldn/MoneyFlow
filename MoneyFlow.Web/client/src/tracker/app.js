angular.module('mf.tracker', ['mf.data', 'mf.controls'])

    // Types

    .constant('Cost', function() {
        this.amount = null;
        this.category = { words: '' };
    })

    // Init

    .run(function($rootScope, uow) {
        $rootScope.uow = uow;
    });