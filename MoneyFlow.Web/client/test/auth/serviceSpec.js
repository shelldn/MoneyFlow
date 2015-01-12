describe('service: authManager', function() {
    var authManager, $httpBackend, ls;

    var MIME_URLENCODED = 'application/x-www-form-urlencoded';

    beforeEach(module('mf.auth'));

    beforeEach(inject(function(_authManager_, _$httpBackend_, localStorageService) {
        // suite globals
        authManager = _authManager_;
        $httpBackend = _$httpBackend_;
        ls = localStorageService;

        // api mock
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe('signIn(Object)', function() {
        var cred, token = 'ABC';

        function isContentType(val) {
            return function(headers) { return headers['Content-Type'] === val; }
        }

        beforeEach(function() {
            cred = { userName: 'johny', password: '123456' };

            $httpBackend.expectPOST('/token',
                "grant_type=password" +
                "&username=" + cred.userName +
                "&password=" + cred.password,

                isContentType(MIME_URLENCODED)

            ).respond({ access_token: token });
        });
        
        it('should persist access token fetched from OAuth endpoint', function() {
            var storage = {};

            spyOn(ls, 'set').and.callFake(function(key, val) { storage[key] = val; });
            spyOn(ls, 'get').and.callFake(function(key) { return storage[key]; });

            // act
            authManager.signIn(cred);
            $httpBackend.flush();

            // assert
            expect(ls.get('access_token')).toEqual(token);
        }); 
    });
});