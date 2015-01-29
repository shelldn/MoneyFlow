describe('controller: AuthCtrl', function() {
    var $scope, $window,
        Account, authManager,
        isAuthorizedDeferred;

    beforeEach(module('mf.auth', function($provide) {
        var Account = jasmine
            .createSpyObj('Account', ['isAuthorized', 'me']);

        $provide.value('Account', Account);
    }));

    beforeEach(inject(function($q, Account) {

        // Mock setup
        isAuthorizedDeferred = $q.defer();

        Account.isAuthorized
            .and.callFake(function(success) {
                return isAuthorizedDeferred.promise.then(success);
            });
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

        it('should fetch authorization status on init', function() {

            // act
            isAuthorizedDeferred.resolve(true);
            $scope.$digest();

            // assert
            expect($scope.isAuthorized).toBe(true);
        });
    });

    describe('account', function() {

        it('should fetch account info only if it is authorized', function() {
            var me = {};

            Account.me.and.returnValue(me);

            // act/assert
            $scope.isAuthorized = false;
            $scope.$digest();
            expect($scope.account).toBeUndefined();

            $scope.isAuthorized = true;
            $scope.$digest();
            expect($scope.account).toBe(me);
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

    describe('signOut()', function() {

        it('should reload page', function() {
            spyOn($window.location, 'reload');

            // act
            $scope.signOut();

            // assert
            expect($window.location.reload).toHaveBeenCalled();
        });
    });
});