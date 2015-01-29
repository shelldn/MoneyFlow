describe('service: Account', function() {
    var Account, $httpBackend;

    beforeEach(module('mf.auth'));

    beforeEach(inject(function(_Account_, _$httpBackend_) {

        // Suite globals
        Account         = _Account_;
        $httpBackend    = _$httpBackend_;
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe('isAuthorized', function() {
        var isAuthorizedSuccess =
                jasmine.createSpy('success');

        var request;

        beforeEach(function() {
            request = $httpBackend
                .expectHEAD('/api/accounts/me');
        });

        it('should return true if the response had 204 status code', function() {
            request.respond(204);   // No Content

            // act
            Account.isAuthorized()
                .$promise.then(isAuthorizedSuccess);

            $httpBackend.flush();

            // assert
            expect(isAuthorizedSuccess).toHaveBeenCalledWith(true);
        });

        it('should return false if the response had 401 status code', function() {
            request.respond(401);   // Unauthorized

            // act
            Account.isAuthorized()
                .$promise.then(isAuthorizedSuccess);

            $httpBackend.flush();

            // assert
            expect(isAuthorizedSuccess).toHaveBeenCalledWith(false);
        });
    });
});

describe('service: authManager', function() {
    var authManager, $httpBackend, ls;

    var MIME_URLENCODED = 'application/x-www-form-urlencoded';

    beforeEach(module('mf.auth', function($provide) {

        // Mock local storage service
        var ls = jasmine.createSpyObj('localStorageService', ['get', 'set']);

        // Provide it to app
        $provide.value('localStorageService', ls);
    }));

    beforeEach(inject(function(localStorageService) {
        var _store = {};

        // Setup local storage service mock
        localStorageService.get.and.callFake(function(key)       { return _store[key];  });
        localStorageService.set.and.callFake(function(key, val)  { _store[key] = val;   });
    }));

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

            // act
            authManager.signIn(cred);
            $httpBackend.flush();

            // assert
            expect(ls.get('access_token')).toEqual(token);
        }); 
    });

    describe('signOut()', function() {
        var token = 'ABC123';

        it('should remove access token from application storage', function() {
            ls.set('access_token', token);

            // act
            authManager.signOut();

            // assert
            expect(ls.get('access_token')).toBeUndefined();
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