angular.module('mf.tracker', ['ngMessages', 'mf.data', 'mf.auth', 'mf.controls'])

    // Init

    .run(function($rootScope, uow) {
        $rootScope.uow = uow;

        // TODO: move periods initialization to TrackerCtrl
        // $rootScope.periods = uow['costs'].periods();
    });