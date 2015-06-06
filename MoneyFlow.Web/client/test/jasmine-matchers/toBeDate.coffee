CustomMatchers = CustomMatchers || {};

CustomMatchers

  # toBeDate: test for ISO8601 matching

  .toBeDate = ->
    compare: (str) ->
      pass: not isNaN (new Date str).valueOf()