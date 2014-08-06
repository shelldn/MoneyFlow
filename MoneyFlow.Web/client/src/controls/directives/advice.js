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

                    var init =
                        scope.init = function() {
                            scope.options = [];
                        };

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

                    var has =
                        scope.has = function() {
                            return (function(opts) {
                                return !!(opts && opts.length);

                            })(scope.options);
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
                    // Processing keys

                    element.on('keydown', function(e) {

                        var sib = function(arr, it, inc) {
                            select(arr[arr.indexOf(it) + inc] || it);

                        // Bind fn to resolve the
                        // options array sibling
                        }.bind(null,
                            scope.options,
                            scope.val
                        );

                        // Process key pressed
                        (function(key, fn) {
                            if (has() && fn[key]) {
                                e.preventDefault();
                                scope.$apply(function() {
                                    fn[key]();
                                });
                            }

                        })(e.keyCode, {
                            13: init,
                            38: sib.bind(null, -1),   // up = prev
                            40: sib.bind(null, +1)    // down = next
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