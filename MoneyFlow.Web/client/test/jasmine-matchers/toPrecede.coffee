CustomMatchers = CustomMatchers || {};

CustomMatchers

  # toPrecede: test for two dates to be siblings by months

  .toPrecede = ->
    compare: (current, next) ->
      current   = new Date current
      next      = new Date next

      pass: switch next.getYear() - current.getYear()

        # If same year
        when 0 then next.getMonth() - current.getMonth()

        # If i.e. Dec 2014 and Jan 2015
        when 1 then current.getMonth() == 11 && next.getMonth() == 0

        else false

      message: if @pass then 'The dates are siblings by months' else 'The dates differs more than by one month'