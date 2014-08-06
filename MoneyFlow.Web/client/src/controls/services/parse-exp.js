angular.module('mf.controls')

    //
    // Expression parser

    .factory('$parseExp', function() {
        var token = '([\\S\\s]+?)';

        function opFactory(operator, tokens) {
            tokens = tokens || {};

            var pattern =
                new RegExp('^{left}\\s+{operator}\\s+{right}$'
                    .replace('{left}', tokens.left || token)
                    .replace('{right}', tokens.right || token)
                    .replace('{operator}', operator));

            return function(exp) {
                var tokens = exp.match(pattern);

                if (!tokens)
                    throw '$parseExp: no {token} token'
                        .replace('{token}', operator);

                return {
                    left: tokens[1],
                    right: tokens[2]
                };
            };
        }

        return {
            $in: opFactory('in'),
            $from: opFactory('from'),
            $as: opFactory('as', { right: '\\1\\.([\\S\\s]+?)' }),
            $for: opFactory('for', { left: '([\\S\\s]+?)\\.([\\S\\s]+?)', right: '\\1' }),
            $trackBy: opFactory('track by')
        };
    });