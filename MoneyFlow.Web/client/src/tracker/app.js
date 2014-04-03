angular.module('mf.tracker', ['mf.data', 'mf.components'])

    .run(function($rootScope, uow) {
        $rootScope.cats = uow.categories;
    });