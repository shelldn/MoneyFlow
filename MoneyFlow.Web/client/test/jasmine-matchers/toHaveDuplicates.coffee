CustomMatchers = CustomMatchers || {}

CustomMatchers

  # toHaveDuplicates: test for having duplicated items in array

  .toHaveDuplicates = ->
    compare: (arr) ->
      hasDuplicates = () ->
        sarr = arr.sort()
        for i in [0...sarr.length - 1] by 1
          return true if sarr[i] is sarr[i + 1]

      pass: if arr.length < 2 or hasDuplicates() then true else false