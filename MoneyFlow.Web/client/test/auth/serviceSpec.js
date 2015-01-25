describe('service: Account', function() {
    var Account, $httpBackend;

    beforeEach(module('mf.auth'));

    beforeEach(inject(function(_Account_, _$httpBackend_) {

        // Suite globals
        Account         = _Account_;
        $httpBackend    = _$httpBackend_;
    }));

    describe('isAuthorized', function() {
        var request;

        beforeEach(function() {
            request = $httpBackend
                .expectHEAD('/api/accounts/me');
        });

        it('should return true if the response had 204 status code', function() {
            var isAuthorized;

            request.respond(204);   // No Content

            // act
            isAuthorized = Account.isAuthorized();
            $httpBackend.flush();

            // assert
            expect(isAuthorized).toBeTruthy();
        });

        it('should return false if the response had 401 status code', function() {
            var isAuthorized;

            request.respond(401);   // Unauthorized

            // act
            isAuthorized = Account.isAuthorized();
            $httpBackend.flush();

            // assert
            expect(isAuthorized).toBeFalsy();
        });
    });
});

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

describe('service: authInterceptor', function() {
    var authInterceptor, ls;

    beforeEach(module('mf.auth'));

    beforeEach(inject(function(_authInterceptor_, localStorageService) {
        var _storage = {};

        // Suite globals
        authInterceptor = _authInterceptor_;
        ls = localStorageService;

        // Service config
        spyOn(ls, 'set').and.callFake(function(key, val) { _storage[key] = val; });
        spyOn(ls, 'get').and.callFake(function(key) { return _storage[key]; });
    }));

    it('should not set the "Authorization" header if there is no stored auth token.', function() {
        var config = { headers: {} };

        // act
        config = authInterceptor.request(config);

        // assert
        expect(config.headers['Authorization']).toBeUndefined();
    });

    it('should set the "Authorization" header with stored Bearer token', function() {
        var config = { headers: {} },
            token = 'AUTH_TOKEN';

        ls.set('access_token', token);

        // act
        config = authInterceptor.request(config);

        // assert
        expect(config.headers['Authorization']).toBe('Bearer ' + token);
    });
});