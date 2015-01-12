describe('controller: AuthCtrl', function() {
    var $scope, $window,
        authManager, signInDeferred;

    beforeEach(module('mf.auth'));

    beforeEach(inject(function($controller, $rootScope, _$window_, $q, _authManager_) {
        // suite globals
        $scope = $rootScope.$new();
        $window = _$window_;
        authManager = _authManager_;

        spyOn(authManager, 'signIn')
            .and.callFake(function() {
                return (signInDeferred = $q.defer()).promise;
            });

        $controller('AuthCtrl', { $scope: $scope, $window: $window, authManager: authManager });
        $scope.$digest();
    }));

    describe('signIn()', function() {
        
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