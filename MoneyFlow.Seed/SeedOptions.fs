namespace MoneyFlow.Seed

open System

type SeedOptions() =
    member val ConsumptionCount = 0 with get, set
    member val MinDate = DateTime.MinValue with get, set
    member val MaxDate = DateTime.MaxValue with get, set
    member val MinAmount = 1 with get, set
    member val MaxAmount = Int32.MaxValue with get, set