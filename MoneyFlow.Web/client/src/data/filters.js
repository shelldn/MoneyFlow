angular.module('mf.data')

    // Link

    .filter('link', function() {
        return function(input, reverse) {
            var k = reverse ? -1 : 1;

            for (var i = 0; i < input.length; i++) {
                input[i].prev = input[i - k];
                input[i].next = input[i + k];
            }

            return input;
        };
    });