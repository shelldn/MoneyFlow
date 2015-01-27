angular.module('mf.auth')

    // Authentication

    .controller('AuthCtrl', function($scope, $window, Account, authManager) {

        var _isAuthorized;

        Object.defineProperty($scope, 'isAuthorized', {
            enumerable: true,
            get: function() {
                return _.isUndefined(_isAuthorized) ?
                    _isAuthorized = Account.isAuthorized() :
                    _isAuthorized;
            }
        });

        $scope.signIn = function() {
            authManager.signIn({
                userName: $scope.userName,
                password: $scope.password

            }).then(function() {

                // hard-reload entire page
                $window.location.reload();
            });
        };
    });