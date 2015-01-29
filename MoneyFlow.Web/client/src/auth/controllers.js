angular.module('mf.auth')

    // Authentication

    .controller('AuthCtrl', function($scope, $window, Account, authManager) {

        Account.isAuthorized(function(isAuthorized) {
            $scope.isAuthorized = isAuthorized;
        });

        $scope.$watch('isAuthorized', function(isAuthorized) {
            if (isAuthorized) $scope.account = Account.me();
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

        $scope.signOut = function() {
            authManager.signOut();
            $window.location.reload();
        }
    });