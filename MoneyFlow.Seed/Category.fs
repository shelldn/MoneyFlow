namespace MoneyFlow.Seed

open MoneyFlow.Model

[<AutoOpen>]
module Category =
    let categories =
        read<Category> "Categories.xml"