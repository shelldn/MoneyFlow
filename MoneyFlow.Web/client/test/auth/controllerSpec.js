describe('controller: AuthCtrl', function() {
    var $scope, $window,
        Account, authManager;

    beforeEach(module('mf.auth'));

    beforeEach(inject(function($controller, $rootScope, _$window_, _Account_, _authManager_) {

        $controller('AuthCtrl', {
            $scope          : $scope          = $rootScope.$new(),
            $window         : $window         = _$window_,
            Account         : Account         = _Account_,
            authManager     : authManager     = _authManager_
        });

        // Go!
        $scope.$digest();
    }));

    describe('isAuthorized', function() {

        it('should be fetched only once on construction', function() {

            spyOn(Account, 'isAuthorized')
                .and.returnValue(false);

            // act
            var results = [
                $scope.isAuthorized,    // 1st getter invoke
                $scope.isAuthorized     // 2nd getter invoke
            ];

            // assert
            expect(first(results)).toBe(second(results));
        });

        it('should return the current account authorization status', function() {
            var isAuthorized = true;

            spyOn(Account, 'isAuthorized')
                .and.returnValue(isAuthorized);

            // act/assert
            expect($scope.isAuthorized).toBe(isAuthorized);
        });
    });

    describe('signIn()', function() {
        var signInDeferred;

        beforeEach(inject(function($q) {
            spyOn(authManager, 'signIn')
                .and.callFake(function() {
                    return (signInDeferred = $q.defer()).promise;
                });
        }));

        it('should reload page on successful sign_in', function() {

            spyOn($window.location, 'reload');

            // act
            $scope.signIn();
            signInDeferred.resolve();
            $scope.$digest();

            // assert
            expect($window.location.reload).toHaveBeenCalled();
        }); 
    });
});