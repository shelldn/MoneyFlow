do (app = angular.module 'mf.data') ->

  class LocalCostStore

    constructor: (@ls) ->

    getPeriods: ->
      dates = _(@ls.get 'costs')
        .map (c) -> Date.parse c.date

      curr = moment.utc _(dates).min()
      edge = moment.utc _(dates).max()

      while not curr.isAfter edge
        prev = curr.toISOString()
        curr.add 1, 'M'
        prev

    getByPeriod: (p) ->
      p = new Date p
      _(@ls.get 'costs')
        .filter((c) ->
          c = new Date c.date
          p.getYear()   == c.getYear() &&
          p.getMonth()  == c.getMonth()
        );

    create: (c) ->
      costs = @ls.get 'costs'
      costs.push c
      @ls.set 'costs', costs

  app.service 'localCostStore', ['localStorageService', LocalCostStore]