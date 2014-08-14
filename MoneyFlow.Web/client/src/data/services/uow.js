angular.module('mf.data')

    //
    // Uow

    .service('uow', function($injector) {
        var self = this;

        /*
        * Используемые репозитории */

        ['cats', 'costs']

            .forEach(function(r) {
                self[r] = $injector.get(r);
            });
    });