###
Array helpers
###

do (h = window.h ?= {}) ->

  # Checks if object is of array type
  h.isArray = Array.isArray ||
    (obj) -> {}.toString.call(obj) is '[object Array]'

  # Checks if array has duplicated values
  h.hasDuplicates = (arr) ->
    return true for x, i in arr.sort() when x[i] is x[i + 1]; false

  return