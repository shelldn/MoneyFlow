namespace MoneyFlow.Seed

open System
open System.Collections.Generic
open MoneyFlow.Model

module Consumptions =

    let seed n =
        [ for i in 0..n -> Consumption() ]

module Data =
    
    type T = { 
        Categories : Category seq;
        Consumptions : Consumption seq
    }

    type ApplyFunc = delegate of T -> unit
    
    let Seed cnsCount (applyFunc : ApplyFunc) =

        let data = { 
            Categories = read<Category> "Categories.xml";
            Consumptions = Consumptions.seed cnsCount
        }

        let rnd = Random()
        let catCount = data.Categories |> Seq.length

        let getDate (min : DateTime) = min.AddHours(float (rnd.Next(8760)) * rnd.NextDouble())

        for cns in data.Consumptions do
            cns.Category <- data.Categories |> Seq.nth (rnd.Next(catCount))
            cns.Date <- getDate (DateTime.Parse("1/1/2014"))
            cns.Amount <- decimal (rnd.Next(500)) + Math.Round(decimal (rnd.NextDouble()), 2)

        applyFunc.Invoke(data)