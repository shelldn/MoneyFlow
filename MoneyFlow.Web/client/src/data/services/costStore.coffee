angular.module 'mf.data'

  .service 'costStore', ($resource, $filter) ->
    date = $filter 'date'

    Cost = $resource '/api/costs', id: '@id',

      getByPeriod:
        method: 'GET'
        url: '/api/costs/:year-:month'
        isArray: true,

    this.getByPeriod = (period) ->
      Cost.getByPeriod
        year: date period, 'yyyy'
        month: date period, 'MM'

    return
