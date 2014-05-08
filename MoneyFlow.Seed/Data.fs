namespace MoneyFlow.Seed

open System
open System.Collections.Generic
open MoneyFlow.Model

module Data =
    
    type T = { 
        Categories : Category seq;
        Consumptions : Consumption seq
    }

    type ApplyFunc = delegate of T -> unit
    
    let Seed cnsCount (applyFunc : ApplyFunc) =

        let data = { 
            Categories = read "Categories.xml";
            Consumptions = [ for i in 0..cnsCount -> Consumption() ]
        }

        applyFunc.Invoke(data)