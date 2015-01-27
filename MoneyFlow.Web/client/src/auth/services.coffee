angular.module 'mf.auth'

  #
  # Auth interceptor

  .service 'authInterceptor', (localStorageService) ->

    @request = (config) ->
      token = localStorageService.get 'access_token';
      config.headers['Authorization'] = "Bearer #{token}" if token;
      config

    return

  #
  # Account resource

  .factory 'Account', ($resource) ->

    $resource '/api/accounts', null,

      isAuthorized:
        method: 'HEAD'
        url: '/api/accounts/me'
        interceptor:
          response:       (x) -> true   if x.status == 204  # No Content
          responseError:  (x) -> false  if x.status == 401  # Unauthorized