angular.module('mf.auth')

    //
    // Authentication manager

    .service('authManager', function($http, localStorageService) {
        var self = this;

        // TODO: Find the proper place for this helper method
        function urlEncode(obj) {
            var urlParts = [];

            for (var prop in obj) urlParts.push(
                encodeURIComponent(prop) + '=' +
                encodeURIComponent(obj[prop]));

            return urlParts.join('&');
        }

        self.signIn = function(cred) {

            var atr = { // access token request
                grant_type: 'password',
                username: cred.userName,
                password: cred.password
            };

            return $http.post('/token', atr, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: urlEncode

            }).success(function(response) {
                localStorageService.set(
                    'access_token',
                    response.access_token
                );
            });
        };

        self.signOut = function() {
            localStorageService.remove('access_token');
        };
    });