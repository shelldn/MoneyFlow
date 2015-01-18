angular.module 'mf.auth'

  #
  # Auth interceptor

  .service 'authInterceptor', (localStorageService) ->

    @request = (config) ->
      token = localStorageService.get 'access_token';
      config.headers['Authorization'] = "Bearer #{token}" if token;
      config

    return