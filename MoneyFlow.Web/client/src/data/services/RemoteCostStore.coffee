do (app = angular.module 'mf.data') ->

  class RemoteCostStore

    constructor: ($resource, $filter) ->
      @date = $filter 'date'

      @Cost = $resource '/api/costs', id: '@id',

        getPeriods:
          method: 'GET'
          url: '/api/costs/periods'
          isArray: true

        getByPeriod:
          method: 'GET'
          url: '/api/costs/:year-:month'
          isArray: true

    getPeriods: -> @Cost.getPeriods()

    getByPeriod: (period) ->
      @Cost.getByPeriod
        year: @date period, 'yyyy'
        month: @date period, 'MM'

    create: (cost) ->
      @Cost.save(cost).$promise


  app.service 'remoteCostStore', ['$resource', '$filter', RemoteCostStore]