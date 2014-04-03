angular.module('mf.components')

    //
    // Understand

    .factory('understand', function() {
        return function(exp) {
            var match = exp.match(/^(\w+?)\s+as\s+\1\.(\w+?)\s+in\s+(\w+?)$/);

            if (!match) throw 'mf: invalid options expression';

            return {
                item: match[1],
                prop: match[2],
                set: match[3]
            };
        }
    });