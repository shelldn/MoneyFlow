angular.module('mf.data')

    // Link

    .filter('link', function() {
        return function(input) {
            for (var i = 0; i < input.length; i++) {
                input[i].prev = input[i - 1];
                input[i].next = input[i + 1];
            }

            return input;
        };
    });