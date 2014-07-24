angular.module('mf.tracker', ['mf.data', 'mf.controler'])

    .run(function($rootScope, uow) {
        $rootScope.cats = uow.categories;
    });