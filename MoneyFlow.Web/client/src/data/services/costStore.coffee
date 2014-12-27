angular.module 'mf.data'

  .service 'costStore', ($resource, $filter) ->
    date = $filter 'date'

    Cost = $resource '/api/costs', id: '@id',

      getPeriods:
        method: 'GET'
        url: '/api/costs/periods'
        isArray: true

      getByPeriod:
        method: 'GET'
        url: '/api/costs/:year-:month'
        isArray: true

    this.getPeriods = -> Cost.getPeriods()

    this.getByPeriod = (period) ->
      Cost.getByPeriod
        year: date period, 'yyyy'
        month: date period, 'MM'

    return
