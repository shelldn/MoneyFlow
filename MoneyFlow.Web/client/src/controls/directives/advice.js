angular.module('mf.controls')

    //
    // Advice

    .directive('mfAdvice', function($parseExp, $templateCache, $http, $compile) {
        return {
            require: 'ngModel',
            scope: {
                val: '=ngModel'
            },
            compile: function(element, attrs) {
                var propName, asstExp;

                //
                // Lookup expression parsing

                (function $parse(exp) {
                    var tokens = $parseExp.$from(exp);

                    /* from */
                    exp = tokens.left;
                    asstExp = tokens.right;

                    tokens = $parseExp.$as(exp);

                    /* as */
                    propName = tokens.right;

                })(attrs['mfAdvice']);

                /* Link */

                return function(scope, element, attrs, ModelCtrl) {
                    var asst =
                        scope.$parent.$eval(asstExp);

                    //
                    // Template loading

                    $http.get('/tmpl/controls/advice', { cache: $templateCache })
                        .success(function(tmpl) {
                            var advice =
                                $compile(tmpl)(scope);

                            element.after(advice);
                        });

                    //
                    // Controller

                    var viewOf =
                        scope.viewOf = function(o) {
                            return o[propName];
                        };

                    var select =
                        scope.select = function(o) {
                            scope.val = o;
                        };

                    var isIt =
                        scope.isIt = function(o) {
                            return viewOf(o) ==
                                viewOf(scope.val);
                        };

                    scope.has = function() {
                        return (function(opts) {
                            return !!(opts && opts.length);

                        })(scope.options);
                    };

                    scope.close = function() {
                        scope.options = [];
                    };

                    //
                    // Model view

                    ModelCtrl.$formatters.push(viewOf);

                    //
                    // Model parsing

                    ModelCtrl.$parsers.push(function(propVal) {
                        scope.options =
                            asst.query({ q: propVal });

                        return Object.defineProperty({}, propName, {
                            value: propVal,
                            enumerable: true
                        });
                    });

                    //
                    // Options match

                    scope.$watchCollection('options', function(opts) {
                        (function(it) {
                            if (!!it) select(it);

                        })(_.find(opts, isIt));
                    });
                };
            }
        };
    });