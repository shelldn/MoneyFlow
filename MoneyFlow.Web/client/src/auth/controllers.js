angular.module('mf.auth')

    // Authentication

    .controller('AuthCtrl', function($scope, $window, authManager) {
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