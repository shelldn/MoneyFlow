###
Costs generator helper
###

costs = (yearHash) ->
  Cost = (@date) ->

  _costs = []

  for own year, months of yearHash
    _costs.push new Cost new Date Date.UTC year, month % 12 \
      for hasCost, month in months when hasCost

  _costs