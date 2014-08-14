namespace MoneyFlow.Seed

open System
open System.Collections.Generic
open MoneyFlow.Model

module Data =
    
    type T = { 
        Categories : Category seq;
        Consumptions : Cost seq
    }

    type ApplyFunc = delegate of T -> unit
    
    let Seed (options : SeedOptions) (applyFunc : ApplyFunc) =

        let data = {
            Categories = categories;
            Consumptions = Consumption.seed 
                options.ConsumptionCount 
                options.MinDate 
                options.MaxDate 
                options.MinAmount 
                options.MaxAmount
        }

        applyFunc.Invoke(data)