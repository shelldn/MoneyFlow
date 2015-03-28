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

  app.service 'localCostStore', ['localStorageService', LocalCostStore]