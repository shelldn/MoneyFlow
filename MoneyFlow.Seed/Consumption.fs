namespace MoneyFlow.Seed

open System
open MoneyFlow.Model

module Math =
    let round (digits : int) (value : float) = 
        Math.Round(value, digits)

module Consumption =    
    let private rnd = Random()

    let private nextCategory () =
        let catCount = categories |> Seq.length
        let catId = rnd.Next(catCount)

        categories |> Seq.nth catId

    let private nextDate (min : DateTime) (max : DateTime) =
        let maxMin = (max - min).TotalMinutes |> int

        rnd.Next(maxMin) |> float |> min.AddMinutes

    let private nextAmount min max = 
        rnd.Next(min, max)
        |> (float >> (+)) 
        <| rnd.NextDouble()
        |> decimal
        
    let seed n minDate maxDate minAmount maxAmount = 
        [ for i in 1..n -> 
            Cost(
                Category = nextCategory(),
                Date = nextDate minDate maxDate,
                Amount = nextAmount minAmount maxAmount
            ) ]