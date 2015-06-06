ISODate = (year, month) ->
  new Date Date.UTC year, month - 1
    .toISOString()