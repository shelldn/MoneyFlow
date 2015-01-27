describe('controller: AuthCtrl', function() {
    var $scope, $window,
        Account, authManager;

    beforeEach(module('mf.auth', function($provide) {
        var Account = jasmine
            .createSpyObj('Account', ['isAuthorized']);

        $provide.value('Account', Account);
    }));

    beforeEach(inject(function($controller, $rootScope, _$window_, _Account_, _authManager_) {

        // Initialize controller
        $controller('AuthCtrl', {
            $scope          : $scope          = $rootScope.$new(),
            $window         : $window         = _$window_,
            Account         : Account         = _Account_,
            authManager     : authManager     = _authManager_
        });
    }));

    describe('isAuthorized', function() {

        it('should be fetched only once on construction', function() {

            Account.isAuthorized
                .and.callFake(function() {
                    return new Boolean(false);
                });

            // act/assert
            expect($scope.isAuthorized).toBe($scope.isAuthorized);
        });

        it('should return the current account authorization status', function() {
            var authorized = true;

            Account.isAuthorized
                .and.returnValue(authorized);

            // act/assert
            expect($scope.isAuthorized).toBe(authorized);
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